using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Caliburn.Micro;
using Xamarin.Forms;
using Lagou.Droid.Renders;
using System.Reflection;

namespace Lagou.Droid {
    [Activity(Label = "Lagou", Theme = "@style/MyTheme", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            LoadApplication(new App(IoC.Get<SimpleContainer>()));

            //if not do this, MyMasterDetailPageRender will not effect.
            var m = typeof(FormsAppCompatActivity).GetMethod("RegisterHandlerForDefaultRenderer", BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(this, new object[] {
                typeof(MasterDetailPage),
                typeof(MyMasterDetailPageRender),
                typeof(MyMasterDetailPageRender)
            });

            m.Invoke(this, new object[] {
                typeof(TabbedPage),
                typeof(TabbedPageRender),
                typeof(TabbedPageRender)
            });
        }
    }
}

