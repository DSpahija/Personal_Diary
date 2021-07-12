using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalDiary.Models
{
    public class Place
    {
        public int IDPlace { get; set; }
        public int IDuser { get; set; }
        public string Shteti { get; set; }
        public string Qyteti { get; set; }
        public string Notes { get; set; }
        public string isVisited { get; set; }

        public Place() { }
        
        public Place(string shteti, string qyteti)
        {
            Shteti = shteti;
            Qyteti = qyteti;
        }

        public Place(int idplace, int iduser, string shteti, string qyteti, string notes, string isvisited)
        {
            IDPlace = idplace;
            IDuser = iduser;
            Shteti = shteti;
            Qyteti = qyteti;
            Notes = notes;
            isVisited = isvisited;
        }
    }
}
