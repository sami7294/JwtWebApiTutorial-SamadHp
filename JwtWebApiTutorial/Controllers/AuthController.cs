using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthController(IConfiguration configuration , DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

       

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {


            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.UserTb.Add(user);

            await _context.SaveChangesAsync();

            return Ok(await _context.UserTb.ToListAsync());
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            var fullUser = await _context.UserTb.FirstOrDefaultAsync(u => u.Username == request.Username );


            if (fullUser == null )
            {
                return BadRequest("Bad credentials username");
            }
            var pass = VerifyPasswordHash(request.Password, fullUser.PasswordHash, fullUser.PasswordSalt);
            if (!pass)
            {
                return BadRequest("Bad credentials password");
            }

            string token = CreateToken(fullUser);


            return Ok(token);



            //var userMan = await _context.UserTb.FirstOrDefault(request);


            //if (userMan.Username != request.Username)
            //{
            //    return BadRequest("User not found.");
            //}

            //if (!VerifyPasswordHash(request.Password, userMan.PasswordHash, userMan.PasswordSalt))
            //{
            //    return BadRequest("Wrong password.");
            //}

            //string token = CreateToken(userMan);


            //return Ok(token);
        }





        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
