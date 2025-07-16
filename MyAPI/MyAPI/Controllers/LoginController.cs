using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Services;
namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase 
    {
        private readonly TokenService _tokenService;

        public LoginController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult Login(LoginModel userlogin)
        {
            var(isvalid,rule)=UserService.ValidateUser(userlogin);
            if (isvalid) { return Ok(_tokenService.GenerateToken(userlogin.UserName, rule)); }
            else {  return NotFound("User is not signed up."); }
        }
    }
}
