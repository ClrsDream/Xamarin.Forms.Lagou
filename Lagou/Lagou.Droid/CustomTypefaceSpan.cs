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
using Android.Text.Style;
using Android.Graphics;
using Android.Text;

namespace Lagou.Droid {
    /// <summary>
    /// http://stackoverflow.com/questions/4819049/how-can-i-use-typefacespan-or-stylespan-with-a-custom-typeface
    /// </summary>
    //public class CustomTypefaceSpan : MetricAffectingSpan {
    //    private Typeface typeface;

    //    public CustomTypefaceSpan(Typeface typeface) {
    //        this.typeface = typeface;
    //    }

    //    public override void UpdateDrawState(TextPaint tp) {
    //        apply(tp);
    //    }

    //    public override void UpdateMeasureState(TextPaint p) {
    //        apply(p);
    //    }
    //    private void apply(Paint paint) {
    //        Typeface oldTypeface = paint.Typeface;
    //        var oldStyle = oldTypeface != null ? oldTypeface.Style : TypefaceStyle.Normal;
    //        var fakeStyle = oldStyle & ~typeface.Style;

    //        paint.FakeBoldText = (fakeStyle & TypefaceStyle.Bold) == TypefaceStyle.Bold;

    //        if ((fakeStyle & TypefaceStyle.Italic) == TypefaceStyle.Italic) {
    //            paint.TextSkewX = -0.25f;
    //        }

    //        paint.SetTypeface(typeface);
    //    }
    //}

    public class CustomTypefaceSpan : TypefaceSpan {
        private Typeface newType;

        public CustomTypefaceSpan(string family) : base(family) {
            newType = family.ToTypeface();
        }

        public override void UpdateDrawState(TextPaint ds) {
            apply(ds, newType);
        }

        public override void UpdateMeasureState(TextPaint paint) {
            apply(paint, newType);
        }

        private static void apply(Paint paint, Typeface tf) {
            TypefaceStyle oldStyle;
            var old = paint.Typeface;
            if (old == null) {
                oldStyle = 0;
            } else {
                oldStyle = old.Style;
            }

            var fake = oldStyle & ~tf.Style;
            if ((fake & TypefaceStyle.Bold) == TypefaceStyle.Bold) {
                paint.FakeBoldText = true;
            }

            if ((fake & TypefaceStyle.Italic) == TypefaceStyle.Italic) {
                paint.TextSkewX = -0.25f;
            }

            paint.SetTypeface(tf);
            paint.Flags = paint.Flags | PaintFlags.SubpixelText;
        }
    }
}