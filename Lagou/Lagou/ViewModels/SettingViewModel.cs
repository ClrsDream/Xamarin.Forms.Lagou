using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class SettingViewModel : BaseVM {
        public override string Title {
            get {
                return "设置";
            }
        }
    }
}
