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

                .PerRequest<JobDetailViewModel>()
                .PerRequest<SearchedItemViewModel>()
                ;

            var f = ViewLocator.LocateTypeForModelType;
            ViewLocator.LocateTypeForModelType = (type, bindable, context) => {
                return f(type, bindable, context ?? Device.OS) ?? f(type, bindable, context);
            };

            //ViewModelLocator.AddSubNamespaceMapping("*.Views.*", "{0}", "");

            var names = Enum.GetNames(typeof(TargetPlatform))
                .Select(p => string.Format(@"\.{0}", p));
            var ps = string.Format("({0})$", string.Join("|", names));
            var rx = new Regex(ps);
            var f2 = ViewModelLocator.LocateTypeForViewType;

            ViewModelLocator.LocateTypeForViewType = (viewType, searchForInterface) => {

                var typeName = viewType.FullName;
                var viewModelTypeList = ViewModelLocator.TransformName(typeName, searchForInterface).ToList();

                if (rx.IsMatch(typeName)) {
                    typeName = rx.Replace(typeName, "ViewModel");
                    return null;
                } else {
                    //var viewModelTypeList = ViewModelLocator.TransformName(typeName, searchForInterface).ToList();
                    return f2(viewType, searchForInterface);
                }
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
