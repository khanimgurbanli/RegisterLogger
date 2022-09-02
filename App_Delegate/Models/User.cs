using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Delegate.Models
{
    public class User
    {
        public static int Counter { get; set; }
        public int _id { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public string _username { get; set; }
        public string _email { get; set; }
        public string _password { get; set; }

        public User() { }
        public User( string name, string surname, string password)
        {
           
            Name = name;
            Surname = surname;
            Password = password;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
