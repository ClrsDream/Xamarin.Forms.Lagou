using Lagou.API.Attributes;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class Search : MethodBase<SearchedItem> {
        public override string Module {
            get {
                return "custom/search.json";
            }
        }

        [Param("pageNo")]
        public int Page {
            get; set;
        } = 1;

        protected override SearchedItem Execute(string result) {
            return base.Execute(result);
        }
    }
}
