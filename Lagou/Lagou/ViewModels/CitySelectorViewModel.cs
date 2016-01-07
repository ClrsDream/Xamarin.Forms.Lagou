using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {
    public class CitySelectorViewModel : BaseVM {
        public override string Title {
            get {
                return "";
            }
        }

        public static BindableCollection<ListViewGroup<City>> Datas {
            get;
            set;
        }

        static CitySelectorViewModel() {
            var datas = Cities.Items.GroupBy(i => new string(i.PY[0], 1))
                .Select(g => new ListViewGroup<City>(g.ToList()) {
                    Title = g.Key,
                    ShortTitle = g.Key
                });

            Datas = new BindableCollection<ListViewGroup<City>>(datas);
        }
    }
}
