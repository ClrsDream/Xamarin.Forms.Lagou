using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Xamarin.Forms;

namespace Lagou.UWP {
    public class TabbedPageHeaderIconConverter : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value != null) {
                var page = (Page)value;
                if (page != null) {
                    if (page.Icon == null) {
                        var glyph = (string)page.GetValue(AttachedFontIcon.GlyphProperty);
                        if (!string.IsNullOrWhiteSpace(glyph)) {
                            var familary = (string)page.GetValue(AttachedFontIcon.FontFamilyProperty);
                            var color = (Color)page.GetValue(AttachedFontIcon.ColorProperty);
                            var size = (double)page.GetValue(AttachedFontIcon.FontSizeProperty);
                            var icon = new Windows.UI.Xaml.Controls.FontIcon() {
                                Glyph = glyph,
                                FontFamily = familary.ToFontFamily(),
                                FontSize = size,
                                Foreground = color.ToBrush()
                            };
                            return icon;
                        }
                    } else {
                        var img = new Image();
                        img.Source = page.Icon;
                        return img;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
