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
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Lagou.Controls;
using Lagou.Droid.Renders;

[assembly: ExportRenderer(typeof(Border), typeof(BorderRender))]
namespace Lagou.Droid.Renders {
    public class BorderRender : VisualElementRenderer<Border> {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            //HandlePropertyChanged (sender, e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        protected override void DispatchDraw(Canvas canvas) {
            if (Element.IsClippedToBorder) {
                canvas.Save(SaveFlags.Clip);
                BorderRendererVisual.SetClipPath(this, canvas);
                base.DispatchDraw(canvas);
                canvas.Restore();
            } else {
                base.DispatchDraw(canvas);
            }
        }
    }


    public static class BorderRendererVisual {
        public static void UpdateBackground(Border border, Android.Views.View view) {
            var strokeThickness = border.StrokeThickness;
            var context = view.Context;

            var corners = new float[] {
                    (float)border.CornerRadius.TopLeft,
                    (float)border.CornerRadius.TopLeft,

                    (float)border.CornerRadius.TopRight,
                    (float)border.CornerRadius.TopRight,

                    (float)border.CornerRadius.BottomRight,
                    (float)border.CornerRadius.BottomRight,

                    (float)border.CornerRadius.BottomLeft,
                    (float)border.CornerRadius.BottomLeft
                };

            // create stroke drawable
            GradientDrawable strokeDrawable = null;

            // if thickness exists, set stroke drawable stroke and radius
            if (strokeThickness.HorizontalThickness + strokeThickness.VerticalThickness > 0) {
                strokeDrawable = new GradientDrawable();
                strokeDrawable.SetColor(border.BackgroundColor.ToAndroid());

                // choose thickest margin
                // the content is padded so it will look like the margin is with the given thickness
                strokeDrawable.SetStroke((int)context.ToPixels(strokeThickness.Max()), border.Stroke.ToAndroid());
                //strokeDrawable.SetCornerRadius((float)border.CornerRadius.TopLeft);
                strokeDrawable.SetCornerRadii(corners);
            }

            // create background drawable
            var backgroundDrawable = new GradientDrawable();

            // set background drawable color based on Border's background color
            backgroundDrawable.SetColor(border.BackgroundColor.ToAndroid());
            //backgroundDrawable.SetCornerRadius((float)border.CornerRadius.TopLeft);
            backgroundDrawable.SetCornerRadii(corners);

            if (strokeDrawable != null) {
                // if stroke drawable exists, create a layer drawable containing both stroke and background drawables
                var ld = new LayerDrawable(new Drawable[] { strokeDrawable, backgroundDrawable });
                ld.SetLayerInset(1, (int)context.ToPixels(strokeThickness.Left), (int)context.ToPixels(strokeThickness.Top), (int)context.ToPixels(strokeThickness.Right), (int)context.ToPixels(strokeThickness.Bottom));
                //view.SetBackgroundDrawable(ld);
                view.Background = ld;
            } else {
                //view.SetBackgroundDrawable(backgroundDrawable);
                view.Background = backgroundDrawable;
            }

            // set Android.View's padding to take into account the stroke thickiness
            view.SetPadding(
                (int)context.ToPixels(strokeThickness.Left + border.Padding.Left),
                (int)context.ToPixels(strokeThickness.Top + border.Padding.Top),
                (int)context.ToPixels(strokeThickness.Right + border.Padding.Right),
                (int)context.ToPixels(strokeThickness.Bottom + border.Padding.Bottom));
        }

        static double Max(this Thickness t) {
            return new double[] {
                t.Left,
                t.Top,
                t.Right,
                t.Bottom
            }.Max();
        }

        static double Max(this CornerRadius t) {
            return new double[] { t.TopLeft, t.TopRight, t.BottomRight, t.BottomLeft }.Max();
        }

        public static void SetClipPath(this BorderRender br, Canvas canvas) {
            var clipPath = new Path();
            //float padding = br;// radius / 2;
            //float radius = (float)br.Element.CornerRadius.TopLeft - br.Context.ToPixels((float)br.Element.Padding.Max());// - padding / 2; // + MaxStrokeThickness());
            var corner = br.Element.CornerRadius;
            var tl = (float)corner.TopLeft;
            var tr = (float)corner.TopRight;
            var bbr = (float)corner.BottomRight;
            var bl = (float)corner.BottomLeft;

            //Array of 8 values, 4 pairs of [X,Y] radii
            float[] radius = new float[] {
                tl, tl, tr, tr, bbr, bbr, bl, bl
            };

            int w = (int)br.Width;
            int h = (int)br.Height;

            clipPath.AddRoundRect(new RectF(
                br.ViewGroup.PaddingLeft,
                br.ViewGroup.PaddingTop,
                w - br.ViewGroup.PaddingRight,
                h - br.ViewGroup.PaddingBottom),
                radius,
                Path.Direction.Cw);

            canvas.ClipPath(clipPath);
        }
    }
}