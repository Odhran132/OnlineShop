using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class Admin : User
    {
        public DateTime LastLogin { get; set; }

        public Admin(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, DateTime lastLogin)
            : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
        {
            LastLogin = lastLogin;
        }
    }
}
