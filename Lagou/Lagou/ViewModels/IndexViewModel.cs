using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class IndexViewModel : BaseVM {

        public override string Title {
            get {
                return "职位列表";
            }
        }

        private SimpleContainer Container = null;
        private INavigationService NS = null;

        public BindableCollection<SearchedItemViewModel> Datas { get; set; }

        public bool InRefresh { get; set; }

        public ICommand ReloadCmd { get; set; }

        public ICommand LoadMoreCmd { get; set; }

        private int Page = 1;

        public IndexViewModel(SimpleContainer container, INavigationService ns) {
            this.Datas = new BindableCollection<SearchedItemViewModel>();
            this.Container = container;
            this.NS = ns;

            this.ReloadCmd = new Command(async () => {
                await this.LoadData(true);
            });

            this.LoadMoreCmd = new Command(async () => {
                await this.LoadData(false);
            });
        }

        protected async override void OnActivate() {
            base.OnActivate();
            await this.LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reload">是否是重新加载</param>
        /// <returns></returns>
        private async Task LoadData(bool reload = false) {
            this.InRefresh = true;
            Device.BeginInvokeOnMainThread(() => {
                this.NotifyOfPropertyChange(() => this.InRefresh);
            });

            var method = new Search() {
                Page = reload ? 1 : this.Page
            };
            var datas = await ApiClient.Execute(method);
            if (!method.HasError && datas.Count() > 0) {

                if (reload) {
                    this.Datas.Clear();
                }

                this.Datas.AddRange(datas.Select(d =>
                    new SearchedItemViewModel(d, this.NS)
                ));
                this.NotifyOfPropertyChange(() => this.Datas);
                this.Page++;
            }

            this.InRefresh = false;
            Device.BeginInvokeOnMainThread(() => {
                this.NotifyOfPropertyChange(() => this.InRefresh);
            });
        }
    }
}
