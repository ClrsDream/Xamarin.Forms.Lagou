using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class SearchViewModel : BaseVM {
        public override string Title {
            get {
                return "职位搜索";
            }
        }

        public string City { get; set; }

        protected async override void OnActivate() {
            base.OnActivate();

            var geo = DependencyService.Get<IGeolocatorService>(DependencyFetchTarget.NewInstance);
            var city = await geo.GetCityNameAsync();
            this.City = city;
            this.NotifyOfPropertyChange(() => this.City);
        }
    }
}
