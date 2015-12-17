using AngleSharp.Parser.Html;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Methods {
    public class PositionDetail : MethodBase<Position> {
        public override string Module {
            get {
                return string.Format("center/job_{0}.html?m=1", this.PositionID);
            }
        }

        public int PositionID { get; set; }

        protected override Position Execute(string result) {
            var parser = new HtmlParser();
            var job = parser.Parse<Position>(result);
            job.CompanyLogo = job.CompanyLogo.FixUrl("http://www.lagou.com");
            return job;
        }
    }
}
