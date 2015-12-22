using Caliburn.Micro;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {
    public class JobDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "职位详情";
            }
        }

        public Position Data { get; set; }

        public BindableCollection<Evaluation> Evaluations {
            get; set;
        } = new BindableCollection<Evaluation>();

        public bool HasEvaluations { get; set;}

        public int ID { get; set; }


        public JobDetailViewModel() {

        }

        protected async override void OnActivate() {
            base.OnActivate();

            var mth = new PositionDetail() {
                PositionID = this.ID
            };
            this.Data = await API.ApiClient.Execute(mth);
            this.NotifyOfPropertyChange(() => this.Data);

            var mth2 = new EvaluationList() {
                PositionID = this.ID
            };
            var evs = await API.ApiClient.Execute(mth2);
            this.Evaluations.AddRange(evs);
            this.NotifyOfPropertyChange(() => this.Evaluations);

            this.HasEvaluations = this.Evaluations.Count > 0;
            this.NotifyOfPropertyChange(() => this.HasEvaluations);
        }
    }
}
