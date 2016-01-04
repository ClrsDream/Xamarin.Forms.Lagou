using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP {

    internal class NotifyCollectionChangedEventArgsEx : NotifyCollectionChangedEventArgs {
        public int Count { get; private set; }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action)
          : base(action) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, IList changedItems)
          : base(action, changedItems) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, IList newItems, IList oldItems)
          : base(action, newItems, oldItems) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
          : base(action, newItems, oldItems, startingIndex) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
          : base(action, changedItems, startingIndex) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
          : base(action, changedItems, index, oldIndex) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, object changedItem)
          : base(action, changedItem) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, object changedItem, int index)
          : base(action, changedItem, index) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
          : base(action, changedItem, index, oldIndex) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, object newItem, object oldItem)
          : base(action, newItem, oldItem) {
            this.Count = count;
        }

        public NotifyCollectionChangedEventArgsEx(int count, NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
          : base(action, newItem, oldItem, index) {
            this.Count = count;
        }
    }






    internal static class NotifyCollectionChangedEventArgsExtensions {
        public static void Apply<TFrom>(this NotifyCollectionChangedEventArgs self, IList<TFrom> from, IList<object> to) {
            int num = (int)NotifyCollectionChangedEventArgsExtensions.Apply(self, (Action<object, int, bool>)((o, i, b) => to.Insert(i, o)), (Action<object, int>)((o, i) => to.RemoveAt(i)), (Action)(() =>
            {
                to.Clear();
                for (int index = 0; index < ((ICollection<TFrom>)from).Count; ++index)
                    to.Add((object)from[index]);
            }));
        }

        public static NotifyCollectionChangedAction Apply(this NotifyCollectionChangedEventArgs self, Action<object, int, bool> insert, Action<object, int> removeAt, Action reset) {
            if (self == null)
                throw new ArgumentNullException("self");
            if (reset == null)
                throw new ArgumentNullException("reset");
            if (insert == null)
                throw new ArgumentNullException("insert");
            if (removeAt == null)
                throw new ArgumentNullException("removeAt");
            switch (self.Action) {
                case NotifyCollectionChangedAction.Add:
                    if (self.NewStartingIndex >= 0) {
                        for (int index = 0; index < self.NewItems.Count; ++index)
                            insert(self.NewItems[index], index + self.NewStartingIndex, true);
                        break;
                    }
                    goto case 4;
                case NotifyCollectionChangedAction.Remove:
                    if (self.OldStartingIndex >= 0) {
                        for (int index = 0; index < self.OldItems.Count; ++index)
                            removeAt(self.OldItems[index], self.OldStartingIndex);
                        break;
                    }
                    goto case 4;
                case NotifyCollectionChangedAction.Replace:
                    if (self.OldStartingIndex >= 0) {
                        for (int index = 0; index < self.OldItems.Count; ++index) {
                            removeAt(self.OldItems[index], index + self.OldStartingIndex);
                            insert(self.OldItems[index], index + self.OldStartingIndex, true);
                        }
                        break;
                    }
                    goto case 4;
                case NotifyCollectionChangedAction.Move:
                    if (self.NewStartingIndex >= 0 && self.OldStartingIndex >= 0) {
                        for (int index = 0; index < self.OldItems.Count; ++index)
                            removeAt(self.OldItems[index], self.OldStartingIndex);
                        int newStartingIndex = self.NewStartingIndex;
                        if (self.OldStartingIndex < self.NewStartingIndex)
                            newStartingIndex -= self.OldItems.Count - 1;
                        for (int index = 0; index < self.OldItems.Count; ++index)
                            insert(self.OldItems[index], newStartingIndex + index, false);
                        break;
                    }
                    goto case 4;
                case NotifyCollectionChangedAction.Reset:
                    reset();
                    return NotifyCollectionChangedAction.Reset;
            }
            return self.Action;
        }

        public static NotifyCollectionChangedEventArgsEx WithCount(this NotifyCollectionChangedEventArgs e, int count) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    return new NotifyCollectionChangedEventArgsEx(count, NotifyCollectionChangedAction.Add, e.NewItems, e.NewStartingIndex);
                case NotifyCollectionChangedAction.Remove:
                    return new NotifyCollectionChangedEventArgsEx(count, NotifyCollectionChangedAction.Remove, e.OldItems, e.OldStartingIndex);
                case NotifyCollectionChangedAction.Replace:
                    return new NotifyCollectionChangedEventArgsEx(count, NotifyCollectionChangedAction.Replace, e.NewItems, e.OldItems, e.OldStartingIndex);
                case NotifyCollectionChangedAction.Move:
                    return new NotifyCollectionChangedEventArgsEx(count, NotifyCollectionChangedAction.Move, e.OldItems, e.NewStartingIndex, e.OldStartingIndex);
                default:
                    return new NotifyCollectionChangedEventArgsEx(count, NotifyCollectionChangedAction.Reset);
            }
        }
    }
}
