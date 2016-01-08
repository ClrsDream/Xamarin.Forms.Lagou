using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using System.Windows.Input;

namespace Lagou {

    public class PickerBinder {

        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.CreateAttached<PickerBinder, IEnumerable>(
                o => GetItemsSource(o),
                Enumerable.Empty<object>(),
                propertyChanged: ItemsSourceChanged
                );

        public static readonly BindableProperty DisplayPathProperty =
            BindableProperty.CreateAttached<PickerBinder, string>(
                o => GetDisplayPath(o),
                null
                );

        public static readonly BindableProperty ValuePathProperty =
            BindableProperty.CreateAttached<PickerBinder, string>(
                o => GetValuePath(o),
                null);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached<PickerBinder, ICommand>(
                o => GetCommand(o),
                null,
                propertyChanged:CmdChanged);

        private static IEnumerable GetItemsSource(BindableObject o) {
            return (IEnumerable)o.GetValue(ItemsSourceProperty);
        }

        private static string GetDisplayPath(BindableObject o) {
            return (string)o.GetValue(DisplayPathProperty);
        }

        private static string GetValuePath(BindableObject o) {
            return (string)o.GetValue(DisplayPathProperty);
        }

        private static ICommand GetCommand(BindableObject o) {
            return (ICommand)o.GetValue(CommandProperty);
        }

        private static void ItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue) {
            var picker = (Picker)bindable;
            if (picker == null)
                throw new Exception("only support Picker");

            var selected = picker.SelectedIndex;

            var type = newValue.GetType().GenericTypeArguments[0].GetTypeInfo();
            var dp = (string)bindable.GetValue(DisplayPathProperty);
            PropertyInfo p = null;
            if (!string.IsNullOrWhiteSpace(dp)) {
                p = type.GetDeclaredProperty(dp);
            }
            foreach (var o in newValue) {
                object value = null;
                if (p != null)
                    value = p.GetValue(o);
                else
                    value = o;

                if (value != null)
                    picker.Items.Add(value.ToString());
            }

            picker.SelectedIndex = selected;
        }

        private static void CmdChanged(BindableObject bindable, ICommand oldValue, ICommand newValue) {
            var picker = (Picker)bindable;
            picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }

        private static void Picker_SelectedIndexChanged(object sender, EventArgs e) {
            var picker = (Picker)sender;
            var source = (IEnumerable<Object>)picker.GetValue(ItemsSourceProperty);
            //var vp = (string)picker.GetValue(ValuePathProperty);
            var o = source.ElementAt(picker.SelectedIndex);
            var cmd = GetCommand(picker);
            if (cmd.CanExecute(o))
                cmd.Execute(o);
        }
    }
}
