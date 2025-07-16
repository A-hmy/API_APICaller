using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Services;
namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {
        private readonly TokenService _tokenService;

        public LoginController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        public string Login(LoginModel userlogin)
        {
            var(isvalid,rule)=UserService.ValidateUser(userlogin);
            if (isvalid) { return _tokenService.GenerateToken(userlogin.UserName, rule); }
            else { return "User not sign up."; }
        }
    }
}
