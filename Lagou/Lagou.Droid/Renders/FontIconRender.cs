using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lagou.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Lagou.Droid.Renders;

[assembly: ExportRenderer(typeof(FontIcon), typeof(FontIconRender))]
namespace Lagou.Droid.Renders {
    public class FontIconRender : ViewRenderer<FontIcon, TextView> {
        
        protected override void OnElementChanged(ElementChangedEventArgs<FontIcon> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var control = new TextView(this.Context);
                var vi = (LayoutInflater)this.Context.GetSystemService(Context.LayoutInflaterService);
                this.SetNativeControl(control);
                this.UpdateNativeControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(FontIcon.FontFamilyProperty.PropertyName) ||
                e.PropertyName.Equals(FontIcon.FontSizeProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.GlyphProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.ColorProperty.PropertyName)) {

                this.UpdateNativeControl();
            }
        }

        private void UpdateNativeControl() {
            var txt = this.Control;
            txt.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, this.Element.FontFamily);
            txt.Text = this.Element.Glyph;
            txt.SetTextColor(this.Element.Color.ToAndroid());
            txt.TextSize = (float)this.Element.FontSize;
        }
    }
}