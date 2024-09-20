using Microsoft.AspNetCore.Mvc;
using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Entities.DTO.Users;
using NewsPaperAuthExample.Service;

namespace NewsPaperAuthExample.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

            await _userService.CreateUserAsync(user);


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, null);
        }

        [HttpPut("{id}")]
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
    }
}

