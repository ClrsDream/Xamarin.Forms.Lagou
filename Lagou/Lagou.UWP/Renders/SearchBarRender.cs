using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;


[assembly: ExportRenderer(typeof(SearchBar), typeof(SearchBarRender))]
namespace Lagou.UWP.Renders {
    public class SearchBarRender : SearchBarRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e) {
            base.OnElementChanged(e);

            if(this.Control != null) {
                this.Control.QuerySubmitted += Control_QuerySubmitted;
            }

        }

        private void Control_QuerySubmitted(Windows.UI.Xaml.Controls.AutoSuggestBox sender, Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs args) {
            InputPane.GetForCurrentView().TryHide();
        }
    }
}
