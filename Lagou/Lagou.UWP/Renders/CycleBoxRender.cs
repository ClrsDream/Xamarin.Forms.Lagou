using Lagou.Controls;
using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CycleBox), typeof(CycleBoxRender))]
namespace Lagou.UWP.Renders {
    public class CycleBoxRender : ViewRenderer<CycleBox, Windows.UI.Xaml.Controls.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<CycleBox> e) {
            base.OnElementChanged(e);

            this.SetNativeControl(new Windows.UI.Xaml.Controls.Border());
            this.UpdateControl();
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (e.PropertyName.Equals(CycleBox.RadiusProperty.PropertyName, StringComparison.OrdinalIgnoreCase)) {
                this.UpdateControl();
            }
        }

        protected override void UpdateNativeControl() {
            base.UpdateNativeControl();
            this.UpdateControl();
        }

        protected override void UpdateBackgroundColor() {
            if (Control != null) {
                Control.Background = this.Element.BackgroundColor.ToBrush();
            }
        }

        private void UpdateControl() {
            if (this.Control == null)
                return;

            var wh = this.Element.Radius * 2;
            this.Control.CornerRadius = new CornerRadius(this.Element.Radius);

            this.Element.WidthRequest = wh;
            this.Element.HeightRequest = wh;
            this.Element.Content.HorizontalOptions = LayoutOptions.Center;
            this.Element.Content.VerticalOptions = LayoutOptions.Center;

            //this.Control.Width = wh;
            //this.Control.Height = wh;
        }
    }
}
