using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class Messeges
    {   [Key]
        public int MessegesId { get; set; }

        
        public int? UserId { get; set; }

       
        public int? RecepientId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        [ForeignKey("RecepientId")]
        public Recepients Recepients { get; set; }

    

        public DateTime DataMess { get; set; }
        public TimeSpan TimeMess { get; set; }
        public string MessegeText { get; set; }
        
        public Messeges()
        {
            DataMess = DateTime.Now.Date;
            TimeMess = DateTime.Now.TimeOfDay;
        }
    }
}
