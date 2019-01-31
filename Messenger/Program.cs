using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class Program
    {
       static public void Registration(UserContext DB)
        {
            Users user1 = new Users();

            Console.WriteLine("Please enter :");
            do
            {
                Console.Write("Name :");
                user1.Name = Console.ReadLine();
            }
            while (user1.Name == null);
            do
            {
                Console.Write("Phone :");
                user1.PhoneNambver = Console.ReadLine();
            }
            while (user1.PhoneNambver == null);
            do
            {
                Console.Write("Password :");
                user1.Password = Console.ReadLine();
            }
            while (user1.Password == null);
            do
            {
                Console.Write("Email :");
                user1.Adress = Console.ReadLine();
            }
            while (user1.Adress == null);

            DB.UserCont.Add(user1);
            DB.SaveChanges();
            Console.WriteLine("BD conect +new User!");
            Console.ReadKey();
        }

        static public void Authorisation(UserContext DB)
        {
            
        }
       //static public void 
        static void Main(string[] args)
        {
            
            using (UserContext db = new UserContext())
            {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            string[] menuItems = { "Add new User", "Add new Recepient", "3", "4" };

            do
            {
                Console.Clear();
                Console.WriteLine("Pick an option . . .");
                
                for (c = 0; c < menuItems.Length; c++)
                {
                   
                    if (curItem == c)
                    {
                        Console.Write(">>");
                        Console.WriteLine(menuItems[c]);
                    }
                    else
                    {
                        Console.WriteLine(menuItems[c]);
                    }
                }
                Console.Write("Select your choice with the arrow keys.");

                key = Console.ReadKey(true);
           
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menuItems.Length - 1) curItem = 0;
                }
                else
                {
                    if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
                    }
                }
        } while (key.KeyChar != 13) ;

              
            switch (curItem)
            {
                case 0: Registration(db) ;break;
                case 1: Console.WriteLine(" case 1"); break;
            }

            

            }

            Console.ReadKey();
        }
    }
}

