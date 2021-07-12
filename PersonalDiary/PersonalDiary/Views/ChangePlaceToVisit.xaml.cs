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
	public partial class ChangePlaceToVisit : ContentPage
    {
        HttpClient client = new HttpClient();
        public static string _ID;
		public ChangePlaceToVisit(string id)
		{
			InitializeComponent ();
            _ID = id;
            GetPlace();
            labelClickFuntion();
          
        }

        private async void GetPlace()
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
            "index.php?IDuser=" + _ID +
            "&IDPlace=" + PlacesToVisitPage.idplace +
            "&get_places&Shteti=" + PlacesToVisitPage.country +
            "&Qyteti=" + PlacesToVisitPage.city);

            var places = JsonConvert.DeserializeObject<List<Place>>(response);
            notesPlace.Text = places[0].Notes;
            country.Text = PlacesToVisitPage.country;
            city.Text = PlacesToVisitPage.city;
            isVisited.Text = PlacesToVisitPage.isvisited;
        
        }

        void labelClickFuntion()
        {
            string opposite;
            
            isVisited.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (isVisited.Text == "No")
                    {
                
                        opposite = "Yes";
                    }
                    else
                    {    
                        opposite = "No";
                    }
                    var res = await DisplayAlert("" + country.Text, "Change 'Visited?' from " + isVisited.Text + " to " + opposite + "?", "Change it!", "Cancel");
                    if (res == true)
                    {
                        isVisited.Text = opposite;
                    }
                })
            });
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }

        private async void BtnDelete_Button(object sender, EventArgs e)
        {
            var res = await DisplayAlert("Delete","Are you sure you want to delete this place?", "Delete", "Close");
            if (res == true)
            {
                //index.php?IDuser=19&IDSong=3&deleteSong
                var response = await client.GetStringAsync(returnJSON.GetURL() +
                    "index.php?IDuser=" + _ID +
                    "&IDPlace=" + PlacesToVisitPage.idplace + "&deletePlace");
                await Navigation.PushModalAsync(new MenuPage(_ID));
            }
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            var response = await client.GetStringAsync
                     (returnJSON.GetURL() + "index.php?IDPlace=" +
                     PlacesToVisitPage.idplace + "IDuser=" + _ID +
                     "&Shteti=" + country.Text +
                     "&Qyteti=" + city.Text +
                     "&Notes=" + notesPlace.Text +
                     "&isVisited=" + isVisited.Text +
                     "&verifyUpdatePlace");

            if (response != "0")
            {
                //
            }
            else
            {
                await DisplayAlert("Sorry!", "For some reasons your place wasn't saved. Please try again.", "Try again");
            }

            deleteButton.IsEnabled = true;
            saveButton.IsEnabled = false;
        }
    }
}