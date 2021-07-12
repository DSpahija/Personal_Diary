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
	public partial class SongsOfRemembrancePage : ContentPage
	{
        HttpClient client = new HttpClient();
        static string _ID;
        public static string songName, singer, idSong;

        public SongsOfRemembrancePage(string id)
		{
			InitializeComponent ();
            _ID = id;
            GetSongs();
            listViewSongs.ItemSelected += ListViewSongs_ItemSelected;
        }

        private async void ListViewSongs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Songs song)
            {
                var res =  await DisplayAlert(song.Song + " by " + song.Singer, 
                    "Reminds you:\n" + song.Moment, "Delete", "Close");
                if (res == true)
                {
                    //index.php?IDuser=19&IDSong=3&deleteSong
                    var response = await client.GetStringAsync(returnJSON.GetURL() +
                        "index.php?IDuser=" + _ID +
                        "&IDSong=" + song.IDSong + "&deleteSong");
                    GetSongs();
                }
                listViewSongs.SelectedItem = null;
            }
        }
        async private void ShowAddASong(object obj, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddASong(_ID));
        }

        public async void GetSongs()
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getSongs");

            var songs = JsonConvert.DeserializeObject<List<Songs>>(response);
            listViewSongs.ItemsSource = songs;
        }

        public async void SearchSinger_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getSongs");

            List<Songs> songs = JsonConvert.DeserializeObject<List<Songs>>(response);

            var singerSearch = searchBarSinger.Text;

            var suggestions = songs.Where(c => (c.Singer.ToLower().StartsWith(singerSearch)
                                        == singerSearch.ToLower().StartsWith(singerSearch)) || 
                                        (c.Song.ToLower().StartsWith(singerSearch) 
                                        == singerSearch.ToLower().StartsWith(singerSearch)));
            listViewSongs.ItemsSource = suggestions;
            if (searchBarSinger.Text == "")
            {
                GetSongs();
            }
        }
    }
}