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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Text;
using Android.Util;

namespace Lagou.Droid {

    /// <summary>
    /// https://github.com/bperin/FontAwesomeAndroid/blob/master/FontAwesomeFiles/DrawableAwesome.java
    /// </summary>
    public class TextDrawable : Drawable {


        private static float PADDING_RATIO = 0.88f;

        private Context context;
        private string text;
        private Paint paint;
        private int width;
        private int height;
        private float size;
        private Color? color;
        private bool antiAliased;
        private bool fakeBold;
        private float shadowRadius;
        private float shadowDx;
        private float shadowDy;
        private Color shadowColor;

        public TextDrawable(
                string text,
                Typeface font,
                int sizeDpi,
                Color? color,
                bool antiAliased,
                bool fakeBold,
                float shadowRadius,
                float shadowDx,
                float shadowDy,
                Color shadowColor,
                Context context
            ) : base() {

            this.context = context;
            this.text = text;
            this.size = DpToPx(sizeDpi) * PADDING_RATIO;
            this.height = DpToPx(sizeDpi);
            this.width = DpToPx(sizeDpi);
            this.color = color;
            this.antiAliased = antiAliased;
            this.fakeBold = fakeBold;
            this.shadowRadius = shadowRadius;
            this.shadowDx = shadowDx;
            this.shadowDy = shadowDy;
            this.shadowColor = shadowColor;
            this.paint = new Paint();

            this.paint.SetStyle(Paint.Style.Fill);
            this.paint.TextAlign = Paint.Align.Center;
            if (this.color.HasValue)
                this.paint.Color = this.color.Value;
            this.paint.TextSize = this.size;
            //var font = Typeface.CreateFromAsset(context.Assets, "fontawesome-webfont.ttf");
            this.paint.SetTypeface(font);
            this.paint.AntiAlias = this.antiAliased;
            this.paint.FakeBoldText = this.fakeBold;
            this.paint.SetShadowLayer(this.shadowRadius, this.shadowDx, this.shadowDy, this.shadowColor);
        }

        public override int IntrinsicHeight {
            get {
                return this.height;
            }
        }

        public override int IntrinsicWidth {
            get {
                return this.width;
            }
        }

        public override void Draw(Canvas canvas) {
            float xDiff = (width / 2.0f);
            canvas.DrawText(this.text, xDiff, size, paint);
        }

        public override int Opacity {
            get {
                return this.paint.Alpha;
            }
        }

        public override void SetAlpha(int alpha) {
            this.paint.Alpha = alpha;
        }

        public override void SetColorFilter(ColorFilter colorFilter) {
            this.paint.SetColorFilter(colorFilter);
        }


        private int DpToPx(int dp) {
            //var displayMetrics = context.Resources.DisplayMetrics;
            //int px = (int)Math.Round(dp * (displayMetrics.Xdpi / (int)DisplayMetricsDensity.Default));
            //return px;
            return (int)(dp * this.context.Resources.DisplayMetrics.Density);
        }
    }

    public class TextDrawableBuilder {
        private Context context;
        private string text;
        private int sizeDpi = 32;
        private Color? color = null;
        private bool antiAliased = true;
        private bool fakeBold = true;
        private float shadowRadius = 0;
        private float shadowDx = 0;
        private float shadowDy = 0;
        private Color shadowColor = Color.White;
        private Typeface font = Typeface.Default;


        public TextDrawableBuilder(Context context, string text = "") {
            this.context = context;
            this.text = text;
        }

        public void SetText(string text) {
            this.text = text;
        }

        public void SetFont(Typeface font) {
            this.font = font;
        }

        public void SetSize(int size) {
            this.sizeDpi = size;
        }

        public void SetColor(Color color) {
            this.color = color;
        }

        public void SetAntiAliased(bool antiAliased) {
            this.antiAliased = antiAliased;
        }

        public void SetFakeBold(bool fakeBold) {
            this.fakeBold = fakeBold;
        }

        public void SetShadow(float radius, float dx, float dy, Color color) {
            this.shadowRadius = radius;
            this.shadowDx = dx;
            this.shadowDy = dy;
            this.shadowColor = color;
        }

        public TextDrawable Build() {
            return new TextDrawable(text,
                font,
                sizeDpi,
                color,
                antiAliased,
                fakeBold,
                shadowRadius,
                shadowDx,
                shadowDy,
                shadowColor,
                context);
        }
    }
}
