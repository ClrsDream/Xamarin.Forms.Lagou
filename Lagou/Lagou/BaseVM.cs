using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou {
    public abstract class BaseVM : Screen {

        public abstract string Title {
            get;
        }


        private bool isBusy = false;
        public bool IsBusy {
            get {
                return this.isBusy;
            }
            set {
                this.isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
                //Device.BeginInvokeOnMainThread(() => {
                //    this.NotifyOfPropertyChange(() => this.IsBusy);
                //});
            }
        }


        public BaseVM() {
            this.DisplayName = this.Title;
        }

    }
}
