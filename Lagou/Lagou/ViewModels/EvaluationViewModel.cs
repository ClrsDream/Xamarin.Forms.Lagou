using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.ViewModels {
    public class EvaluationViewModel : BaseVM {
        public override string Title {
            get {
                return "";
            }
        }

        public Evaluation Data { get; set; }

        public EvaluationViewModel(Evaluation data) {
            this.Data = data;
        }
    }
}
