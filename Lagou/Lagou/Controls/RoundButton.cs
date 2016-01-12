using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.Controls {
    public class RoundButton : Button {

        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create<RoundButton, float>(
                o => o.Radius,
                0
                );

        public float Radius {
            get {
                return (float)this.GetValue(RadiusProperty);
            }
            private set {
                this.SetValue(RadiusProperty, value);
            }
        }
    }
}
