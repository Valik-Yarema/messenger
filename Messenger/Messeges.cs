using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Messenger
{
    [DataContract]
    class Messeges
    {   [Key][DataMember]
        public int MessegesId { get; set; }

        [DataMember]
        public int? UserId { get; set; }

       [DataMember]
        public int? RecepientId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        [ForeignKey("RecepientId")]
        public Recepients Recepients { get; set; }


        [DataMember]
        public DateTime DataMess { get; set; }
        public TimeSpan TimeMess { get; set; }
        [DataMember]
        public string MessegeText { get; set; }
        
        public Messeges()
        {
            DataMess = DateTime.Now.Date;
            TimeMess = DateTime.Now.TimeOfDay;
        }
    }
}
