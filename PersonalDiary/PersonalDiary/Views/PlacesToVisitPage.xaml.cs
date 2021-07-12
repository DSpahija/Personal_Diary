using Newtonsoft.Json;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalDiary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacesToVisitPage : ContentPage
    {
        static string _ID;
        public static string country, city, isvisited, idplace;
        HttpClient client = new HttpClient();

        public PlacesToVisitPage(string id)
        {
            InitializeComponent();
            _ID = id;
            GetPlaces();
            listViewPlaces.ItemSelected += listViewPlaces_ItemSelected;
        }

        async private void ShowAddPlace(object obj, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddPlaceToVisitPage(_ID));
        }

        private void listViewPlaces_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //string opposite;
            if (e.SelectedItem is Place place)
            {
                country = place.Shteti;
                city = place.Qyteti;
                isvisited = place.isVisited;
                idplace = place.IDPlace.ToString();

                listViewPlaces.SelectedItem = null;
                Navigation.PushModalAsync(new ChangePlaceToVisit(_ID));
            }
            /*
               if (place.isVisited == "Yes")
                {
                    opposite = "No";
                }
                else
                {
                    opposite = "Yes";
                }

                var res = await DisplayAlert("Country: " + place.Shteti, "Change 'Visited?' from " + place.isVisited + " to " + opposite + "?", "Change it!", "Cancel");
                
                if (res == true)
                {
                    var response = await client.GetStringAsync
                    (returnJSON.GetURL() + "index.php?IDPlace=" +
                    place.IDPlace + "IDuser=" + _ID +
                    "&Shteti=" + place.Shteti +
                    "&Qyteti=" + place.Qyteti +
                    "&isVisited=" + opposite +
                    "&verifyUpdatePlace");
                    listViewPlaces.ItemsSource = null;
                    listViewPlaces.SelectedItem = null;
                    GetPlaces();
                }
                else
                {
                    listViewPlaces.ItemsSource = null;
                }
            }
             */
        }

        public async void SearchCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getPlaces");

            List<Place> stories = JsonConvert.DeserializeObject<List<Place>>(response);

            var titleSearch = searchBarCountry.Text;

            var suggestions = stories.Where(c => c.Shteti.ToLower().StartsWith(titleSearch)
                                        == titleSearch.ToLower().StartsWith(titleSearch));
            listViewPlaces.ItemsSource = suggestions;
            if (searchBarCountry.Text == "")
            {
                GetPlaces();
            }
        }
        public async void GetPlaces()
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getPlaces");
            var places = JsonConvert.DeserializeObject<List<Place>>(response);
                listViewPlaces.ItemsSource = places;
        }
    } 
}