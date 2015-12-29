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

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lagou.UWP.Renders {
    public class TabbedPageRender : TabbedPageRenderer {

        public TabbedPageRender() {
            this.ElementChanged += TabbedPageRender_ElementChanged;
        }

        private void TabbedPageRender_ElementChanged(object sender, VisualElementChangedEventArgs e) {
            if(this.Control != null) {
                this.Control.Style = (Windows.UI.Xaml.Style)Windows.UI.Xaml.Application.Current.Resources["TabbedPageStyle2"];
            }
        }
    }
}
