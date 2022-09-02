using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Delegate
{
    public static class UserExtensions
    {
        public static int PasswordEnd { get; set; } = 1;
        public static string Username(this string str)
        {
            return String.Format(str.Trim().ToLower());
        }
        public static string EmailFormat(this string str)
        {
            return String.Format(str.Trim().ToLower()+"@gmail.com");
        }
        public static string PasswordFormat(this string str)
        {
            return String.Format(str.Trim().ToLower() + PasswordEnd++);
        }
    }
}
