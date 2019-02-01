using Messenger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
namespace Messenger
{
    static class FunctionSet
    {
       // static bool Work = true;

        public static UserContext UsersContDB = new UserContext();
        private static string[] menuItems = { "Registration", "Authentication", "Add new Recepient", "Main Menu", "Close App" };
        static public void Registration(UserContext DB)
        {
            // Users user1 = new Users();
            Users regUser = new Users();
            Console.WriteLine("Please enter :");
            do
            {
                Console.Write("Name :");
                regUser.Name = Console.ReadLine();
                if (regUser.Name == "break")
                {
                    return;
                }
            }
            while (regUser.Name == null);

            do
            {
                Console.Write("Phone number: ");
                regUser.PhoneNumber = Console.ReadLine();
                Users alreadyRegisteredUser = UsersContDB.UserCont.FirstOrDefault(p => p.PhoneNumber == regUser.PhoneNumber);
                if (alreadyRegisteredUser != null)
                {
                    Console.Clear();
                    Console.WriteLine(" >> Error: This phone number is already register. Press any button to continue <<");
                    Console.ReadKey();
                    FirstMenu();
                    return;
                }
            }
            while (regUser.PhoneNumber == null);
            do
            {
                Console.Write("Password :");
                regUser.Password = Console.ReadLine();
            }
            while (regUser.Password == null);
            do
            {
                Console.Write("Email :");
                regUser.Adress = Console.ReadLine();
            }
            while (regUser.Adress == null);

            DB.UserCont.Add(regUser);
            DB.SaveChanges();
            Console.WriteLine($"BD conect +new {regUser.Name} are added!");
            Console.ReadKey();
            FirstMenu();
        }

        static public void NewRecepient(UserContext DB)
        {
            // Users user1 = new Users();
            Recepients NewRecepient = new Recepients();
            Console.WriteLine("Please enter :");
            do
            {
                Console.Write("Name :");
                NewRecepient.Name = Console.ReadLine();
                if (NewRecepient.Name == "break")
                {
                    return;
                }
            }
            while (NewRecepient.Name == null);

            do
            {
                Console.Write("Phone number: ");
                NewRecepient.RecepientNumber = Console.ReadLine();
                Recepients alreadyRegisteredUser = UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientNumber == NewRecepient.RecepientNumber);
                if (alreadyRegisteredUser != null)
                {
                    Console.Clear();
                    Console.WriteLine(" >> Error: This phone number is already register. Press any button to continue <<");
                    Console.ReadKey();
                    FirstMenu();
                    return;
                }
            }
            while (NewRecepient.RecepientNumber == null);


            do
            {
                Console.Write("Email :");
                NewRecepient.Adress = Console.ReadLine();
            }
            while (NewRecepient.Adress == null);

