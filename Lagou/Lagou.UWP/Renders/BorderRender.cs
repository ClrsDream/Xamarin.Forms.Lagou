using Lagou.Controls;
using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;
using WM = Windows.UI.Xaml.Media;
using Windows.Foundation;

[assembly: ExportRendererAttribute(typeof(Lagou.Controls.Border), typeof(BorderRender))]
namespace Lagou.UWP.Renders {
    public class BorderRender : ViewRenderer<Border, WC.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            SetNativeControl(new WC.Border());
            UpdateControl();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Content") {
                PackChild();
            } else if (e.PropertyName == Border.StrokeProperty.PropertyName ||
                       e.PropertyName == Border.StrokeThicknessProperty.PropertyName ||
                       e.PropertyName == Border.CornerRadiusProperty.PropertyName ||
                       e.PropertyName == Border.PaddingProperty.PropertyName) {
                UpdateControl();
            }
        }

        // the base class is setting the background to the renderer when Control is null
        protected override void UpdateBackgroundColor() {
            if (Control != null) {
                Control.Background = (this.Element.BackgroundColor != Xamarin.Forms.Color.Default ? this.Element.BackgroundColor.ToBrush() : base.Background);
            }
        }

        private void PackChild() {
            if (Element.Content == null) {
                return;
            }
            if (Element.Content.GetOrCreateRenderer() == null) {
                Platform.SetRenderer(Element.Content, Platform.GetRenderer(Element.Content));
            }
            var render = Platform.GetRenderer(Element.Content) as WX.UIElement;
            Control.Child = render;
        }

        private void UpdateControl() {
            Control.CornerRadius = new WX.CornerRadius(Element.CornerRadius);
            Control.BorderBrush = Element.Stroke.ToBrush();
            Control.BorderThickness = Element.StrokeThickness.ToWinPhone();
            Control.Padding = Element.Padding.ToWinPhone();

            if (Element.IsClippedToBorder) {
                // var size = Control.Child.RenderSize;
                Control.Child.Clip = new WM.RectangleGeometry() {
                    Rect = new Rect(0, 0, 400, 400), // just testing with some values for now
                };
            }
        }
    }
}
