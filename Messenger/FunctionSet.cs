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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger
{
    static class FunctionSet
    {   
        public static UserContext UsersContDB = new UserContext();

        private static string[] menuItems = { "Registration", "Authentication", "Possible actions", "Add new Recepient", "Main Menu", "Close App" };

        private static string dilapidated = "|";// " " - It is not necessary because for the notifications will work incorrectly
        private static Users AvtUser = new Users();
        private static bool isauthorized = false;

        static public void Registration()
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
                Console.Write("Phone (+XX..X): ");
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
                Console.Write("Password (10 numbers):");
                regUser.Password = Console.ReadLine();
            }
            while (regUser.Password == null);
            do
            {
                Console.Write("Email :");
                regUser.Adress = Console.ReadLine();
            }
            while (regUser.Adress == null);

            UsersContDB.UserCont.Add(regUser);
            UsersContDB.SaveChanges();
            Console.WriteLine($"BD conect +new {regUser.Name} are added!");
            Console.ReadKey();
            FirstMenu();
        }

        static public void NewRecepient()
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

            UsersContDB.RecepientsCont.Add(NewRecepient);
            UsersContDB.SaveChanges();
            Console.WriteLine($"BD conect +new {NewRecepient.Name} are added!");
            Console.ReadKey();
            FirstMenu();
        }
        static public void Authorisation()
        {
            isauthorized = false;
            AvtUser = new Users();
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
                isauthorized = true;
                //send messeg
                SelectMessenger(AvtUser);

            }
        }

        static private void PossibleActions()
        {
            if (isauthorized)
            {
                SelectMessenger(AvtUser);
            }
            else
            {
                Authorisation();
            }
        }
        static public void SelectMessenger(Users avtUser)
        {
            string[] SelMes = { "Write to one", "write to everyone", "Output to file", "Show data in file", "Menu" };
            short curItem = 0;
            switch (Menu(curItem, SelMes))
            {
                case 0: SendMessege(avtUser); ; break;
                case 1: SendMessegeAll(avtUser); ; break;
                case 2: OutputToFile();break;
                case 3: SelectFileWrite(); break;
                case 4: FirstMenu(); break;

            }
        }
        static public void OutputToFile()
        {
            string[] SelMes = { "Output to file Users", "Output to file Recepient", "Output to file Messeges", "Main Menu" };
            short curItem = 0;
            switch (Menu(curItem, SelMes))
            {
                case 0: SelectTypeToFile("Users"); break;
                case 1: SelectTypeToFile("Recepients"); break;
                case 2: SelectTypeToFile("Messeges"); break;
                case 3: FirstMenu(); break;
                    
            }
        }
        static public void SelectTypeToFile(string n)
        {
            int usC = UsersContDB.UserCont.Count();
            int reC = UsersContDB.RecepientsCont.Count();
            int meC = UsersContDB.MessegesCont.Count();
         
            List<string[]> OutText = new List<string[]>();
            if (n == "Users")
            {
                for (int i = 1; i <= usC; i++)
                {   //no password
                    //string[] Rstr = new string[3];
                    //To write from a file you need to write a password
                    string[] Rstr = new string[4];
                    Users usersTime = UsersContDB.UserCont.FirstOrDefault(p => p.UserId == i);
                    Rstr[0] = Tab(usersTime.Name);
                    Rstr[1] = Tab(usersTime.PhoneNumber);
                    Rstr[2] = Tab(usersTime.Adress);
                    Rstr[3] = Tab(usersTime.Password);// need???

                    OutText.Add(Rstr);
                }
            }

            if (n == "Recepients")
            {
                for (int i = 1; i <= reC; i++)
                {
                    string[] Rstr = new string[3];

                    Recepients RecepTime = UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientId == i); 
                    Rstr[0] = Tab(RecepTime.Name); 
                    Rstr[1] = Tab(RecepTime.RecepientNumber);
                    Rstr[2] = Tab(RecepTime.Adress);
                

                    OutText.Add(Rstr);
                }
            }

            if(n== "Messeges")
            {
                for(int i = 1; i <= meC; i++)
                {
                    string[] Rstr = new string[5];
                    Messeges mesTime = UsersContDB.MessegesCont.FirstOrDefault(p => p.MessegesId == i); 
                    Rstr[0] = Tab(mesTime.UserId.ToString());
                    Rstr[1] = Tab(mesTime.RecepientId.ToString());
                    Rstr[2] = Tab(mesTime.TimeMess.ToString());
                    Rstr[3] = Tab(mesTime.DataMess.ToString());
                    Rstr[4] = Tab(mesTime.MessegeText);
                    OutText.Add(Rstr);
                }
            }

            //ReadRecord.ReadingInf(n + ".txt", OutText);
            string[] vs=new string[OutText.Count()]; 
            for(int i=0; i<OutText.Count();i++)
            {
                vs[i] = "";
                for (int j = 0; j < OutText[i].Length; j++)
                {
                    vs[i] += OutText[i][j];
                }
            }
            ReadRecord.ReadingInf(n + ".txt", vs);

        }
        
        //Tab to separate items in a file
        private static string Tab(string str)
        {
            
            ReadRecord.dilapidated = dilapidated;
            str += dilapidated;
            return str;

        }

        static public void SelectAction()
        {
            short curItem = 0;
            string[] menuSelect = { "Main Menu", "Exit" };

            switch (Menu(curItem, menuSelect))
            {
                case 0: Registration(); break;
                case 1: Program.Work = false; return;

            }
        }
        static public void SelectFileWrite()
        {
            short curItem = 0;
            string[] menuSelect = { "Users", "Recepients","Messeges","Main Menu" };

            switch (Menu(curItem, menuSelect))
            {
                case 0: ReadRecord.ReadingInf("Users"); break;
                case 1: ReadRecord.ReadingInf("Recepients"); break;
                case 2: ReadRecord.ReadingInf("Messeges"); break;
                case 3: FirstMenu(); break;

            }
        }
       
        static public void FirstMenu()
        {
            short curItem = 0;

            switch (Menu(curItem, menuItems))
            {
                case 0: Registration() ; break;
                case 1: Authorisation(); break;
                case 2: PossibleActions(); break;
                case 3: NewRecepient(); break;
                case 4: FirstMenu(); break;
                case 5: Program.Work = false; return;
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
            int mesId = UsersContDB.MessegesCont.Count();
            List<Recepients> recepients = new List<Recepients>();
            Messeges[] Mes = new Messeges[recepCount];

            Console.WriteLine();
            do
            {
                recepients.Add(UsersContDB.RecepientsCont.FirstOrDefault(p => p.RecepientId == i+1));
                if (recepients[i] == null)
                {
                    Console.WriteLine($" if (recepients[{i}] == null)");
                }
                Console.WriteLine(recepients[i].RecepientNumber + "\t"+recepients[i].Name);
               i++;
            }
            while (i< recepCount);

            Console.WriteLine($"Recipients count: {recepCount} in DB ");
            Console.ReadKey();
           
            Console.Write("Text messege:");
            mess = Console.ReadLine();
          

            var date= DateTime.Now;


            for (i = 0; i < recepCount; i++)
            {
                Messeges messeges1 = new Messeges();

                messeges1.MessegesId = mesId++;
                messeges1.RecepientId = recepients[i].RecepientId;
                messeges1.UserId = UserC.UserId;
                messeges1.MessegeText = mess;
                messeges1.DataMess = DateTime.Now;
                messeges1.TimeMess=DateTime.Now.TimeOfDay;
                Mes[i] = messeges1;
               // message.Add(messeges1);
                UsersContDB.MessegesCont.Add(Mes[i]);
            }
            
           
            
            UsersContDB.SaveChanges();
            SaveInFileJson("Test3", Mes);

            Console.WriteLine("Messege are sended. Press any key to continue");
            Console.ReadKey();
        }
        
        public static void SendMessege(Users UserC)
        {
            Recepients currentRecepient = new Recepients();
            Recepients recepient = new Recepients();
            Messeges messege = new Messeges();
            
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
            }
            do
            {
                Console.Write("Email :");
                currentRecepient.Adress = Console.ReadLine();

                UsersContDB.RecepientsCont.Add(currentRecepient);
                UsersContDB.SaveChanges();
                Console.Write(UsersContDB.RecepientsCont.FirstOrDefault(p => p.Adress == currentRecepient.Adress));

            }
            while (currentRecepient.Adress == null);
            
            Console.WriteLine("Text messege:");
            messege.MessegeText = Console.ReadLine();
            messege.RecepientId = (recepient == null) ? currentRecepient.RecepientId : recepient.RecepientId;
            messege.UserId = UserC.UserId;
            messege.DataMess = DateTime.Now;
            messege.TimeMess = DateTime.Now.TimeOfDay;
            UsersContDB.MessegesCont.Add(messege);
            UsersContDB.SaveChanges();

            Messeges[] messageArray = new Messeges[1];
            messageArray[0] = messege;
            SaveInFileJson("Test2", messageArray);

            Console.WriteLine("Messege are sended. Press any key to continue");
            Console.ReadKey();
        }
       
        public static void SaveInFileJson<T>(string FileName, T[] data)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));

            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, data);
            }
        }

      



    }
}
