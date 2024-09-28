    using LoginAuth.Models;
    using LoginAuth.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics.Contracts;

    namespace LoginAuth.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            private readonly IConfiguration _config;
            private readonly Auth _auth;
            public LoginController(IConfiguration config)
            {
                _config = config;
                _auth = new Auth(config);
            }
            [AllowAnonymous]
            [HttpPost]
            public IActionResult Login([FromBody] LoginRequest login)
            {
                IActionResult response = Unauthorized();
                var user = _auth.AutheticationUser (login);
                if(user != null)
                {
                    string tokenString = _auth.GenerateJSONWebToken(user);
                    response = Ok(new {token = tokenString});
                }
                return response;
            }
        }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
