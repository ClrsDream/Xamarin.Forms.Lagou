using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lagou.UWP.Renders {

    //[TemplatePart(Name = "ContentPresenter", Type = typeof(ContentControl))]
    public class ClipBorder : ContentControl {

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ClipBorder), new PropertyMetadata(new CornerRadius()));

        public CornerRadius CornerRadius {
            get {
                return (CornerRadius)this.GetValue(CornerRadiusProperty);
            }
            set {
                this.SetValue(CornerRadiusProperty, value);
            }
        }

        public ClipBorder() {
            this.DefaultStyleKey = typeof(ClipBorder);
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }
    }
}
