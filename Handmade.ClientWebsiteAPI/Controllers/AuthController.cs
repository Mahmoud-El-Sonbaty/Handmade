using Handmade.Application.Services.AuthServices;
using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handmade.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Client-Register")]
        public async Task<IActionResult> ClientRegister(ClientRegisterDTO clientRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<ClientRegisterDTO> registerResult = await _authService.ClientRegisterAsync(clientRegisterDTO);
                if (registerResult.IsSuccess)
                {
                    return Created();
                }
                return BadRequest(registerResult.Msg);
            }
            return BadRequest(ModelState.ErrorCount);
        }

        [HttpPost("Seller-Login")]
        public async Task<IActionResult> SellerLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _authService.SellerLoginAsync(userLoginDTO);
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
            ResultView<LoginResultDTO> loginResult = await _authService.ClientLoginAsync(userLoginDTO);
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
