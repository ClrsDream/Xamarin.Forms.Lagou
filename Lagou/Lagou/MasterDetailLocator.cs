using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou {
    public class MasterDetailLocator {

        public static readonly BindableProperty MasterProperty =
                    BindableProperty.CreateAttached<MasterDetailLocator, Screen>(
                        bindable => GetMaster(bindable),
                        null,
                        BindingMode.OneWay,
                        null,
                        MasterChanged,
                        null,
                        null);

        public static readonly BindableProperty DetailProperty =
            BindableProperty.CreateAttached<MasterDetailLocator, Screen>(
                p => GetDetail(p),
                null,
                BindingMode.OneWay,
                propertyChanged: DetailChanged
                );

        private static Page GetPage(Screen vm) {
            var vmView = ViewLocator.LocateForModel(vm, null, null);
            if (vmView == null)
                throw new Exception("没有找到视图");
            ViewModelBinder.Bind(vm, vmView, null);

            var activator = vm as IActivate;
            if (activator != null)
                activator.Activate();

            return (Page)vmView;
        }

        private static void MasterChanged(BindableObject bindable, object oldValue, object newValue) {
            var mdp = bindable as MasterDetailPage;
            if (mdp != null) {
                if (newValue == null)
                    return;

                var vm = (Screen)newValue;
                var page = GetPage((Screen)newValue);
                page.Title = vm.DisplayName;

                mdp.Master = page;
                //mdp.Master.Title = vm.DisplayName;
            }
        }

        private static void DetailChanged(BindableObject bindable, object oldValue, object newValue) {
            var mdp = bindable as MasterDetailPage;
            if (mdp != null) {
                if (newValue == null)
                    return;

                var vm = (Screen)newValue;
                var page = GetPage((Screen)newValue);
                page.Title = vm.DisplayName;
                mdp.Detail = page;
            }
        }

        private static Screen GetMaster(BindableObject bindable) {
            return null;
        }

        private static Screen GetDetail(BindableObject bindable) {
            return null;
        }
    }
}
