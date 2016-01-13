using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class CitySelectorViewModel : BaseVM {

        public event EventHandler OnCancel = null;
        public event EventHandler<ChoicedCityEventArgs> OnChoiced = null;

        public override string Title {
            get {
                return "";
            }
        }

        public static BindableCollection<ListViewGroup<City>> Datas {
            get;
            set;
        }

        public ICommand CancelCmd {
            get; set;
        }

        public City ChoicedCity { get; set; }

        static CitySelectorViewModel() {
            var datas = Cities.Items.GroupBy(i => new string(i.PY[0], 1))
                .Select(g => new ListViewGroup<City>(g.ToList().OrderBy(i => i.Name)) {
                    Title = g.Key.ToUpper(),
                    ShortTitle = g.Key.ToUpper()
                });

            Datas = new BindableCollection<ListViewGroup<City>>(datas);
        }

        public CitySelectorViewModel() {
            this.CancelCmd = new Command(() => {
                if (this.OnCancel != null)
                    this.OnCancel.Invoke(this, EventArgs.Empty);
            });
        }

        public void Choice() {
            if (this.OnChoiced != null)
                this.OnChoiced.Invoke(this, new ChoicedCityEventArgs(this.ChoicedCity.Name));
        }

        public class ChoicedCityEventArgs : EventArgs {
            public string City { get; private set; }

            public ChoicedCityEventArgs(string city) {
                this.City = city;
            }
        }
    }
}
