using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MasterDetailPageRender))]
namespace Lagou.UWP.Renders {
    public class MasterDetailPageRender : MasterDetailPageRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<MasterDetailPage> e) {
            base.OnElementChanged(e);

            //this.Control.MasterTitleVisibility = Windows.UI.Xaml.Visibility.Visible;
            //this.Control.ToolbarBackground = new SolidColorBrush(Colors.Purple);

            this.Control.Master.SizeChanged += Master_SizeChanged;
        }

        private void Master_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e) {
            // if not do this , master page content not show
            this.Element.Master.Layout(new Rectangle(0, 0, e.NewSize.Width, e.NewSize.Height));
        }
    }
}
