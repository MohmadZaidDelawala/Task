using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.ApplicationContext;
using Task.JWTServices;
using Task.Models;

namespace Task.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly JwtTokenService _tokenService;

        public AuthController(ApplicationDBContext context, JwtTokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {

            var user = (from A in _context.UserMaster
                        join B in _context.TokenMaster on A.EmployeeID equals B.EmployeeID
                        where A.Name == req.Username && A.Password == req.Password
                        select new
                        {
                            A.Name,
                            A.EmployeeID,
                            B.Token
                        }).FirstOrDefault();

            if (user == null)
                return Unauthorized(new {result = "Un-Authorise", Status = "Fail"});


            return Ok(new { result = user, Status = "OK" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest req)
        {
            var exists = _context.UserMaster.Any(u => u.Name == req.Name);
            if (exists)
                return Conflict(new {Result = "Employee already exists." });

            var newEmployee = new UserMaster
            {
                Name = req.Name,
                Password = req.Password
            };

            _context.UserMaster.Add(newEmployee);
            _context.SaveChanges();

            var insertedEmployee = _context.UserMaster.First(u => u.Name == req.Name);

            var token = _tokenService.GenerateToken(insertedEmployee.EmployeeID, insertedEmployee.Name);

            _context.TokenMaster.Add(new TokenMaster
            {
                EmployeeID = insertedEmployee.EmployeeID,
                Token = token
            });

            _context.SaveChanges();

            return Ok(new { result = token, Status = "OK" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
