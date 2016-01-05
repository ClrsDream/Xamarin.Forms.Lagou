using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou {
    public class AttachedFontIcon {

        public static readonly BindableProperty GlyphProperty =
            BindableProperty.CreateAttached<AttachedFontIcon, string>(
                o => (string)o.GetValue(GlyphProperty),
                string.Empty);

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.CreateAttached<AttachedFontIcon, string>(
                o => GetFontFamily(o),
                string.Empty);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.CreateAttached<AttachedFontIcon, double>(
                //Bug, If direct GetValue without Function, xaml value can't convert to target type.
                // Use Function will not have this issue.
                o => GetFontSize(o), //(double)o.GetValue(FontSizeProperty),
                12);

        public static readonly BindableProperty ColorProperty =
            BindableProperty.CreateAttached<AttachedFontIcon, Color>(
                o => GetColor(o),//(Color)o.GetValue(ColorProperty),
                Color.Default);

        public static Color GetColor(BindableObject bindable) {
            var color = (Color)bindable.GetValue(ColorProperty);
            return color;
        }

        public static double GetFontSize(BindableObject bindable) {
            return (double)bindable.GetValue(FontSizeProperty);
        }

        public static string GetFontFamily(BindableObject obj) {
            return obj.GetValue(FontFamilyProperty) as string;
        }
    }
}
