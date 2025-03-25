using AirTickets.Dto;
using AirTickets.Data;
using AirTickets.Services;
using Microsoft.AspNetCore.Mvc;
using AirTickets.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using AirTickets.BlModels;
using Microsoft.AspNetCore.Authorization;
using AirTickets.Identity;
using System.Security.Claims;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public UserController(ILogger<UserController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, BlUser>();
                cfg.CreateMap<BlUser, UserDto>();
                cfg.CreateMap<UserPostDto, BlUser>();
                cfg.CreateMap<BlUser, UserPostDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [HttpGet]
        [Route("users/{email}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "User is not in database.")]
        public IActionResult Get(string email)
        {
            var userService = new UserService(_context);

            try
            {
                var user = userService.ReadCurrent(email);

                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("users/login")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "User is not in database.")]
        public IActionResult Login([FromBody]UserPostDto user)
        {
            var userService = new UserService(_context);

            try
            {
                var existingUser = userService.Login(user.Email, user.Password);

                var tokenClass = new TokenClass();
                var token = tokenClass.GenerateToken(new TokenRequest 
                {
                    Id = existingUser.Id,
                    Role = existingUser.Role,
                    Email = user.Email,
                    Password = user.Password
                });

                return Ok(new TokenResponse { Jwt = token });
            }
            catch (NotExistingUserException)
            {
                return NotFound();
            }
            catch (IncorrectPasswordException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("users")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(409, "Email is busy.")]
        public IActionResult Post(UserPostDto user)
        {
            var userService = new UserService(_context);

            try
            {
                var createdUser = userService.Register(_mapper.Map<BlUser>(user));
                return Ok(_mapper.Map<UserDto>(createdUser));
            }
            catch (ExistingUserException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("users")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "User is not in database.")]
        [SwaggerResponse(409, "Email is busy.")]
        public IActionResult Patch(UserPatchDto user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);
            var userService = new UserService(_context);
            var postUser = new UserPostDto()
            {
                Id = userId,
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            try
            {
                var createdUser = userService.ChangeSettings(_mapper.Map<BlUser>(postUser), user.NewPassword);
                return Ok(_mapper.Map<UserDto>(createdUser));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ExistingUserException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("users/{userId}")]
        public IActionResult Delete(Int64 userId)
        {
            var userService = new UserService(_context);
            userService.Delete(userId);

            return Ok();
        }
    }
}
