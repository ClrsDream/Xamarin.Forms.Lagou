using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MDIViewModel : BaseVM {
        public Screen Master { get; set; }

        public Screen Detail { get; set; }

        public override string Title {
            get {
                return "MDI";
            }
        }

        public MDIViewModel(SimpleContainer container) {
            this.Master = container.GetInstance<SettingViewModel>();
            this.Detail = container.GetInstance<TabViewModel>();
            //var vm = container.GetInstance<JobDetailViewModel>();
            //vm.ID = 1178538;
            //this.Detail = vm;
        }
    }
}
