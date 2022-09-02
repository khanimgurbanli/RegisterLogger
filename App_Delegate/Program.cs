using App_Delegate.Models;
using App_Delegate.Service;
using static App_Delegate.Models.Company;
using static App_Delegate.Program;

namespace App_Delegate
{

    public class Program
    {

        public static void Main()
        {


            Company company = new Company();
        backMenu:
            Console.WriteLine(" \n -----Enter category key----- \n\n1.Register --> 1 \n2.Login --> 2 \n3.See all users in Company --> 3" +
                "\n4.Get one User from Company --> 4  \n5.Update User's infos --> 5 \n6.Delete User from Company --> 6" +
                "\n7 Exit --> 7");
            Console.Write("\n\nSelect one : ");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    company.Registration();
                        goto backMenu; 
                    break;
                case 2:
                    Console.WriteLine("Please,enter username");
                    string username = Console.ReadLine();
                    Console.WriteLine("Please,enter  password");
                    string password = Console.ReadLine();
                    var result=company.Login(username, password);
                    if (result==true)
                        Console.WriteLine("Succesfully logged! Welcome your account");
                    goto backMenu;
                    break;
                case 3:
                    company.GetAll();
                    goto backMenu;
                    break;
                case 4:
                    Console.WriteLine("Please,enter user id");
                    int id = int.Parse(Console.ReadLine());
                    company.GetById(id);
                    goto backMenu;
                    break;
                case 5:
                    company.SelecCategoryEdit();
                    goto backMenu;
                    break;
                case 6:
                    Console.WriteLine("Please,enter user id for deleted");
                    int userId = int.Parse(Console.ReadLine());
                    company.DeleteById(userId);
                    goto backMenu;
                    break;
                case 7:
                    company.Exic();
                    break;
            }

        }
    }
}

