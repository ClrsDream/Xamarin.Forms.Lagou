using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Caliburn.Micro;
using System.Reflection;
using Java.Lang;

namespace Lagou.Droid {
    [Application]
    public class Application : CaliburnApplication {
        private SimpleContainer container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) {


            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {

        }

        void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e) {
            //线程取消异常, 不好捕捉,在这里可以处理掉.
            //System.Threading.Tasks.TaskCanceledException: A task was canceled.
            e.Handled = true;
        }

        //private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
        //    e.SetObserved();
        //}

        public override void OnCreate() {
            base.OnCreate();

            Initialize();
            //this.ConnectXamlSpy();
        }

        //private void ConnectXamlSpy() {
        //    var service = FirstFloor.XamlSpy.Services.XamlSpyService.Current;
        //    service.Connect("192.168.0.124", 4530, "41169");
        //}

        protected override void Configure() {
            container = new SimpleContainer();
            container.Instance(container);
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[]
            {
                GetType().Assembly,
                typeof (Lagou.App).Assembly
            };
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }
    }
}