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
	public partial class SettingsPage : ContentPage
	{
        HttpClient client = new HttpClient();
        string _ID;
		public SettingsPage (string id)
		{
			InitializeComponent ();
            _ID = id;
        }
        
        private async void dontsave_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MenuPage(_ID));
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            if (entryPassword.Text == null)
            {
                await DisplayAlert("Error", "Please change password.\nOr else, click the button Don't save.", "Close");
            }
            else
            {
                var res = await DisplayAlert("Changing password", "Are you sure you want to change your password?", "Change it!", "Cancel");
                if (res == true)
                {
                    var response = await client.GetStringAsync(returnJSON.GetURL() +
                        "index.php?IDuser=" + _ID +
                        "&Password=" + entryPassword.Text);
                    await Navigation.PushModalAsync(new MenuPage(_ID));
                }                
            }
        }
    }
}