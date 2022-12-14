using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekomanApp.Data;
using Xamarin.Forms;
using LekomanApp.Models;
using Xamarin.Forms.Xaml;

namespace LekomanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LekomanListPage : ContentPage
    {
        public LekomanListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LekomanItemDatabase database = await LekomanItemDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LekomanItemPage
            {
                BindingContext = new LekomanItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new LekomanItemPage
                {
                    BindingContext = e.SelectedItem as LekomanItem
                });
            }
        }


        async void Exit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }


    }
}