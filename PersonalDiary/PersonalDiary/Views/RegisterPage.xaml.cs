using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalDiary.Models;
using System.Net.Http;

namespace PersonalDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        string _ID="";
		public RegisterPage(string id)
		{
			InitializeComponent();
            entryEmail.Completed += (s, e) => entryUsername.Focus();
            entryUsername.Completed += (s, e) => entryPassword.Focus();
        }

        private async void Clicked_LogIn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(_ID));
        }

        private async void Clicked_Register(object sender, EventArgs e)
        {            
            if(entryEmail.Text == null || entryUsername.Text == null || entryPassword.Text == null)
            {
                await DisplayAlert("Error", "Please fill all the empty fields!", "Sir yes sir!");
            }
            else
            {
                HttpClient client = new HttpClient();

                var response = await client.GetStringAsync
                    (returnJSON.GetURL() + "index.php?Email=" +
                    entryEmail.Text + "&Username=" +
                    entryUsername.Text + "&Password=" +
                    entryPassword.Text);

                if (response != "0")
                {
                    registerButton.IsEnabled = false;
                    await DisplayAlert("Welcome", "Thank you for choosing Dear Diary for expressing youself!", "Continue");
                    await Navigation.PushModalAsync(new MenuPage(response));
                }
                else
                {
                    await DisplayAlert("Error", "Error", "Error");
                }
            }
        }
    }
}