using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {
    /// <summary>
    /// 公司职位列表
    /// </summary>
    public class CompanyPositionsViewModel : BaseVM {
        public override string Title {
            get {
                return "职位列表";
            }
        }

        /// <summary>
        /// 职位类型列表
        /// </summary>
        public List<string> PositionTypes { get; set; } = Enum.GetNames(typeof(PositionTypes)).ToList();

        public string SelectedPositionType { get; set; }

        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }


        public BindableCollection<PositionBrief> Datas { get; set; } = new BindableCollection<PositionBrief>();


        private int Page = 1;

        public CompanyPositionsViewModel() {

        }

        protected async override void OnActivate() {
            await Task.Delay(500).ContinueWith(t => this.SetPosType(this.PositionTypes.First()));
        }

        private async Task LoadPosByType() {
            var method = new PositionList() {
                CompanyID = this.CompanyID,
                PositionType = (PositionTypes)Enum.Parse(typeof(PositionTypes), this.SelectedPositionType),
                Page = this.Page
            };
            var datas = await API.ApiClient.Execute(method);
            if (!method.HasError && datas.Count() > 0) {
                this.Page++;
                this.Datas.AddRange(datas);
            }
        }

        private async Task SetPosType(string type) {
            this.SelectedPositionType = type;
            this.Page = 1;
            this.Datas.Clear();
            await this.LoadPosByType();
        }
    }
}
