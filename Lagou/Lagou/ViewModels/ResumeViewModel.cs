using Lagou.API;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ResumeViewModel : BaseVM {
        public override string Title {
            get {
                return "我的简历";
            }
        }

        public HtmlWebViewSource Data { get; set; }

        protected override void OnActivate() {
            base.OnActivate();
            this.LoadResume();
        }

        private async void LoadResume() {
            this.IsBusy = true;

            var mth = new ResumePreview();
            var html = await ApiClient.Execute(mth);

            this.Data = new HtmlWebViewSource() {
                Html = html
            };

            this.NotifyOfPropertyChange(() => this.Data);

            this.IsBusy = false;
        }
    }
}
