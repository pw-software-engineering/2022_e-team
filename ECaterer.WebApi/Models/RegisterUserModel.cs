using ECaterer.Core.Models;

namespace ECaterer.WebApi.Models
{
    public class RegisterUserModel
    {
        public string Password { get; set; }
        public Client Client { get; set; }
    }
}
