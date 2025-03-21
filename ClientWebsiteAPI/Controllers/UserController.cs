using Handmade.Application.Services.UserServices;
using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Moderator-Login")]
        public async Task<IActionResult> ModeratorLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _userService.ModeratorLoginAsync(userLoginDTO);
            if (loginResult.IsSuccess)
            {
                return Accepted(loginResult);
            }
            else
            {
                return BadRequest(loginResult.Msg);
            }
        }

        [HttpPost("Seller-Login")]
        public async Task<IActionResult> SellerLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _userService.SellerLoginAsync(userLoginDTO);
            if (loginResult.IsSuccess)
            {
                return Accepted(loginResult);
            }
            else
            {
                return BadRequest(loginResult.Msg);
            }
        }

        [HttpPost("Client-Login")]
        public async Task<IActionResult> ClientLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _userService.ClientLoginAsync(userLoginDTO);
            if (loginResult.IsSuccess)
            {
                return Accepted(loginResult);
            }
            else
            {
                return BadRequest(loginResult.Msg);
            }
        }
    }
}
