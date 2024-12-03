using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class Customer :User
    {
        public string Status { get; set; }
        public string Role { get; set; }

        public Customer(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, string status, string role)
            : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
        {
            Status = status;
            Role = role;
        }
    }
}
