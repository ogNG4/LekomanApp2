using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LekomanApp.Models;
using LekomanApp.Data;

namespace LekomanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

       

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(EntryUser.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();



            if (myquery != null)
            {
                App.Current.MainPage = new NavigationPage(new LekomanListPage());
            }

            else
            {
                Device.BeginInvokeOnMainThread(async () => {

                    var result = await this.DisplayAlert("Błąd!", "Błędny login lub haslo!", "OK", "Anuluj");

                    if (result)
                        await Navigation.PushAsync(new LoginPage());
                    else
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }
                });

            }
        }

    }
}