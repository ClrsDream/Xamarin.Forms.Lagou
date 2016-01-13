using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Lagou.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lagou.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class FavoritesViewModel : BaseVM {

        public static readonly string AddFavorite = "AddFavorite";

        private static readonly string Key = "Favorites";

        public override string Title {
            get {
                return "本地收藏";
            }
        }

        private List<Position> Favorites = new List<Position>();


        public BindableCollection<SearchedItemViewModel> Datas { get; set; }


        private INavigationService NS = null;

        public FavoritesViewModel(SimpleContainer container, INavigationService ns) {
            this.NS = ns;

            this.Favorites = PropertiesHelper.Get<List<Position>>(Key) ?? new List<Position>();
            this.Datas = new BindableCollection<SearchedItemViewModel>();
            var datas = this.Favorites.Select(f => {
                var d = this.Convert(f);
                return new SearchedItemViewModel(d, ns);
            });

            this.Datas.AddRange(datas);
            this.RegistMessaging();
        }

        private PositionBrief Convert(Position f) {
            return new PositionBrief() {
                City = f.WorkAddress,
                CompanyId = f.CompanyID,
                CompanyLogo = f.CompanyLogo,
                CompanyName = f.CompanyName,
                CreateTime = "N/A",
                PositionFirstType = "N/A",
                PositionId = f.PositionID,
                PositionName = f.JobTitle,
                Salary = f.Salary
            };
        }

        private void RegistMessaging() {
            MessagingCenter.Subscribe<JobDetailViewModel, Position>(this,
                AddFavorite,
                async (sender, arg) => {
                    if (!this.Favorites.Any(f => f.PositionID == arg.PositionID)) {
                        var d = this.Convert(arg);
                        this.Datas.Add(new SearchedItemViewModel(d, this.NS));
                        await this.AddToFavorite(arg);

                        await Application.Current.MainPage.DisplayAlert("提示", "已收藏到本地", "OK");
                    } else
                        await Application.Current.MainPage.DisplayAlert("提示", "该职位已收藏", "OK");
                });
        }

        private async Task AddToFavorite(Position data) {
            this.Favorites.Add(data);
            await this.SaveFavorite();
        }

        private async Task SaveFavorite() {
            PropertiesHelper.SetObject(Key, this.Favorites);
            await PropertiesHelper.Save();
        }
    }
}
