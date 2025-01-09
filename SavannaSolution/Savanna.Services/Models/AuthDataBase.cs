using Savanna.Data.Data;

namespace Savanna.Services.Models
{
    public class AuthDataBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ErrorMessage {  get; set; }

        public AuthDataBase()
        {

        }
    }

    public class AuthData : AuthDataBase
    {
        public bool IsAuthentificated { get; set; }
        public AuthData() : base()
        {

        }
    }

    public class RegistrationData : AuthDataBase
    {
        public bool IsRegistred { get; set; }
        public string SecurityQuestion { get; set; }

        public RegistrationData() : base()
        {

        }
    }

    public class ChangingPasswordData : AuthDataBase
    {
        public bool IsPasswordChanged { get; set; }
        public ChangingPasswordData() : base()
        {

        }
    }
}
