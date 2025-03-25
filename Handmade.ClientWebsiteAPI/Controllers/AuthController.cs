using Handmade.Application.Services.AuthServices;
using Handmade.DTOs.AuthDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handmade.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("buyer-register")]
        public async Task<IActionResult> BuyerRegister(ClientRegisterDTO clientRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<string> registerResult = await _authService.SendVerificationEmailAsync(clientRegisterDTO, "Buyer");
                if (registerResult.IsSuccess)
                {
                    return Ok(registerResult);
                }
                return BadRequest(registerResult.Msg);
            }
            return BadRequest(ModelState.ErrorCount);
        }

        [HttpPost("seller-login")]
        public async Task<IActionResult> SellerLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _authService.SellerLoginAsync(userLoginDTO);
            if (loginResult.IsSuccess)
            {
                return Accepted(loginResult);
            }
            else
            {
                return BadRequest(loginResult);
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            Console.WriteLine(token);
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid Token.");
            }
            ResultView<LoginResultDTO> loginResultDTO = await _authService.CreateUserAsync(token);
            if (loginResultDTO.IsSuccess)
            {
                return Ok(loginResultDTO);
            }
            return BadRequest(loginResultDTO);
        }

        [HttpPost("buyer-login")]
        public async Task<IActionResult> BuyerLogin(UserLoginDTO userLoginDTO)
        {
            ResultView<LoginResultDTO> loginResult = await _authService.BuyerLoginAsync(userLoginDTO);
            if (loginResult.IsSuccess)
            {
                return Accepted(loginResult);
            }
            else
            {
                return BadRequest(loginResult);
            }
        }
    }
}
