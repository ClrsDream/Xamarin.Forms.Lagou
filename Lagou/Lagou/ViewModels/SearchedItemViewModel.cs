using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class SearchedItemViewModel : BaseVM {
        public override string Title {
            get {
                return "Item";
            }
        }

        public SearchedItem Data {
            get; set;
        }

        public ICommand TapCmd { get; set; }

        private INavigationService NS;

        public SearchedItemViewModel(SearchedItem data, INavigationService ns) {
            this.Data = data;
            this.TapCmd = new Command(() => ShowDetail());
            this.NS = ns;
        }

        private void ShowDetail() {
            this.NS
                .For<JobDetailViewModel>()
                .WithParam(p => p.ID, this.Data.PositionId)
                .Navigate();

        }
    }
}
