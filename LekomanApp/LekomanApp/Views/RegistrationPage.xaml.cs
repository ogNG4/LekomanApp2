using LekomanApp.Models;
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
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Błąd", "Wprowadź poprawny adres email.(Brakuje znaku '@')", "OK");
                });

                
                return;
            }

            if (string.IsNullOrEmpty(EntryUserPassword.Text))
            {
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Błąd", "Wprowadż hasło.", "OK");
                });

                
                return;
            }

            
            if (string.IsNullOrEmpty(EntryUserName.Text))
            {
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Błąd", "Wprowadź nick.", "OK");
                });

                
                return;
            }

            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<LekomanItem>();

            var item = new LekomanItem()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text

            };

            // Sprawdź, czy istnieje już użytkownik o podanym adresie email lub nazwie użytkownika
            var existingUser = db.Table<LekomanItem>().Where(u => u.Email == EntryUserEmail.Text || u.UserName == EntryUserName.Text).FirstOrDefault();
            if (existingUser != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Błąd", "Użytkownik o podanym adresie email lub nazwie użytkownika już istnieje.", "OK");
                });
                return;
            }



            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () => {

                var resultPositive = await this.DisplayAlert("Gratulacje", "Rejestracja przebiegła pomyślnie", "OK", "Anuluj");

                if (resultPositive)
                    await Navigation.PushAsync(new LoginPage());
            });
        }
    }
}