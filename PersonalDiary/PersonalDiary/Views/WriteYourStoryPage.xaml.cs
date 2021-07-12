using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WriteYourStoryPage : ContentPage
    {
        public static string title;
        public static string date;
        string _ID;

        public WriteYourStoryPage (string id)
		{
			InitializeComponent ();
            _ID = id;
		}

        private async void continueToTheStory_Clicked(object sender, EventArgs e)
        {
            if (TitleOfTheStory.Text == null)
            {
                await DisplayAlert("Title missing", "Please write the title!", "Cancel");
            }
            else
            {
                DateTime dt = new DateTime();
                dt = DateOfTheStory.Date;
                title = TitleOfTheStory.Text;
                date = dt.ToString("dd/MM/yyyy");
                await Navigation.PushModalAsync(new StoryPage(_ID));
            }
        }

        private void onEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

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