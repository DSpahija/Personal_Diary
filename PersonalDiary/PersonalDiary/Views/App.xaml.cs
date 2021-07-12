using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PersonalDiary
{
	public partial class App : Application
	{
		public App ()
		{
            string id = "";
			InitializeComponent();
            MainPage = new MainPage(id);

            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Honeydew);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
