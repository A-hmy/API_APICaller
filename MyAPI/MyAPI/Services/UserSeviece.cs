using MyAPI.Models;

namespace MyAPI.Services
{
    public class UserService
    {
        static public (bool isValid, string role) ValidateUser(LoginModel user)
        {
            if (user.UserName == "Ali" && user.Password == "1234")
                return (true, "admin");
            if (user.UserName == "Amir" && user.Password == "1234")
                return (true, "user");
            return (false, null);
        }
    }

}
 