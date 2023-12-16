using AutoMapper;
using EcommerceStore.DTOs;
using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Iusers users;
        private readonly ResponseDTO _responseDTO;
        public UserController(IMapper mapper, Iusers _users, ResponseDTO responseDTO)
        {
            _mapper = mapper;
            _responseDTO = responseDTO;
            users = _users;
        }
        public async Task<ActionResult<ResponseDTO>> CreateUser(AddUserDTO newuser)
        {
            var user = _mapper.Map<User>(newuser);
            var usr = await users.GetUserByEmail(newuser.Email);
            if (usr != null)
            {
                _responseDTO.Message = "You can not have duplicate emails. Enter unque field.";
                _responseDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_responseDTO);
            }
            var newusr = await users.CreateUserAsync(user);
            _responseDTO.Result = newusr;
            return Ok(newusr);
        }
    }
}
