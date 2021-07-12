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
	public partial class AddASong : ContentPage
    {
        HttpClient client = new HttpClient();
        private string _ID;
        public AddASong (string id)
		{
			InitializeComponent ();
            _ID = id;
        }

        async private void BackToMenu_Clicked(object obj, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }

        async private void addSongOfRemembrance_Clicked(object sender, EventArgs e)
        {
            if (songName.Text == null || singerName.Text == null)
            {
                await DisplayAlert("Empty", "Please write the singer/band and the song name", "Okay");
            }
            else
            {
                var res = await DisplayAlert("Adding a song", "Are you sure you want to add " + songName.Text + " by " + singerName.Text + "?", "Pretty sure!", "Nope");
                if (res == true)
                {
                    var response = await client.GetStringAsync
                    (returnJSON.GetURL() + "index.php?IDuser=" +
                    _ID + "&Singer=" +
                    singerName.Text + "&Song=" +
                    songName.Text + "&Moment=" +
                    momentNotes.Text + "&insertSong");

                    if (response != "0")
                    {
                        addSongOfRemembrance.IsEnabled = false;
                        await Navigation.PushModalAsync(new MenuPage(_ID));
                    }
                    else
                    {
                        await DisplayAlert("Sorry!", "For some reasons the song you entered wasn't added. Please try again.", "Try again");
                    }
                }
            }
        }
    }
}