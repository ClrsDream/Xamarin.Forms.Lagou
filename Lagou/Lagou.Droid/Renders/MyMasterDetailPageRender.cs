using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms;
using Lagou.Droid.Renders;
using System.Reflection;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MyMasterDetailPageRender))]
namespace Lagou.Droid.Renders {
    public class MyMasterDetailPageRender : MasterDetailPageRenderer {

        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement) {
            base.OnElementChanged(oldElement, newElement);

            //if not do this, in Android 5.1.1, well have a whrite space between navigation bar and tab bar.
            var fld = typeof(MasterDetailPageRenderer).GetField("detailLayout", BindingFlags.NonPublic | BindingFlags.Instance);
            var fldValue = fld.GetValue(this);
            var p = fld.FieldType.GetProperty("TopPadding", BindingFlags.Public | BindingFlags.Instance);
            p.SetValue(fldValue, 0);
        }
    }
}