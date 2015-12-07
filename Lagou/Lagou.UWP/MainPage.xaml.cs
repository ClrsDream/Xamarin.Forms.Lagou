using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Lagou.UWP {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage {
        public MainPage() {
            this.InitializeComponent();

            this.LoadApplication(new Lagou.App(IoC.Get<WinRTContainer>()));
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Color.FromArgb(0xff, 0x00, 0x97, 0xa7);//#0097A7
                statusBar.BackgroundOpacity = 1;
            }

            this.Loaded += MainPage_Loaded;
            DisplayInformation.GetForCurrentView().OrientationChanged += MainPage_OrientationChanged;
        }

        private async void MainPage_OrientationChanged(DisplayInformation sender, object args) {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                var flag = sender.CurrentOrientation != DisplayOrientations.Portrait;
                StatusBar statusBar = StatusBar.GetForCurrentView();
                if (flag)
                    await statusBar.HideAsync();
                else
                    await statusBar.ShowAsync();
            }
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var btn = this.FindChildControl<ToggleButton>("splitViewToggle");
            btn.Click += Btn_Click;

            var backBtn = this.FindChildControl<ToggleButton>("backToggle");
            backBtn.Click += BackBtn_Click;
        }

        private async void BackBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            await Lagou.App.Current.MainPage.Navigation.PopAsync();
        }

        private void Btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var spv = this.FindChildControl<SplitView>("masterDetailSplitView");
            spv.IsPaneOpen = !spv.IsPaneOpen;
        }
    }
}
