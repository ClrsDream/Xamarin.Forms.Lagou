using Caliburn.Micro;
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

        public CitySelectorViewModel CityVM { get; private set; }

        public bool IsCitySelectorShowing { get; set; }

        public ICommand ShowCitySelectorCmd {
            get; set;
        }

        public SearchViewModel(SimpleContainer container) {
            this.CityVM = container.GetInstance<CitySelectorViewModel>();
            this.ShowCitySelectorCmd = new Command(() => {
                this.IsCitySelectorShowing = !this.IsCitySelectorShowing;
                this.NotifyOfPropertyChange(() => this.IsCitySelectorShowing);
            });
        }

        protected async override void OnActivate() {
            base.OnActivate();

            var geo = DependencyService.Get<IGeolocatorService>(DependencyFetchTarget.NewInstance);
            var city = await geo.GetCityNameAsync();
            this.City = city;
            this.NotifyOfPropertyChange(() => this.City);
        }
    }
}
