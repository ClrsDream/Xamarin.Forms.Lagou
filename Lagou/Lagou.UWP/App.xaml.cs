using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Lagou.UWP {
    public sealed partial class App {
        private WinRTContainer _container;
        private IEventAggregator _eventAggregator;

        public App() {
            InitializeComponent();
            this.UnhandledException += App_UnhandledException;

            //var spy = FirstFloor.XamlSpy.Services.ServiceProvider.GetService<ClientService>();
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e) {

        }

        protected override void Configure() {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }


        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[]
            {
                GetType().GetTypeInfo().Assembly,
                typeof (Lagou.App).GetTypeInfo().Assembly
            };
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args) {
            Xamarin.Forms.Forms.Init(args);

            this.DisplayRootView<MainPage>();
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e) {

        }

        protected override object GetInstance(Type service, string key) {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }
    }
}
