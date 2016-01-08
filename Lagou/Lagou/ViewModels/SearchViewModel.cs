using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class SearchViewModel : BaseVM {
        public override string Title {
            get {
                return "职位搜索";
            }
        }

        public string City { get; set; }

        public string Key { get; set; }

        public CitySelectorViewModel CityVM { get; private set; }

        public bool IsCitySelectorShowing { get; set; }

        public ICommand ShowCitySelectorCmd { get; set; }

        public ICommand SearchCmd { get; set; }

        public ICommand LoadMoreCmd { get; set; }

        public BindableCollection<SearchedItemViewModel> Datas { get; set; }

        public bool InRefresh { get; set; }

        private int Page = 1;

        private SimpleContainer Container = null;
        private INavigationService NS = null;


        public SearchViewModel(INavigationService ns, SimpleContainer container) {
            this.NS = ns;
            this.Container = container;

            this.CityVM = container.GetInstance<CitySelectorViewModel>();
            this.CityVM.OnCancel += CityVM_OnCancel;
            this.CityVM.OnChoiced += CityVM_OnChoiced;

            this.ShowCitySelectorCmd = new Command(() => {
                this.ToggleCitySelector(true);
            });

            this.SearchCmd = new Command(() => {
                this.Search();
            });

            this.LoadMoreCmd = new Command(async () => {
                await this.LoadData(false);
            });

            this.Datas = new BindableCollection<SearchedItemViewModel>();
        }

        private void CityVM_OnChoiced(object sender, CitySelectorViewModel.ChoicedCityEventArgs e) {
            this.City = e.City;
            this.NotifyOfPropertyChange(() => this.City);
            this.ToggleCitySelector(false);
        }

        private void CityVM_OnCancel(object sender, EventArgs e) {
            this.ToggleCitySelector(false);
        }

        public void ToggleCitySelector(bool show) {
            this.IsCitySelectorShowing = show;
            this.NotifyOfPropertyChange(() => this.IsCitySelectorShowing);
        }

        protected async override void OnActivate() {
            base.OnActivate();

            var geo = DependencyService.Get<IGeolocatorService>(DependencyFetchTarget.NewInstance);
            var city = await geo.GetCityNameAsync();
            this.City = city.TrimEnd(new char[] { '市' });
            this.NotifyOfPropertyChange(() => this.City);
        }

        public async void Search() {
            await this.LoadData(true);
        }

        private async Task LoadData(bool reload = false) {
            this.InRefresh = true;
            Device.BeginInvokeOnMainThread(() => {
                this.NotifyOfPropertyChange(() => this.InRefresh);
            });

            var method = new Search() {
                Page = reload ? 1 : this.Page,
                City = this.City,
                Key = this.Key
            };
            var datas = await ApiClient.Execute(method);
            if (!method.HasError && datas.Count() > 0) {

                if (reload) {
                    this.Datas.Clear();
                }

                //this.Datas.AddRange(datas.Select(d =>
                //    new SearchedItemViewModel(d, this.NS)
                //));

                foreach (var d in datas) {
                    this.Datas.Add(new SearchedItemViewModel(d, this.NS));
                }

                //this.NotifyOfPropertyChange(() => this.Datas);
                this.Page++;
            }

            this.InRefresh = false;
            Device.BeginInvokeOnMainThread(() => {
                this.NotifyOfPropertyChange(() => this.InRefresh);
            });
        }
    }
}
