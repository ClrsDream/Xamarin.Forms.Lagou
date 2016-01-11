using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;
using XF = Xamarin.Forms;
using System.Reflection;
using Lagou.UWP.Renders;

[assembly: ExportRenderer(typeof(XF.Button), typeof(ButtonRender))]
namespace Lagou.UWP.Renders {
    public class ButtonRender : ViewRenderer<XF.Button, CornerButton> {

        private bool fontApplied;

        protected override void OnElementChanged(ElementChangedEventArgs<XF.Button> e) {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
            if (this.Control == null) {
                var control = new CornerButton();
                control.Click -= OnButtonClick;
                control.Click += OnButtonClick;
                this.SetNativeControl(control);
            }
            this.UpdateContent();
            if (this.Element.BackgroundColor != XF.Color.Default)
                this.UpdateBackground();

            if (this.Element.TextColor != XF.Color.Default)
                this.UpdateTextColor();

            if (this.Element.BorderColor != XF.Color.Default)
                this.UpdateBorderColor();

            if (this.Element.BorderWidth != 0.0)
                this.UpdateBorderWidth();

            if (this.Element.BorderRadius != (int)Xamarin.Forms.Button.BorderRadiusProperty.DefaultValue)
                this.UpdateBorderRadius();

            this.UpdateFont();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == XF.Button.TextProperty.PropertyName ||
                e.PropertyName == XF.Button.ImageProperty.PropertyName) {
                this.UpdateContent();
            } else if (e.PropertyName == XF.VisualElement.BackgroundColorProperty.PropertyName)
                this.UpdateBackground();
            else if (e.PropertyName == XF.Button.TextColorProperty.PropertyName)
                this.UpdateTextColor();
            else if (e.PropertyName == XF.Button.FontProperty.PropertyName)
                this.UpdateFont();
            else if (e.PropertyName == XF.Button.BorderColorProperty.PropertyName)
                this.UpdateBorderColor();
            else if (e.PropertyName == XF.Button.BorderWidthProperty.PropertyName) {
                this.UpdateBorderWidth();
            } else {
                if (!(e.PropertyName == XF.Button.BorderRadiusProperty.PropertyName))
                    return;
                this.UpdateBorderRadius();
            }
        }

        private void UpdateBorderRadius() {
            this.Control.CornerRadius = new WX.CornerRadius(this.Element.BorderRadius);
        }

        private void OnButtonClick(object sender, WX.RoutedEventArgs e) {
            if (this.Element == null)
                return;

            //TODO Bug here
            var mth = this.Element.GetType().GetMethod("SendClicked", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mth != null) {
                mth.Invoke(this.Element, new object[] { });
            }
        }

        private void UpdateContent() {
            if (this.Element.Image != null) {
                var stackPanel = new WC.StackPanel() {
                    Orientation = WC.Orientation.Horizontal
                };
                var img = new WC.Image() {
                    Source = new WX.Media.Imaging.BitmapImage(new Uri("ms-appx:///" + this.Element.Image.File)),
                    Width = 30,
                    Height = 30,
                    Margin = new WX.Thickness(0.0, 0.0, 20.0, 0.0),
                };

                img.ImageOpened += ImageOpened;
                stackPanel.Children.Add(img);

                if (this.Element.Text != null)
                    stackPanel.Children.Add(new WC.TextBlock() {
                        Text = this.Element.Text
                    });
                this.Control.Content = (object)stackPanel;
            } else
                this.Control.Content = (object)this.Element.Text;
        }

        private void ImageOpened(object sender, WX.RoutedEventArgs e) {
            var mth = this.Element.GetType().GetMethod("NativeSizeChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mth != null)
                mth.Invoke(this.Element, new object[] { });
        }

        private void UpdateFont() {
            if (this.Control == null || this.Element == null || this.Element.Font == XF.Font.Default && !this.fontApplied)
                return;
            FontExtensions.ApplyFont(this.Control, this.Element.Font == XF.Font.Default ? XF.Font.SystemFontOfSize(XF.NamedSize.Medium) : this.Element.Font);
            this.fontApplied = true;
        }

        private void UpdateBackground() {
            this.Control.Background = this.Element.BackgroundColor != XF.Color.Default ? this.Element.BackgroundColor.ToBrush() : (WX.Media.Brush)(Windows.UI.Xaml.Application.Current.Resources)["ButtonBackgroundThemeBrush"];
        }

        private void UpdateTextColor() {
            this.Control.Foreground = this.Element.TextColor != XF.Color.Default ? this.Element.TextColor.ToBrush() : (WX.Media.Brush)((IDictionary<object, object>)Windows.UI.Xaml.Application.Current.Resources)[(object)"DefaultTextForegroundThemeBrush"];
        }

        private void UpdateBorderColor() {
            this.Control.BorderBrush = this.Element.BorderColor != XF.Color.Default ? this.Element.BorderColor.ToBrush() : (WX.Media.Brush)(Windows.UI.Xaml.Application.Current.Resources)["ButtonBorderThemeBrush"];
        }

        private void UpdateBorderWidth() {
            this.Control.BorderThickness = this.Element.BorderWidth == 0.0 ? new Windows.UI.Xaml.Thickness(3.0) : new Windows.UI.Xaml.Thickness(this.Element.BorderWidth);
        }

    }





    public class CornerButton : WC.Button {

        public static readonly WX.DependencyProperty CornerRadiusProperty =
            WX.DependencyProperty.Register("CornerRadius",
                typeof(WX.CornerRadius),
                typeof(CornerButton),
                new WX.PropertyMetadata(new WX.CornerRadius(0)));


        public WX.CornerRadius CornerRadius {
            get {
                return (WX.CornerRadius)this.GetValue(CornerRadiusProperty);
            }
            set {
                this.SetValue(CornerRadiusProperty, value);
            }
        }
    }
}
