using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Lagou.UWP {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Splash {

        private SplashScreen SP = null;
        private Rect splashImageRect;
        private double scaleFactor;


        public Splash(SplashScreen sp) {
            this.InitializeComponent();
            this.SP = sp;
            this.SP.Dismissed += SP_Dismissed;
            this.splashImageRect = sp.ImageLocation;
            this.scaleFactor = (double)DisplayInformation.GetForCurrentView().ResolutionScale / 100;

            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);
        }

        private async void SP_Dismissed(SplashScreen sender, object args) {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () => {
                ((App)App.Current).ShowMainPage();
            });
        }


        void PositionImage() {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.Left);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Top);
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")) {
                extendedSplashImage.Height = splashImageRect.Height / scaleFactor;
                extendedSplashImage.Width = splashImageRect.Width / scaleFactor;
            } else {
                extendedSplashImage.Height = splashImageRect.Height;
                extendedSplashImage.Width = splashImageRect.Width;
            }
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e) {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (this.SP != null) {
                // Update the coordinates of the splash screen image.
                this.splashImageRect = this.SP.ImageLocation;
                this.PositionImage();
            }
        }
    }
}
