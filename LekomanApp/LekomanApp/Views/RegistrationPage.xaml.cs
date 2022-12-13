using LekomanApp.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LekomanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, EventArgs e)
        {

            if (!EntryUserEmail.Text.Contains("@"))
            {
                // If not, display an error message
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Błąd", "Wprowadź poprawny adres email.", "OK");
                });

                // Return early to avoid inserting the user into the database
                return;
            }

            if (string.IsNullOrEmpty(EntryUserPassword.Text))
            {
                // If not, display an error message
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Error", "Please enter a password.", "OK");
                });

                // Return early to avoid inserting the user into the database
                return;
            }

            // Check if the username has been entered
            if (string.IsNullOrEmpty(EntryUserName.Text))
            {
                // If not, display an error message
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Error", "Please enter a username.", "OK");
                });

                // Return early to avoid inserting the user into the database
                return;
            }


            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegUserTable>();

            var item = new RegUserTable()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text

            };



            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () => {

                var resultPositive = await this.DisplayAlert("Gratulacje", "Rejestracja przebiegła pomyślnie", "OK", "Anuluj");

                if (resultPositive)
                    await Navigation.PushAsync(new LoginPage());
            });
        }
    }
}