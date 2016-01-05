using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou {

    public class TabbedPageVMLocatorBinder {

        public static readonly BindableProperty VMsProperty =
            BindableProperty.CreateAttached<TabbedPageVMLocatorBinder, INotifyCollectionChanged>(
                o => GetVMsProperty(o),
                null,
                propertyChanged: VMSChanged);


        public static INotifyCollectionChanged GetVMsProperty(BindableObject obj) {
            if (null == (TabbedPage)obj)
                throw new Exception("TabbedPageVMLocatorBinder only used for TabbedPage");

            return (INotifyCollectionChanged)obj.GetValue(VMsProperty);
        }

        private static void VMSChanged(BindableObject bindable, INotifyCollectionChanged oldValue, INotifyCollectionChanged newValue) {
            var tab = (TabbedPage)bindable;
            var tmp = new Tmp(tab, newValue);
        }

        private class Tmp : IDisposable {

            private TabbedPage Tab = null;

            public Tmp(TabbedPage tab, INotifyCollectionChanged vms) {
                vms.CollectionChanged += Vms_CollectionChanged;
                this.Tab = tab;
                Vms_CollectionChanged(vms, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            private void Vms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
                var collection = (IList)sender;
                e.Apply(
                    //insert
                    (o, i, b) => {
                        Insert(o, i, b);
                    },
                    //remove
                    (o, i) => {
                        Remove(o, i);
                    },
                    //reset
                    () => {
                        Reset(collection);
                    });
            }

            private void Insert(object vm, int idx, bool b) {
                var page = LocatePage(vm);
                Tab.Children.Insert(idx, page);
            }

            private void Remove(object vm, int idx) {
                Tab.Children.RemoveAt(idx);
            }

            private void Reset(IList items) {
                Tab.Children.Clear();
                foreach (var o in items) {
                    Tab.Children.Add(LocatePage(o));
                }
            }

            private static Page LocatePage(object o) {
                var vmView = ViewLocator.LocateForModel(o, null, null);
                if (vmView == null)
                    throw new Exception("没有找到视图");
                ViewModelBinder.Bind(o, vmView, null);

                var activator = o as IActivate;
                if (activator != null)
                    activator.Activate();

                var page = (Page)vmView;
                if (page != null) {
                    page.Title = ((Screen)o)?.DisplayName;
                    return page;
                } else
                    return null;
            }

            public void Dispose() {
                
            }
        }
    }
}
