using Caliburn.Micro.Xamarin.Forms;
using Lagou.API;
using Lagou.API.Methods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lagou.ViewModels {
    public class LoginViewModel : BaseVM, IDisposable {
        public override string Title {
            get {
                return "登陆";
            }
        }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Captcha { get; set; }

        public ImageSource CaptchSource { get; set; }

        public ICommand ReloadCaptcha { get; set; }

        public ICommand LoginCmd { get; set; }

        public bool IsBusy { get; set; }


        private INavigationService NS = null;

        public LoginViewModel(INavigationService ns) {
            this.NS = ns;

            this.ReloadCaptcha = new Command(async () => await this.LoadCaptcha());
            this.LoginCmd = new Command(async () => await this.Login());
        }


        private Stream Stm = null;

        protected async override void OnActivate() {
            await this.LoadCaptcha();
        }

        protected override void OnDeactivate(bool close) {
            base.OnDeactivate(close);
            if (this.Stm != null) {
                this.Stm.Dispose();
                this.CaptchSource = null;
            }
        }

        private async Task LoadCaptcha() {
            var mth = new GetCaptcha();
            var bytes = await ApiClient.Execute(mth);
            if (this.Stm != null)
                this.Stm.Dispose();
            this.Stm = new MemoryStream(bytes);
            this.CaptchSource = StreamImageSource.FromStream(() => this.Stm);
            this.NotifyOfPropertyChange(() => this.CaptchSource);
        }

        private async Task Login() {
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            var mth = new Login() {
                UserName = this.UserName,
                Password = this.Pwd,
                VerifyCode = this.Captcha
            };
            var flag = await ApiClient.Execute(mth);

            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            if (flag) {
                await Application.Current.MainPage.DisplayAlert("提示", "登陆成功", "OK");
                await this.NS.GoBackAsync();
            } else {
                await Application.Current.MainPage.DisplayAlert("提示", mth.Message, "OK");
            }
        }


        ~LoginViewModel() {
            Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.Stm != null)
                    this.Stm.Dispose();
            }
        }
    }
}
