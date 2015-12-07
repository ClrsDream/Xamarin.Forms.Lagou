using Lagou.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRender))]
namespace Lagou.UWP.Renders {
    public class NavigationPageRender : NavigationPageRenderer {

        public NavigationPageRender() : base() {
            //this.Element.BarBackgroundColor = Color.Red;
            this.ElementChanged += NavigationPageRender_ElementChanged;
        }

        private void NavigationPageRender_ElementChanged(object sender, VisualElementChangedEventArgs e) {
            if (e.NewElement != null) {
                this.Element.BarBackgroundColor = Color.FromRgb(0x00, 0xbc, 0xd4);// new Color(0x00, 0xBC, 0xD4, 0xff);//#00BCD4
                this.Element.Pushed -= Element_Pushed;
                this.Element.Pushed += Element_Pushed;

                this.Element.PoppedToRoot += Element_PoppedToRoot;
                this.Element.Popped += Element_Popped;
            }
        }

        private void Element_Popped(object sender, NavigationEventArgs e) {
            if(this.Element.Navigation.NavigationStack.Count == 1) {
                var root = this.ContainerElement.GetRootPage();
                root.FindChildControl<ToggleButton>("backToggle").Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                root.FindChildControl<ToggleButton>("splitViewToggle").Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void Element_PoppedToRoot(object sender, NavigationEventArgs e) {
            var root = this.ContainerElement.GetRootPage();
            root.FindChildControl<ToggleButton>("backToggle").Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            root.FindChildControl<ToggleButton>("splitViewToggle").Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void Element_Pushed(object sender, NavigationEventArgs e) {
            //backToggle
            var root = this.ContainerElement.GetRootPage();
            root.FindChildControl<ToggleButton>("backToggle").Visibility = Windows.UI.Xaml.Visibility.Visible;
            root.FindChildControl<ToggleButton>("splitViewToggle").Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