            DB.RecepientsCont.Add(NewRecepient);
            DB.SaveChanges();
            Console.WriteLine($"BD conect +new {NewRecepient.Name} are added!");
            Console.ReadKey();
            FirstMenu();
        }
        static public void Authorisation(UserContext DB)
        {
            Users AvtUser = new Users();
            string phoneNumber;
            string password;

            do
            {
                Console.Clear();
                Console.Write("Phone number : ");
                phoneNumber = Console.ReadLine();

                AvtUser = UsersContDB.UserCont.FirstOrDefault(p => p.PhoneNumber == phoneNumber);

                if (AvtUser == null)
                {
                    Console.WriteLine("Undefined phone number. Press any key to continue");
                    Console.ReadKey();
                }
            }
            while (AvtUser == null);

            Console.Write("Password: ");
            password = Console.ReadLine();

            if (password != AvtUser.Password)
            {
                Console.WriteLine(" >> Error: Invalid data. Please, check in or try again. Press any key to continue<<");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Authorisation complete");
                Console.ReadKey();
                //send messeg
                SelectMessenger(AvtUser);

            }
        }
        static public void SelectMessenger(Users avtUser)
        {
            string[] SelMes = { "Write to one", "write to everyone", "Menu" };
            short curItem = 0;
            switch (Menu(curItem, SelMes))
            {
                case 0: SendMessege(avtUser); ; break;
                case 1: SendMessegeAll(avtUser); ; break;
                case 2: FirstMenu(); break;

            }
        }
        static public void SelectAction()
        {
            short curItem = 0;
            string[] menuSelect = { "Main Menu", "Exit" };

            switch (Menu(curItem, menuSelect))
            {
                case 0: Registration(UsersContDB); break;
                case 1: Program.Work = false; return;

            }
        }
        static public void FirstMenu()
        {
            short curItem = 0;

            switch (Menu(curItem, menuItems))
            {
                case 0: Registration(UsersContDB); break;
                case 1: Authorisation(UsersContDB); break;
                case 2: NewRecepient(UsersContDB); break;
                case 3: FirstMenu(); break;
                case 4:Program.Work = false; return;
            }
        }
        static public short Menu(short curItem, string[] MenuText)
        {
            ConsoleKeyInfo key;
            short c;
            do
            {
                Console.Clear();
                Console.WriteLine("[ MENU ]");

                for (c = 0; c < MenuText.Length; c++)
                {

                    if (curItem == c)
                    {
                        Console.Write(">>");
                        Console.WriteLine(MenuText[c]);
                    }
                    else
                    {
                        Console.WriteLine(MenuText[c]);
                    }
                }
                Console.Write("\nSelect and press {Enter}");

                key = Console.ReadKey(true);

                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > MenuText.Length - 1) curItem = 0;
                }
                else
                {
                    if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(MenuText.Length - 1);
                    }
                }
            } while (key.KeyChar != 13);

            return curItem;

        }

        public static void SendMessegeAll(Users UserC)
        {
           
            string mess;
            int i = 0;
            int recepCount = UsersContDB.RecepientsCont.Count();
            List<Recepients> recepients = new List<Recepients>();
          
           

            Messeges[] message = new Messeges[recepCount];
            do
            {
                recepients.Add(UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientId == i));
                i++;
            }
            while (i< recepCount);

            Console.Write($"Recipients count: {recepCount} in DB ");
            Console.ReadKey();

           
            Console.WriteLine("Text messege:");
            mess = Console.ReadLine();
            for (i = 0; i < recepCount; i++)
            {
                message[i].MessegeText = mess;
                message[i].RecepientId = recepients[i].RecepientId;
                message[i].UserId = UserC.UserId;
            }
            SaveInFileJson("Test3", message);

            Console.WriteLine("Messege are sended. Press any key to continue");
            Console.ReadKey();
        }
        
        public static void SendMessege(Users UserC)
        {
            Recepients currentRecepient = new Recepients();
            Recepients recepient = new Recepients();
            Messeges message = new Messeges();
            
            do
            {
                Console.Write("Recipient's phone: ");
                currentRecepient.RecepientNumber = Console.ReadLine();
            }
            while (currentRecepient.RecepientNumber == null);

            recepient = UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientNumber == currentRecepient.RecepientNumber);

            if (recepient == null)
            {
                Console.Write("Recepient's name arbitrarily : ");
                currentRecepient.Name = Console.ReadLine();
                UsersContDB.RecepientsCont.Add(currentRecepient);
                UsersContDB.SaveChanges();
                Console.Write(UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientNumber == currentRecepient.RecepientNumber));
            }

            Console.WriteLine("Text messege:");
            message.MessegeText = Console.ReadLine();
            message.RecepientId = (recepient == null) ? currentRecepient.RecepientId : recepient.RecepientId;
            message.UserId = UserC.UserId;

            UsersContDB.MessegesCont.Add(message);
            UsersContDB.SaveChanges();

            Messeges[] messageArray = new Messeges[1];
            messageArray[0] = message;
            SaveInFileJson("Test2", messageArray);

            Console.WriteLine("Messege are sended. Press any key to continue");
            Console.ReadKey();
        }

        public static void SaveInFileJson<T>(string FileName, T[] data)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));

            using (FileStream fs = new FileStream("Messeges.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, data);
            }
        }

       /*private static void UpdateDemographics(Int32 customerID,string demoXml, string connectionString)
        {
            string commandText;
            Console.Write("Write your request : ");
            commandText = Console.ReadLine();
            //commandText = "UPDATE Sales.Store SET Demographics = @demographics "+ "WHERE CustomerID = @ID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                command.Parameters["@ID"].Value = customerID;
                // SQL Server will implicitly convert strings into XML.
                command.Parameters.AddWithValue("@demographics", demoXml);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }*/



    }
}
