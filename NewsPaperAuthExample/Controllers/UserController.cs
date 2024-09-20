using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsPaperAuthExample.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            var userDtos = new List<UserGetDTO>();
            foreach (var user in users)
            {
                userDtos.Add(new UserGetDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name
                });
            }

            return Ok(userDtos);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserGetDTO>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            var userDto = new UserGetDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name
            };

            return Ok(userDto);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserAddDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                PasswordHash = model.Password 
            };

            await _userService.CreateUserAsync(user, model.Role);


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, null);
        }



        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserEditDTO model)
        {
            if (id != model.Id)
                return BadRequest("User ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            user.Email = model.Email;
            user.Name = model.Name;
            user.UserName = model.UserName;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = model.Password; 
            }

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName)
    };

            var roles = await _userService.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValidUser = await _userService.ValidateUserCredentialsAsync(model.Email, model.Password);
            if (!isValidUser)
            {
                return Unauthorized("Invalid credentials.");
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);

            var tokenString = await GenerateJwtToken(user);
            return Ok(new { Token = tokenString });
        }

    }
}

