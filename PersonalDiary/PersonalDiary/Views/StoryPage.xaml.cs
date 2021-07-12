using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PersonalDiary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoryPage : ContentPage
    {
        string _ID;
        public StoryPage(string id)
		{
			InitializeComponent ();
            _ID = id;
            storyTitle.Text = WriteYourStoryPage.title;
            storyDate.Text = WriteYourStoryPage.date;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync
                (returnJSON.GetURL() + "index.php?IDuser=" + 
                _ID + "&Title=" +
                storyTitle.Text + "&Date=" +
                storyDate.Text + "&StoryContent=" +
                storyContent.Text + "&InsertStory");

            if (response != "0")
            {
                SaveButton.IsEnabled = false;
                await Navigation.PushModalAsync(new MenuPage(_ID));
            }
            else
            {
                await DisplayAlert("Sorry!", "For some reasons your story wasn't saved. Please try again.", "Try again");
            }
        }
    }
}