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
using Android.Support.V4.Content;
using Android.Text.Style;
using Android.Text;
using Android.Graphics;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lagou.Droid.Renders {
    public class TabbedPageRender : TabbedPageRenderer {

        private Android.Views.View formViewPager = null;
        private TabLayout tabLayout = null;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e) {
            base.OnElementChanged(e);

            this.formViewPager = this.GetChildAt(0);
            this.tabLayout = (TabLayout)this.GetChildAt(1);

            this.UpdateTabIcons();
            //this.UpdateTabHeader();
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            base.OnLayout(changed, l, t, r, b);

            // update layout , let tab on the bottom of the page
            // formViewPager upon tab.
            var w = r - 1;
            var h = b - t;
            if (w > 0 && h > 0) {
                int ypos = Math.Min(h, Math.Max(this.tabLayout.MeasuredHeight, this.tabLayout.MinimumHeight));
                this.formViewPager.Layout(0, -ypos, r, b - ypos);
                this.tabLayout.Layout(l, h - ypos, r, b);
            }
        }


        //private void UpdateTabHeader() {
        //    if (this.tabLayout.TabCount != this.Element.Children.Count)
        //        return;

        //    for (var i = 0; i < this.Element.Children.Count; i++) {
        //        var page = this.Element.Children[i];
        //        if (string.IsNullOrEmpty(page.Icon)) {
        //            var glyph = (string)page.GetValue(AttachedFontIcon.GlyphProperty);
        //            if (!string.IsNullOrWhiteSpace(glyph)) {
        //                var font = (string)page.GetValue(AttachedFontIcon.FontFamilyProperty);
        //                var size = (int)(double)page.GetValue(AttachedFontIcon.FontSizeProperty);

        //                var tab = this.tabLayout.GetTabAt(i);

        //                var ss = new SpannableString(glyph);
        //                var fontSpan = new CustomTypefaceSpan(font);
        //                ss.SetSpan(fontSpan, 0, 2, SpanTypes.ExclusiveExclusive);
        //                tab.SetText(ss);
        //            }
        //        }
        //    }
        //}


        private void UpdateTabIcons1() {
            var tabLayout = this.tabLayout;
            if (tabLayout.TabCount != this.Element.Children.Count)
                return;

            var builder = new TextDrawableBuilder(this.Context);
            //builder.SetColor(Color.Accent.ToAndroid());

            var arr = this.Context.ObtainStyledAttributes(new int[] { Resource.Attribute.colorPrimary });
            var color = arr.GetColor(0, 0);
            builder.SetColor(color);

            for (int i = 0; i < this.Element.Children.Count; ++i) {
                var page = this.Element.Children[i];
                if (string.IsNullOrEmpty(page.Icon)) {
                    var glyph = (string)page.GetValue(AttachedFontIcon.GlyphProperty);
                    if (!string.IsNullOrWhiteSpace(glyph)) {
                        builder.SetText(glyph);

                        var font = (string)page.GetValue(AttachedFontIcon.FontFamilyProperty);
                        if (!string.IsNullOrWhiteSpace(font))
                            builder.SetFont(font.ToTypeface());

                        var size = (int)(double)page.GetValue(AttachedFontIcon.FontSizeProperty);
                        builder.SetSize(size);

                        tabLayout.GetTabAt(i)
                            .SetIcon(builder.Build());
                    }
                }
            }
        }

        private void UpdateTabIcons() {
            var tabLayout = this.tabLayout;
            if (tabLayout.TabCount != this.Element.Children.Count)
                return;

            for (int i = 0; i < this.Element.Children.Count; ++i) {
                var page = this.Element.Children[i];
                if (string.IsNullOrEmpty(page.Icon)) {
                    var glyph = (string)page.GetValue(AttachedFontIcon.GlyphProperty);
                    if (!string.IsNullOrWhiteSpace(glyph)) {

                        SpannableStringBuilder sb = new SpannableStringBuilder();
                        sb.Append(glyph);

                        var font = (string)page.GetValue(AttachedFontIcon.FontFamilyProperty);
                        if (!string.IsNullOrWhiteSpace(font)) {
                            var span = new CustomTypefaceSpan(font);
                            sb.SetSpan(span, 0, glyph.Length, SpanTypes.ExclusiveExclusive);
                        }

                        //var size = (int)(double)page.GetValue(AttachedFontIcon.FontSizeProperty);
                        sb.SetSpan(new AbsoluteSizeSpan(40), 0, glyph.Length, SpanTypes.ExclusiveExclusive);

                        sb.Append("\n");
                        sb.Append(page.Title);

                        //sb.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.Red), 3, 4, SpanTypes.ExclusiveExclusive);

                        //Must set app:tabTextAppearance textAllCaps to false.
                        // See : tabs.xml,
                        //if not do this, span will not work not work
                        tabLayout.GetTabAt(i).SetText(sb);

                        ////without specify color
                        //var v = new TextView(this.Context) {
                        //    TextAlignment = Android.Views.TextAlignment.Center
                        //};
                        //v.SetText(sb, TextView.BufferType.Spannable);

                        //tabLayout.GetTabAt(i).SetCustomView(v);
                    }
                }
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