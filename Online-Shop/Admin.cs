using System;

namespace OnlineShop
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

