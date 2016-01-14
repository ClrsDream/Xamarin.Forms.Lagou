using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API;
using Lagou.ViewModels;
using Lagou.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou {
    public partial class App : FormsApplication {

        private SimpleContainer Container = null;

        public App(SimpleContainer container) {
            this.InitializeComponent();

            this.Container = container;
            this.RegistModel(container);
            this.FixCM(container);

            this.DisplayRootView<MDIView>();
            API.ApiClient.OnMessage += ApiClient_OnMessage;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void ApiClient_OnMessage(object sender, API.MessageArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                this.DealMessage(e);
            });
        }

        private async void DealMessage(MessageArgs e) {
            switch (e.ErrorType) {
                case ErrorTypes.NeedLogin:
                    var nav = await this.MainPage.DisplayAlert("提示", e.Message, "需要登陆", "返回上一页");
                    if (nav) {
                        this.Container.GetInstance<INavigationService>()
                            .For<LoginViewModel>()
                            .Navigate();
                    } else {
                        await this.Container.GetInstance<INavigationService>().GoBackAsync();
                    }
                    break;
                case ErrorTypes.Network:
                    await this.MainPage.DisplayAlert("网络异常", "当前网络不可用,请检查", "OK");
                    break;
                default:
                    await this.MainPage.DisplayAlert("提示", e.Message, "OK");
                    break;
            }
        }

        private void RegistModel(SimpleContainer container) {

            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => new { T = t, Mode = t.GetCustomAttribute<RegistAttribute>()?.Mode })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types) {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton) {
                    container.RegisterSingleton(type, null, type);
                } else if (t.Mode == InstanceMode.PreRequest) {
                    container.RegisterPerRequest(type, null, type);
                }
            }

            //container
            //    .Singleton<TabViewModel>()
            //    .Singleton<SettingViewModel>()
            //    .Singleton<MDIViewModel>()
            //    .Singleton<IndexViewModel>()
            //    .Singleton<SearchViewModel>()
            //    .Singleton<CompanyPositionsViewModel>()
            //    .Singleton<MyViewModel>()
            //    .Singleton<LoginViewModel>()
            //    .Singleton<FavoritesViewModel>()

            //    .PerRequest<CitySelectorViewModel>()
            //    .PerRequest<JobDetailViewModel>()
            //    .PerRequest<SearchedItemViewModel>()
            //    ;
        }

        private void FixCM(SimpleContainer container) {
            var f = ViewLocator.LocateTypeForModelType;
            ViewLocator.LocateTypeForModelType = (type, bindable, context) => {
                return f(type, bindable, context ?? Device.OS) ?? f(type, bindable, context);
            };

            var ps = string.Join("|", Enum.GetNames(typeof(TargetPlatform)).Select(p => string.Format(@"\.{0}", p)));
            var rx = new Regex(string.Format("({0})$", ps));
            var f2 = ViewModelLocator.LocateForViewType;
            ViewModelLocator.LocateForViewType = viewType => {
                var vm = f2(viewType);
                if (vm == null) {
                    if (rx.IsMatch(viewType.FullName)) {
                        //if (viewType.FullName.EndsWith(".Windows")) {
                        var vmTypeName = rx.Replace(viewType.FullName, "ViewModel")
                                        .Replace(".Views.", ".ViewModels.");
                        var vmType = Type.GetType(vmTypeName);
                        if (vmType != null) {
                            return container.GetInstance(vmType, null);
                        }
                    }
                }
                return vm;
            };
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            //防止因线程取消等错误把程挂掉
            e.SetObserved();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage) {
            //navigationPage.BarBackgroundColor = Color.Green;
            this.Container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }



        protected override void OnResume() {
            base.OnResume();
        }

        protected override void OnSleep() {
            base.OnSleep();
        }


        protected override void OnStart() {
            base.OnStart();
        }
    }
}
