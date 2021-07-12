using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalDiary.Models;
using Newtonsoft.Json.Linq;

namespace PersonalDiary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReadAStoryPage : ContentPage
	{
        HttpClient client = new HttpClient();
        
        string _ID;

        public ReadAStoryPage(string id)
        {
            InitializeComponent();
            _ID = id;
            storyTitle.Text = ReadYourPastPage.title;
            storyDate.Text = ReadYourPastPage.date;
            GetStory();
        }
        private async void GetStory()
        {
             var response = await client.GetStringAsync(returnJSON.GetURL() +
             "index.php?IDuser=" + _ID +
             "&Title=" + ReadYourPastPage.title +
             "&Date=" + ReadYourPastPage.date);
            
            var stories = JsonConvert.DeserializeObject<List<Story>>(response);
            storyContent.Text = stories[0].StoryContent;
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }
        
        private async void BtnDelete_Button(object sender, EventArgs e)
        {
            var res = await DisplayAlert("Delete", "Are you sure you want to delete this story?", "Im sure", "Nope");
            
            if(res == true)
            {

                var responseDelete = await client.GetStringAsync(returnJSON.GetURL() +
                    "index.php?IDuser=" + _ID +
                    "&IDStory=" + ReadYourPastPage.idStory + "&deleteStory");

                await Navigation.PushModalAsync(new MenuPage(_ID));
            }
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            var response = await client.GetStringAsync
                (returnJSON.GetURL() + "index.php?IDuser=" +
                _ID + "&IDStory=" + ReadYourPastPage.idStory +
                "&StoryContent=" + storyContent.Text +
                "&verifyUpdate");

            if (response != "0")
            {
                await Navigation.PushModalAsync(new MenuPage(_ID));
            }
            else
            {
                await DisplayAlert("Sorry!", "For some reasons your story wasn't saved. Please try again.", "Try again");
            }
        }
        
        private void onEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Editor)sender;

            if (entry.Text.Length > 20)
            {
                string entryText = entry.Text;
                entry.TextChanged -= onEntry_TextChanged;
                entry.Text = e.OldTextValue;
                entry.TextChanged += onEntry_TextChanged;
            }
        }
    }
}