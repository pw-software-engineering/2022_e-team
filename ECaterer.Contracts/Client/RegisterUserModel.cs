using ECaterer.Contracts.Client;

namespace ECaterer.Contracts
{
    public class RegisterUserModel
    {
        public string Password { get; set; }
        public ClientModel Client { get; set; }
    }
}
