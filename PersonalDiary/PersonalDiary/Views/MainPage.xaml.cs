using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalDiary.Models;

namespace PersonalDiary
{
	public partial class MainPage : ContentPage
	{
        string _ID="";
        public MainPage(string id)
        { 
            InitializeComponent();
            entryUsername.Completed += (s, e) => entryPassword.Focus();
        }

        private async void Clicked_Register(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage(_ID));
        }

        async void Clicked_LogIn(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?verifylogin&Username=" + entryUsername.Text +
                "&Password=" + entryPassword.Text);
            if(response != "0")
            {
                await Navigation.PushModalAsync(new MenuPage(response));
            }
            else
            {
                await DisplayAlert("Error", "Wrong Username or Password", "Try again");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
