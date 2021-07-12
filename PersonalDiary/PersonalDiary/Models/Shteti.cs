using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalDiary.Models
{
    public class Shteti
    {
        public int IDShteti { get; set; }
        public string Name { get; set; }

        public Shteti() { }
        public Shteti(int id, string name)
        {
            IDShteti = id;
            Name = name;
        }
    }
}
