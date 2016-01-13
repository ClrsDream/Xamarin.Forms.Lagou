using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class TabViewModel : BaseVM {

        public BindableCollection<Screen> Datas {
            get; set;
        }

        public override string Title {
            get {
                return "Tab";
            }
        }

        public TabViewModel(SimpleContainer container) {
            this.Datas = new BindableCollection<Screen>() {
                    container.GetInstance<IndexViewModel>(),
                    container.GetInstance<SearchViewModel>(),
                    container.GetInstance<MyViewModel>(),
                    container.GetInstance<FavoritesViewModel>()
                };
        }
    }
}
