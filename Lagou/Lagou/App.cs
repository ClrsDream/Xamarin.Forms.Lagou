using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.ViewModels;
using Lagou.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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

            this.Container
                .Singleton<TabViewModel>()
                .Singleton<SettingViewModel>()
                .Singleton<MDIViewModel>()
                .Singleton<IndexViewModel>()
                .Singleton<SearchViewModel>()
                .Singleton<CompanyPositionsViewModel>()
                .Singleton<MyViewModel>()

                .PerRequest<JobDetailViewModel>()
                .PerRequest<SearchedItemViewModel>()
                ;

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
                    if (viewType.FullName.EndsWith(".Windows")) {
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

            this.DisplayRootView<MDIView>();

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
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
