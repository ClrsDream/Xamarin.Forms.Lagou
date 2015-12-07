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

        public BaseVM() {
            this.DisplayName = this.Title;
        }

    }
}
