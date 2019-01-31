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
    {
        [Key]
        public int IdMesseg { get; set; }

        public int? SenderPhone { get; set; }
        [ForeignKey("SenderPhone")]
        public Users Users { get; set; }

        public int? RecepientPhone { get; set; }
        [ForeignKey("RecepientPhone")]
        public Recepients Recepients { get; set; }

        public DataType DataMess { get; set; }
        public DataType TimeMess { get; set; }
        public string MessegeText { get; set; }
        
    }
}
