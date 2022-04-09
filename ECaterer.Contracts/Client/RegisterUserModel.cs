using ECaterer.Core.Models;

namespace ECaterer.Contracts
{
    public class RegisterUserModel
    {
        public string Password { get; set; }
        public Client Client { get; set; }
    }
}
