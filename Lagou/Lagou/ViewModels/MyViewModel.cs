using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : BaseVM {
        public override string Title {
            get {
                return "我的";
            }
        }

        public ICommand LoginCmd { get; set; }

        public ICommand ViewResumeCmd { get; set; }

        public MyViewModel(INavigationService ns) {
            this.LoginCmd = new Command(() => {
                ns.For<LoginViewModel>()
                .Navigate();
            });

            this.ViewResumeCmd = new Command(() => {
                ns.For<ResumeViewModel>()
                .Navigate();
            });
        }
    }
}
