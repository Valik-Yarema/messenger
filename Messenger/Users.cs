using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Messenger
{
    class Users
    {   [Key]
        public int PhoneId { get; set; }
        private string phoneNambver;
        public string PhoneNambver
        {
            get
            {
                return this.phoneNambver;
            }


            set
            {
                Regex Rexphone = new Regex(@"^\+\d{12}");
                if (Rexphone.IsMatch(value))
                {
                    phoneNambver = value;
                }
                else
                {
                    Console.WriteLine("wrong phone number!");
                }
            }
        }

        private string password;
        public string Password
        { get
            {
                return this.password;
            }
            set
            {
                Regex Rexphone = new Regex(@"^\d{10}");
                if (Rexphone.IsMatch(value))
                {
                    password= value;
                }
            }
            
        }

        public string Name { get; set; }
        private string adress;
        public string Adress
        { get
            {
                return this.adress;
            }
            set
            {
                Regex RexAdress = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");
                if (RexAdress.IsMatch(value))
                {
                    adress = value;
                }
            }
        }

        public ICollection<Users> UsersColl { get; set; }
        public Users()
        {
            UsersColl = new List<Users>();
        }

    }
}
