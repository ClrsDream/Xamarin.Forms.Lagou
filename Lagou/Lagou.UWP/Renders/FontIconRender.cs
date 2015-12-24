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

            this.Control.FontFamily = this.Convert(this.Element.FontFamily);
            this.Control.Foreground = new SolidColorBrush(this.Element.Color.ToMediaColor());
            this.Control.Glyph = this.Element.Glyph;
            this.Control.FontSize = this.Element.FontSize;
        }

        private FontFamily Convert(string ff) {
            // ff like : FontAwesome.otf
            // Full Path must like : Assets/Fonts/FontAwesome.otf#FontAwesome
            // font name must same as font file name
            var fontName = Path.GetFileNameWithoutExtension(ff);
            // not have prefix "/", if have preifx "/", Path.Combin will return fail path.
            string path = string.Format("Assets/Fonts/{0}", ff);
            if (File.Exists(Path.Combine(AppContext.BaseDirectory, path))) {
                return new FontFamily(string.Format("/{0}#{1}", path, fontName));
            } else
                return FontFamily.XamlAutoFontFamily;
        }
    }
}
