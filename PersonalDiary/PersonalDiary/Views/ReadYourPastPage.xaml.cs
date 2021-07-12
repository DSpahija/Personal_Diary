using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalDiary.Models;
using System.Net;
namespace PersonalDiary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadYourPastPage : ContentPage
    {
        HttpClient client = new HttpClient();
        static string _ID;
        public static string title, date, idStory;

        public ReadYourPastPage(string id)
        {
            InitializeComponent();
            _ID = id;
            GetStories();
            listViewStories.ItemSelected += ListViewStories_ItemSelected;
        }

        private void ListViewStories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Story story)
            {
                title = story.Title;
                date = story.Date;
                idStory = story.IDStory.ToString();

                listViewStories.SelectedItem = null;
                Navigation.PushModalAsync(new ReadAStoryPage(_ID));
            }
        }

        public async void GetStories()
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getStories");

            var stories = JsonConvert.DeserializeObject<List<Story>>(response);
            listViewStories.ItemsSource = stories;
        }

        public async void SearchTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = await client.GetStringAsync(returnJSON.GetURL() +
                "index.php?IDuser=" + _ID +
                "&getStories");

            List<Story> stories = JsonConvert.DeserializeObject<List<Story>>(response);

            var Search = searchBarTitle.Text;
         
            var suggestions = stories.Where(c => (c.Title.ToLower().StartsWith(Search) 
                                        == Search.ToLower().StartsWith(Search)) ||
                                        c.Date.ToLower().StartsWith(Search)
                                        == Search.ToLower().StartsWith(Search));
            listViewStories.ItemsSource = suggestions;
            if (searchBarTitle.Text == "")
            {
                GetStories();
            }
        }
    }
}