using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

//[assembly:ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lagou.UWP.Renders {
    public class TabbedPageRender : TabbedPageRenderer {

        public TabbedPageRender() {
            this.ElementChanged += TabbedPageRender_ElementChanged;
        }

        private void TabbedPageRender_ElementChanged(object sender, VisualElementChangedEventArgs e) {
            if(this.Control != null) {
                //var o = this.Control.FindChildControl<PivotHeaderPanel>("StaticHeader");
                //this.Control.Background = new SolidColorBrush(Colors.Green);
                var s = this.Control.Style;
            }
        }
    }
}
