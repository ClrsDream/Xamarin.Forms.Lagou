using Lagou.Controls;
using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipRender))]
namespace Lagou.UWP.Renders {
    public class FlipRender : ViewRenderer<Flip, FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            if (e.NewElement == null)
                return;

            var ctl = new FlipView();
            foreach (var c in this.Element.Children) {
                var render = Platform.CreateRenderer(c);
                ctl.Items.Add(render);
            }
            this.SetNativeControl(ctl);
        }
    }
}
