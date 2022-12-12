using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekomanApp.Models;
using LekomanApp.Data;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace LekomanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LekomanItemPage : ContentPage
    {
        public LekomanItemPage()
        {
            InitializeComponent();

        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var lekomanItem = (LekomanItem)BindingContext;
            LekomanItemDatabase database = await LekomanItemDatabase.Instance;
            await database.SaveItemAsync(lekomanItem);
            await Navigation.PopAsync();

        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var lekomanItem = (LekomanItem)BindingContext;
            LekomanItemDatabase database = await LekomanItemDatabase.Instance;
            await database.DeleteItemAsync(lekomanItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Exit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }


    }


}