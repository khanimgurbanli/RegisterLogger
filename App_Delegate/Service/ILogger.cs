using App_Delegate.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Delegate.Service
{

    public interface ILogger
    {
        void SetLogger(string log);
    }
    public delegate void RegisterLoggDelegate(string message);
    public delegate void LoginLoggDelegate(string message);
    public delegate void FindUserLoggDelegate(string message);
    public class RegisterLogger : ILogger
    {
        public void SetLogger(string log)
        {
            Console.WriteLine($"Set  Logger  -> {log}");
        }
        public void NameLogger(string log)
        {
            Console.WriteLine($"Name input is null -> {log}");
        }
        public void SurnameLogger(string log)
        {
            Console.WriteLine($"Surname input is null -> {log}");
        }
        public void PasswordLogger(string log)
        {
            Console.WriteLine($"Password format is invalid -> {log}"); 
        }

        public class LoginLogger : ILogger
        {
            public void SetLogger(string log)
            {
                Console.WriteLine($"Password format is invalid -> {log}"); ;
            }

            public void NotFoundUserLogger(string log)
            {
                Console.WriteLine($"Not found user which you entered his(him) info -> {log}"); ;
            }
        }

        public class FindUserLogger : ILogger
        {
            public void SetLogger(string log)
            {
                Console.WriteLine($"User exciting-> {log}"); 
            }

            public void NotFoundUserLogger(string log)
            {
                Console.WriteLine($"User already exciting -> {log}"); ;
            }
        }

    }
}
