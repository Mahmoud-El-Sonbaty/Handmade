using Handmade.DTOs.AuthDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.AuthServices
{
    public interface IAuthService
    {
        //public Task<ResultView<GetAllAdminsDTO>> GetUserById(string id);
        //public Task<ResultView<List<GetAllAdminsDTO>>> GetAllAsync(string role);
        //public Task<ResultView<EntityPaginated<GetAllAdminsDTO>>> GetAllPaginatedAsync(string role, int pageNumber, int pageSize);
        public List<IdentityRole<int>> GetRoles();
        public Task<ResultView<LoginResultDTO>> ModeratorLoginAsync(UserLoginDTO accountLoginDTO);
        public Task<ResultView<LoginResultDTO>> SellerLoginAsync(UserLoginDTO accountLoginDTO);
        public Task<ResultView<LoginResultDTO>> BuyerLoginAsync(UserLoginDTO accountLoginDTO);
        public Task<ResultView<string>> SendVerificationEmailAsync(ClientRegisterDTO cUAccountDTO, string role);
        public Task<ResultView<LoginResultDTO>> CreateUserAsync(string token);
        public Task LogoutAsync();
        public string GenerateRegistrationToken(ClientRegisterDTO clientRegisterDTO, string role);
        public ResultView<VerifyRegisterTokenDTO> VerifyRegisterationToken(string token);
        public ResultView<string> GenerateLoginToken(string id, string email, string username, string role);
        //public Task<ResultView<CUAccountDTO>> AddModerator(CUAccountDTO cUAccountDTO);
        //public Task<ResultView<CUAccountDTO>> DeleteAsync(int userId);
        //public Task<ResultView<ClientDetailsDTO>> GetClientDetails(int userId);
    }
}
