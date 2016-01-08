using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou {
    public class CmdBinder {

        public static readonly BindableProperty EventProperty =
            BindableProperty.CreateAttached<CmdBinder, string>(
                o => GetEvent(o),
                string.Empty
                );

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached<CmdBinder, ICommand>(
                o => GetCommand(o),
                null,
                propertyChanged: CommandChanged);

        private static void CommandChanged(BindableObject bindable, ICommand oldValue, ICommand newValue) {
            Observable.FromEventPattern(bindable, GetEvent(bindable))
                .Subscribe(e => {
                    var cmd = GetCommand(bindable);
                    if (cmd != null && cmd.CanExecute(e))
                        cmd.Execute(e);
                });
        }

        private static ICommand GetCommand(BindableObject o) {
            return (ICommand)o.GetValue(CommandProperty);
        }

        private static string GetEvent(BindableObject o) {
            return (string)o.GetValue(EventProperty);
        }
    }
}
