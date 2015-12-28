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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Lagou.Droid.Renders;
using Android.Support.Design.Widget;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lagou.Droid.Renders {
    public class TabbedPageRender : TabbedPageRenderer {

        private Android.Views.View formViewPager = null;
        private TabLayout tab = null;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e) {
            base.OnElementChanged(e);

            this.formViewPager = this.GetChildAt(0);
            this.tab = (TabLayout)this.GetChildAt(1);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            base.OnLayout(changed, l, t, r, b);

            var w = r - 1;
            var h = b - t;
            if (w > 0 && h > 0) {
                int ypos = Math.Min(h, Math.Max(this.tab.MeasuredHeight, this.tab.MinimumHeight));
                this.formViewPager.Layout(0, -ypos, r, b - ypos);
                this.tab.Layout(l, h - ypos, r, b);
            }
        }

        internal static class MeasureSpecFactory {
            public static int MakeMeasureSpec(int size, MeasureSpecMode mode) {
                return size + (int)mode;
            }

            //public static int GetSize(int measureSpec) {
            //    return measureSpec & 1073741823;
            //}
        }
    }
}