using OnlineQuiz.Infrastructure;
using OnlineQuiz.Domain;
using System;
using System.Linq;
using System.Web.Http;

namespace OnlineQuiz.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly QuizDbContext _context = new QuizDbContext();

        //REGISTER
        [HttpPost, Route("register")]
        public IHttpActionResult Register(RegisterModel model)
        {
            if (model == null) return BadRequest("Invalid data.");

            if (_context.AppUsers.Any(u => u.Email == model.Email))
                return BadRequest("Email already exists.");

            var user = new AppUser
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = model.Password, 
                PasswordSalt = "",
                Role = "User"
            };

            _context.AppUsers.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }

        //LOGIN
        [HttpPost, Route("login")]
        public IHttpActionResult Login(LoginModel model)
        {
            if (model == null) return BadRequest("Invalid data.");

            var user = _context.AppUsers
                .FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == model.Password);

            if (user == null)
                return BadRequest("Invalid credentials.");

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                role = user.Role
            });
        }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
