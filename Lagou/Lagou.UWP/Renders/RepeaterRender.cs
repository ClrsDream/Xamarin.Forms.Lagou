using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using L = Lagou.Controls;


namespace Lagou.UWP.Renders {
    public class RepeaterRender : ViewRenderer<L.Repeater, ItemsControl> {

        protected override void OnElementChanged(ElementChangedEventArgs<L.Repeater> e) {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var control = new ItemsControl() {
                Template = new ControlTemplate()
            };
        }

    }
}
