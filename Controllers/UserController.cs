using AutoMapper;
using EcommerceStore.DTOs;
using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace EcommerceStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Iusers users;
        private readonly ResponseDTO _responseDTO;
        private readonly Ijwt _ijwt;
        public UserController(IMapper mapper, Iusers _users, ResponseDTO responseDTO, Ijwt ijwt)
        {
            _mapper = mapper;
            _responseDTO = responseDTO;
            users = _users;
            _ijwt = ijwt;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> CreateUser(AddUserDTO newuser)
        {
            var user = _mapper.Map<User>(newuser);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var checkuser = users.GetUserByEmail(user.Email);
            if (checkuser != null)
            {
                return BadRequest("User found");
            }

            var usr = await users.CreateUserAsync(user);
            return Ok(_responseDTO);

            //if (usr != null)
            //{
            //    _responseDTO.Message = "You can not have duplicate emails. Enter unque field.";
            //    _responseDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
            //    return BadRequest(_responseDTO);
            //}
            //var newusr = await users.CreateUserAsync(user);
            //_responseDTO.Result = newusr;
            //return Ok(newusr);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> LoginUser(LogInDTO login)
        {
            //check if user exists
            var checker = await users.GetUserByEmail(login.Email);
            if (checker == null)
            {
                return BadRequest("User Exists");
            }
            var verify = BCrypt.Net.BCrypt.Verify(login.Password, checker.Password);
            if (!verify)
            {
                return BadRequest("Invalid");
            }
            var token = _ijwt.GenerateJwtToken(checker);
            return Ok(token);

        }

    }
}
