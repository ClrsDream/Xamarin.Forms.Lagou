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
using WX = Windows.UI.Xaml;
using WC = Windows.UI.Xaml.Controls;
using System.Reflection;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.Foundation;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lagou.UWP.Renders {
    public class TabbedPageRender : VisualElementRenderer<TabbedPage, WC.Pivot> {

        public TabbedPageRender() {
            this.AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var p = new WC.Pivot();
                this.SetNativeControl(p);
                p.Loaded += P_Loaded;
                p.Unloaded += P_Unloaded;
                p.SelectionChanged += P_SelectionChanged;
                p.DataContext = this.Element;
                p.ItemTemplate = (WX.DataTemplate)WX.Application.Current.Resources["TabbedPageItemDataTemplate"];
                p.HeaderTemplate = (WX.DataTemplate)WX.Application.Current.Resources["TabbedPageHeaderDataTemplate"];

                this.UpdateCurrentPage();
                this.OnPagesChanged(this.Element.Children, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                this.UpdateBackgroundColor();

                ((INotifyCollectionChanged)this.Element.Children).CollectionChanged += this.OnPagesChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("CurrentPage")) {
                this.UpdateCurrentPage();
            } else if (e.PropertyName.Equals(Xamarin.Forms.Page.TitleProperty.PropertyName)) {
                //this.UpdateTitle();
            }
        }

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize) {
            return base.ArrangeOverride(finalSize);
        }

        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize) {
            return base.MeasureOverride(availableSize);
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
            return base.GetDesiredSize(widthConstraint, heightConstraint);
        }

        //public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
        //    var size = new Windows.Foundation.Size(widthConstraint, heightConstraint);
        //    double width = this.Control.Width;
        //    double height = this.Control.Height;
        //    //this.Control.Height = double.NaN;
        //    //this.Control.Width = double.NaN;
        //    this.Control.Measure(size);
        //    var finallSize = new Xamarin.Forms.Size(Math.Ceiling(this.Control.DesiredSize.Width), Math.Ceiling(this.Control.DesiredSize.Height));
        //    this.Control.Width = width;
        //    this.Control.Height = height;
        //    return new SizeRequest(finallSize);
        //}

        private void UpdateCurrentPage() {
            var currentPage = this.Element.CurrentPage;
            if (currentPage == null) {
                return;
            }
            this.Control.SelectedItem = currentPage;
        }

        private void P_SelectionChanged(object sender, WC.SelectionChangedEventArgs e) {
            if (this.Element != null) {
                var currPage = (Xamarin.Forms.Page)e.AddedItems?[0];
                this.Element.CurrentPage = currPage;
            }
        }

        private void P_Unloaded(object sender, WX.RoutedEventArgs e) {
            if (this.Element != null) {
                var mhd = this.Element.GetType().GetMethod("SendDisappearing", BindingFlags.NonPublic | BindingFlags.Instance);
                mhd.Invoke(this.Element, new object[] { });
            }
        }

        private void P_Loaded(object sender, WX.RoutedEventArgs e) {
            if (this.Element != null) {
                var mhd = this.Element.GetType().GetMethod("SendAppearing", BindingFlags.NonPublic | BindingFlags.Instance);
                mhd.Invoke(this.Element, new object[] { });
            }
        }

        private void OnPagesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (this.Element != null && this.Control != null) {
                e.Apply(this.Element.Children, this.Control.Items);
            }
            if (this.Control != null) {
                this.Control.UpdateLayout();
            }
        }
    }
}
