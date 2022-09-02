using App_Delegate.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static App_Delegate.Program;
using static App_Delegate.Service.RegisterLogger;

namespace App_Delegate.Models
{
    public class Company
    {
        public delegate bool RegisterLog(string name, string surname, string password);
        public int Id { get; set; }
        public static int Counter { get; set; } = 1;
        public string Name { get; set; }

        //---> Add user company employee list
        List<User> CompanyUserList = new List<User>();

        public void Registration()
        {
            string name = "";
            string surname = "";
            string password = "";
            bool again;
            bool Exit = true;


            Console.WriteLine("Create Company");
            Name = Console.ReadLine();
again:
            while (Exit)
            {
            backname:
                Console.WriteLine("Enter your name");
                name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    again = Register(name, surname, password);
                    goto backname;
                }
            backsurname:
                Console.WriteLine("Enter your surname");
                surname = Console.ReadLine();
                if (string.IsNullOrEmpty(surname))
                {
                    again = Register(name, surname, password);
                    goto backsurname;
                }
            backpassword:
                Console.WriteLine("Enter your password");
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password))
                {
                    again = Register(name, surname, password);
                    goto backpassword;
                }

                again = Register(name, surname, password);

                if (!again)
                    goto backpassword;

                Exit = false;
            }
        
        }
        public bool Register(string name, string surname, string password)
        {
            //---> Register error logger
            FindUserLogger findUserLogger = new FindUserLogger();
            RegisterLoggDelegate FoundUserLogger = new RegisterLoggDelegate(findUserLogger.FoundUserLogger);
            RegisterLoggDelegate notFoundUserLogger = new RegisterLoggDelegate(findUserLogger.FoundUserLogger);

            RegisterLogger nullItemLogger = new RegisterLogger();
            RegisterLoggDelegate registerErrorLogg = new RegisterLoggDelegate(nullItemLogger.NameLogger);
            RegisterLoggDelegate registerErrorLogg2 = new RegisterLoggDelegate(nullItemLogger.SurnameLogger);
            RegisterLoggDelegate registerErrorLogg3 = new RegisterLoggDelegate(nullItemLogger.PasswordLogger);



            //---> Check valid password 
            Regex regex = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match match = regex.Match(password.ToString());

            if (string.IsNullOrEmpty(name))
            {
                registerErrorLogg.Invoke("Name is not valid ");
                return false;
            }
            if (string.IsNullOrEmpty(surname))
            {
                registerErrorLogg2.Invoke("Surname is not valid ");
                return false;
            }
            if (!match.Success)
            {
                registerErrorLogg3.Invoke("Password is not valid ");
                return false;
            }
            else
            {
                var username = $"{name}{surname}".Username();
                var email = $"{username}".EmailFormat();
                password = $"{password}".PasswordFormat();
                var checkUserInDb=CompanyUserList.FirstOrDefault(x => x._name == name
                && x._surname == surname
                && x._email == email
                && x._username == username);
                if (checkUserInDb==null)
                {
                    CompanyUserList.Add(new User
                    {
                        _id = Counter++,
                        _name = Name,
                        _surname = surname,
                        _password = password,
                        _username = username,
                        _email = email
                    });
                }
                else
                {
                    FoundUserLogger.Invoke("User already find in database");
                    return false;
                }
            }
            return true;
        }
        public bool Login(string username, string password)
        {
            //----> Login not found user error logger 
            LoginLogger loginLogger = new LoginLogger();
            RegisterLoggDelegate loginLogg = new RegisterLoggDelegate(loginLogger.NotFoundUserLogger);


            var user = CompanyUserList.FirstOrDefault(x => x._username == username && x.Password == password);
            if (user== null)
                loginLogg.Invoke("Not found user ");
            return true;
        }

        public void GetAll()
        {
            var GetUsers = CompanyUserList;
            foreach (var user in GetUsers)
            {
                Console.WriteLine($"Name: {user.Name}\n Surname: {user.Surname}\n Username: {user._username}\n Email: {user._email}");
            }
        }
        public void GetById(int id)
        {
            //----> Update time not found user error logger 
            LoginLogger loginLogger = new LoginLogger();
            RegisterLoggDelegate loginLogg = new RegisterLoggDelegate(loginLogger.NotFoundUserLogger);

            var userById = CompanyUserList.FirstOrDefault(x => x._id == id);
            Console.WriteLine($"\nName: {userById.Name}\n Surname: {userById.Surname}\n Username: {userById._username}\n Email: {userById._email}");
            if (userById==null)
                    loginLogg.Invoke("Not found user ");
        }
        public void UpdateById(int userId, int category, string value)
        {
            //----> Update time not found user error logger 
            LoginLogger loginLogger = new LoginLogger();
            RegisterLoggDelegate loginLogg = new RegisterLoggDelegate(loginLogger.NotFoundUserLogger);

            var userById = CompanyUserList.FirstOrDefault(x => x._id == userId);
            if (userById == null)
                loginLogg.Invoke("Not found user ");
            switch (category)
            {
                case 1:
                    userById._name = value;
                    break;
                case 2:
                    userById._surname = value;
                    break;
                case 3:
                    userById._username = value;
                    break;
                case 4:
                    userById._email = value;
                    break;
            }
            CompanyUserList.Add(userById);
        }

        public void SelecCategoryEdit()
        {

            Console.WriteLine("Please, select you want edit category");
            Console.WriteLine("Name edit --> 1 \n Surname edit --> 2 \n Username edit --> 3 \n Email edit --> 4");
            int categoryId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter user id");
            int userId = int.Parse(Console.ReadLine());
            switch (categoryId)
            {
                case 1:
                    string updateName = Console.ReadLine();
                    UpdateById(userId, categoryId, updateName);
                    break;
                case 2:
                    string updateSurname = Console.ReadLine();
                    UpdateById(userId, categoryId, updateSurname);
                    break;
                case 3:
                    string updateUsername = Console.ReadLine();
                    UpdateById(userId, categoryId, updateUsername);
                    break;
                case 4:
                    string updateEmail = Console.ReadLine();
                    UpdateById(userId, categoryId, updateEmail);
                    break;
            }
        }
        public void DeleteById(int id)
        {
            CompanyUserList.Remove(CompanyUserList.FirstOrDefault(x => x._id == id));
        }
        public string Exic()
        {
            Console.WriteLine("\nContinue -> 'Y' Exic -> 'N' ");
            var ExitOrContinue = Console.ReadLine();
            if (ExitOrContinue.ToUpper() == "Y")
                return "Y";
            else
                return ExitOrContinue;

        }
    }
}
