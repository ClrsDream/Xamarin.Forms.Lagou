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
using Android.Graphics;
using Xamarin.Forms;

namespace Lagou.Droid {
    public static class Helper {

        public static Typeface ToTypeface(this string fontfamilary) {
            return Typeface.CreateFromAsset(Forms.Context.Assets, fontfamilary);
        }

    }
}