namespace DaoLibrary.Entities
{
    public class IdentityUser
    {
        public string UserIdentity { get; set; }
        public string Password { get; set; }

        public IdentityUser(string userIdentity, string password)
        {
            UserIdentity = userIdentity;
            Password = password;
        }
    }
}
