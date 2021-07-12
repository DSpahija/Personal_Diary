using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalDiary.Models
{
    public class Story
    {
        public int IDStory { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string StoryContent { get; set; }

        public Story() { }

        public Story(string title, string date)
        {
            Title = title;
            Date = date;
        }

        public Story(string title, string date, string storyContent)
        {
            Title = title;
            Date = date;
            StoryContent = storyContent;
        }

        public Story(string storyContent)
        {
            StoryContent = storyContent;
        }
    }
}
