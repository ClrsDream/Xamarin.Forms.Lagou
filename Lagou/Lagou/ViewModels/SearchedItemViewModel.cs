using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SearchedItemViewModel(SearchedItem data) {
            this.Data = data;
        }
    }
}
