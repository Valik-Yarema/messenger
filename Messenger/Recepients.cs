using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Messenger
{
    class Recepients
    { [Key]
        public int IdRecepient { get; set; }

        private string recepientNamber;
        public string RecepientNamber
        {
            get
            {
                return this.recepientNamber;
            }
            
            set
            {
                Regex Rexphone = new Regex(@"^\+\d{12}");
                if (Rexphone.IsMatch(value))
                {
                    recepientNamber = value;
                }
            } 
        }
        
        public string Name { get; set; }
        private string adress;
        public string Adress
        {
            get
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

        public ICollection<Recepients> RecepientsColl { get; set; }
        Recepients()
        {
            RecepientsColl = new List<Recepients>();
        }
    }
}
