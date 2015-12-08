using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;

//[assembly: ExportRenderer(typeof(Grid), typeof(GridRender))]
namespace Lagou.UWP.Renders {
    public class GridRender : ViewRenderer<Xamarin.Forms.Grid, WC.Grid> {

        protected override void OnElementChanged(ElementChangedEventArgs<Grid> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var g = new WC.Grid();
                var rs = e.NewElement.RowDefinitions.Select(r => new WC.RowDefinition() {
                    Height = new WX.GridLength(r.Height.Value)
                });
                foreach (var r in rs)
                    g.RowDefinitions.Add(r);

                var cs = e.NewElement.ColumnDefinitions.Select(c => new WC.ColumnDefinition() {
                    Width = new WX.GridLength(c.Width.Value)
                });
                foreach (var c in cs) {
                    g.ColumnDefinitions.Add(c);
                }

                this.SetNativeControl(g);
            }
        }

    }
}
