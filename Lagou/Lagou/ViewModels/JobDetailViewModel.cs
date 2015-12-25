using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API.Entities;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class JobDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "职位详情";
            }
        }

        public Position Data { get; set; }

        public BindableCollection<EvaluationViewModel> Evaluations {
            get; set;
        } = new BindableCollection<EvaluationViewModel>();

        public bool HasEvaluations { get; set; }

        public bool NotHaveEvaluations { get; set; }

        public int ID { get; set; }

        public ICommand SeeAllCmd {
            get;
            set;
        }

        private INavigationService NS = null;

        public JobDetailViewModel(INavigationService ns) {
            this.SeeAllCmd = new Command(() => this.SeeAll());
            this.NS = ns;
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
            this.Evaluations.AddRange(evs.Select(e => new EvaluationViewModel(e)));
            this.NotifyOfPropertyChange(() => this.Evaluations);

            this.HasEvaluations = this.Evaluations.Count > 0;
            this.NotHaveEvaluations = !this.HasEvaluations;
            this.NotifyOfPropertyChange(() => this.HasEvaluations);
            this.NotifyOfPropertyChange(() => this.NotHaveEvaluations);
        }

        private void SeeAll() {
            this.NS.For<CompanyPositionsViewModel>()
                .WithParam(p => p.CompanyID, this.Data?.CompanyID)
                .WithParam(p => p.CompanyName, this.Data?.CompanyName)
                .WithParam(p => p.CompanyLogo, this.Data?.CompanyLogo)
                .Navigate();
        }
    }
}
