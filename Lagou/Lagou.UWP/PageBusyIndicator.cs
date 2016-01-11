using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF = Xamarin.Forms;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.ViewManagement;

namespace Lagou.UWP {
    public class PageBusyIndicator {

        private WC.ProgressRing busyIndicator;

        private int Width = 50, Height = 50;

        public static void Init() {
            var indicator = new PageBusyIndicator();
            indicator.Subscribe();
        }

        private PageBusyIndicator() { }

        private void Subscribe() {
            XF.MessagingCenter.Subscribe<XF.Page, bool>(
                (object)this,
                "Xamarin.BusySet",
                (sender, enabled) => {
                    this.ShowBusyIndicator(enabled);
                });
        }

        private void ShowBusyIndicator(bool flag) {
            var busy = this.GetBusyIndicator();
            busy.Visibility = flag ? WX.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            busy.IsActive = flag;
        }

        private WC.ProgressRing GetBusyIndicator() {
            if (this.busyIndicator == null) {
                this.busyIndicator = new WC.ProgressRing() {
                    IsActive = false,
                    Visibility = WX.Visibility.Collapsed,
                    //VerticalAlignment = WX.VerticalAlignment.Bottom,
                    //HorizontalAlignment = WX.HorizontalAlignment.Center,
                    Width = this.Width,
                    Height = this.Height
                };
                WC.Canvas.SetZIndex(this.busyIndicator, 1);
                var c = WX.Window.Current.Content.FindChildControl<WC.Canvas>("");
                if (c != null) {
                    var x = (c.ActualWidth - this.Width) / 2;
                    var y = (c.ActualHeight - this.Height) / 2;

                    WC.Canvas.SetTop(this.busyIndicator, y);
                    WC.Canvas.SetLeft(this.busyIndicator, x);

                    c.Children.Add(this.busyIndicator);
                }
            }
            return this.busyIndicator;
        }
    }

}
