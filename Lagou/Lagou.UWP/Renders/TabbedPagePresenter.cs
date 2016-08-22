using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using System.Reflection;

namespace Lagou.UWP.Renders {
    internal class TabbedPagePresenter : Windows.UI.Xaml.Controls.ContentPresenter {
        public TabbedPagePresenter() {
            this.SizeChanged += TabbedPagePresenter_SizeChanged;
        }

        private void TabbedPagePresenter_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e) {
            if (this.ActualWidth <= 0.0 || this.ActualHeight <= 0.0)
                return;

            var page = (Xamarin.Forms.Page)((Element)this.DataContext).Parent;
            //page.ContainerArea = new Rectangle(0.0, 0.0, this.ActualWidth, this.ActualHeight);
            var rect = new Rectangle(0.0, 0.0, this.ActualWidth, this.ActualHeight);

            var p = page.GetType().GetProperty("ContainerArea", BindingFlags.Instance | BindingFlags.NonPublic);
            p.SetValue(page, rect);
            page.ForceLayout();
        }
    }
}
