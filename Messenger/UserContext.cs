using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
   
        class UserContext : DbContext
        {
            public UserContext()
                : base("DbConnection")
            { }

            public DbSet<Users> UserCont { get; set; }
            public DbSet<Messeges> MessegesCont { get; set; }
            public DbSet<Recepients> Recepients { get; set; }
        }
    
}
