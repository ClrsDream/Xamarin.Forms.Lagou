using Caliburn.Micro;
using Lagou.API;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {
    public class IndexViewModel : BaseVM {

        public override string Title {
            get {
                return "职位列表";
            }
        }

        private SimpleContainer Container = null;

        public BindableCollection<SearchedItemViewModel> Datas { get; set; }

        public IndexViewModel(SimpleContainer container) {
            this.Datas = new BindableCollection<SearchedItemViewModel>();
            this.Container = container;
        }

        protected async override void OnActivate() {
            base.OnActivate();

            var method = new Search();
            var datas = await ApiClient.Execute(method);
            this.Datas.AddRange(datas.Select(d =>
                new SearchedItemViewModel(d)
            ));
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}
