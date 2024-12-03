namespace OnlineShop
{
    public class User
    {
        public int ID { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }

        public User(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity)
        {
            ID = id;
            UserName = userName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            AddressStreet = addressStreet;
            AddressCity = addressCity;
        }
    }
}


