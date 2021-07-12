using Newtonsoft.Json;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalDiary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPlaceToVisitPage : ContentPage
    {
        HttpClient client = new HttpClient();
        private string _ID;
		public AddPlaceToVisitPage (string ID)
		{
			InitializeComponent ();
            _ID = ID;
            GetCountries();

            pickerPlace.Items.Add("Yes");
            pickerPlace.Items.Add("No");
            pickerPlace.SelectedIndex = 0;
            pickerCountry.Title = "Country";
		}

        async private void BackToMenu_Clicked(object obj, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }

        async private void AddPlace_Clicked(object obj, EventArgs e)
        {
            if (pickerCountry.SelectedIndex == -1)
            {
                await DisplayAlert("Add a country!", "Please select a country from the list!", "Okay");
            }
            else
            {
                var response = await client.GetStringAsync
                    (returnJSON.GetURL() + "index.php?IDuser=" +
                    _ID + "&Shteti=" +
                    pickerCountry.SelectedItem + "&Qyteti=" +
                    addCityName.Text + "&Notes=" +
                    notesPlace.Text + "&isVisited=" +
                    pickerPlace.SelectedItem + "&insertPlace");

                if (response != "0")
                {
                    addPlaceToVisit.IsEnabled = false;
                    await Navigation.PushModalAsync(new MenuPage(_ID));
                }
                else
                {
                    await DisplayAlert("Sorry!", "For some reasons place to visit wasn't added. Please try again.", "Try again");
                }
            }
        }
        public async void GetCountries()
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?getcountries");
            
            var countries = JsonConvert.DeserializeObject<List<Shteti>>(response);
            for (int i = 0; i < countries.Count; i++)
            {
                pickerCountry.Items.Add(countries[i].Name);
            }
        }
    }
}