using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.Controls {

    [ContentProperty("Children")]
    public class Flip : View {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<Flip, IEnumerable>(p => p.ItemsSource, null, propertyChanged: ItemsSourceChanged);
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create<Flip, ScrollOrientation>(p => p.Orientation, ScrollOrientation.Horizontal);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<Flip, DataTemplate>(p => p.ItemTemplate, null);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public ScrollOrientation Orientation {
            get {
                return (ScrollOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        public IEnumerable<View> Children {
            get;
            private set;
        } = new List<View>();

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            flip.CalcChild();
        }

        private void CalcChild() {
            var children = new List<View>();

            if (this.ItemsSource == null || this.ItemTemplate == null)
                return;

            foreach (var o in this.ItemsSource) {
                var view = (View)this.ItemTemplate.CreateContent();
                view.BindingContext = o;
                children.Add(view);
                view.Parent = this;///Must , 如果不设置 Parent , 如果是 StackLayout , 不会显示
            }

            this.Children = children;
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);

            foreach (var c in this.Children) {
                if (c.Parent == null)
                    c.Parent = this;
                c.Layout(new Rectangle(0, 0, width, height));
            }
        }
    }
}
