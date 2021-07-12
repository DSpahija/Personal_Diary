using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalDiary.Models
{
    public class User
    {
        public int IDUser { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        
        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
        /*
        public bool checkInfo()
        {
            if (_Username.Equals("") && _Password.Equals(""))
                return true;
            else
                return false;
        }*/
    }
}
