using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalDiary.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        string _ID;
		public MenuPage (string id)
		{
			InitializeComponent();
            _ID = id;            
        }
        private async void writeYourStory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new WriteYourStoryPage(_ID));
        }

        private async void readYourPast_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ReadYourPastPage(_ID));
        }

        private async void placesToVisit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PlacesToVisitPage(_ID));
        }

        private async void songsOfRemembrance_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SongsOfRemembrancePage(_ID));
        }

        private async void settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage(_ID));
        }

        private async void logOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(_ID));
        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }        
    }
}