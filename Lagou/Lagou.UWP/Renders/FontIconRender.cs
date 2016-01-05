using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Xamarin.Forms;
using L = Lagou.Controls;
using W = Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using Windows.UI.Xaml.Media;
using Lagou.UWP.Renders;
using System.IO;

[assembly: ExportRenderer(typeof(L.FontIcon), typeof(FontIconRender))]
namespace Lagou.UWP.Renders {
    public class FontIconRender : ViewRenderer<L.FontIcon, W.FontIcon> {

        protected override void OnElementChanged(ElementChangedEventArgs<L.FontIcon> e) {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            this.SetNativeControl(new W.FontIcon());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(L.FontIcon.FontFamilyProperty.PropertyName) ||
                e.PropertyName.Equals(L.FontIcon.FontSizeProperty.PropertyName) ||
                    e.PropertyName.Equals(L.FontIcon.GlyphProperty.PropertyName) ||
                    e.PropertyName.Equals(L.FontIcon.ColorProperty.PropertyName)) {

                this.UpdateNativeControl();
            }
        }

        protected override void UpdateNativeControl() {
            base.UpdateNativeControl();

            if (this.Control == null)
                return;

            this.Control.FontFamily = this.Element.FontFamily.ToFontFamily();
            this.Control.Foreground = new SolidColorBrush(this.Element.Color.ToMediaColor());
            this.Control.Glyph = this.Element.Glyph;
            this.Control.FontSize = this.Element.FontSize;
        }

    }
}
