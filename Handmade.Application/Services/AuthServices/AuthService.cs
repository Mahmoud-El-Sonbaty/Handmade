using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.AuthDTOs;
using Handmade.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Handmade.Application.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Handmade.Application.Services.EmailServices;
using Mapster;

namespace Handmade.Application.Services.AuthServices
{
    public class AuthService(IConfiguration configuration, IEmailService emailService,
        UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService> logger,
        RoleManager<IdentityRole<int>> roleManager) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ILogger<AuthService> _logger = logger;
        //private readonly IMapper _mapper = mapper; IMapper mapper,
        private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;

        public async Task<ResultView<string>> SendVerificationEmailAsync(ClientRegisterDTO clientRegisterDTO, string role)
        {
            ResultView<string> result = new();
            try
            {
                User? findUserEmail = await _userManager.FindByEmailAsync(clientRegisterDTO.Email);
                if (findUserEmail is null)
                {
                    //if (cUAccountDTO.ImageData is not null)
                    //{
                    //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(cUAccountDTO.ImageData.FileName);
                    //    if (!Directory.Exists(cUAccountDTO.ImagePath))
                    //    {
                    //        //Directory.CreateDirectory(cUAccountDTO.ImagePath);
                    //    }
                    //    string filePath = Path.Combine(cUAccountDTO.ImagePath, uniqueFileName);
                    //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    //    {
                    //        await cUAccountDTO.ImageData.CopyToAsync(fileStream);
                    //    }
                    //    cUAccountDTO.ImagePath = Path.Combine("/Images/Accounts/", uniqueFileName);
                    //}
                    var linkWithToken = $"https://localhost:7199/api/auth/verify-email?token={GenerateRegistrationToken(clientRegisterDTO, role)}";
                    await _emailService.SendEmailAsync(
                        clientRegisterDTO.Email,
                        "Email Verification",
                        $"Hi, Welcome To our handmade website, Please click the link below to activate your account\n{linkWithToken}\nPlease note that this link is valid for 10 minutes only.\nThanks For Your Interest"
                    );
                    result.IsSuccess = true;
                    result.Msg = $"A verification link has been sent to your email ({clientRegisterDTO.Email}). Please check your email to activate your account.";
                    result.Data = $"A verification link has been sent to your email ({clientRegisterDTO.Email}). Please check your email to activate your account.";
                    //User mappedUser = clientRegisterDTO.Adapt<User>();
                    //mappedUser.NormalizedEmail = clientRegisterDTO.Email.ToUpper();
                    //mappedUser.UserName = clientRegisterDTO.Email.Split("@")[0];
                    //mappedUser.NormalizedUserName = mappedUser.UserName.ToUpper();
                    //IdentityResult? userToCreate = await _userManager.CreateAsync(user: mappedUser, password: clientRegisterDTO.Password);
                    //if (userToCreate.Succeeded)
                    //{
                    //    User? createdUser = _userManager.Users.FirstOrDefault(u => u.Email == mappedUser.Email);
                    //    IdentityResult roleToAdd = await _userManager.AddToRoleAsync(createdUser, "Client");
                    //    if (roleToAdd.Succeeded)
                    //    {
                    //        resultView.IsSuccess = true;
                    //        resultView.Data = null;
                    //        resultView.Msg = $"Account ({clientRegisterDTO.Email}) Created Successfully, and verification link was sent to your email";
                    //    }
                    //    else
                    //    {
                    //        resultView.Msg = $"Account ({clientRegisterDTO.Email}) Not Created Because ({roleToAdd.Errors})";
                    //    }
                    //}
                    //else
                    //{
                    //    resultView.Msg = $"Account ({clientRegisterDTO.Email}) Not Created Because ({userToCreate.Errors})";
                    //}
                }
                else
                {
                    result.Msg = $"This Email ({findUserEmail.Email}) Already Exists, Please Login";
                }
            }
            catch (Exception ex)
            {
                result.Msg = $"Error Occured While Registering ({clientRegisterDTO.Email}), {ex.Message}";
            }
            return result;
        }

        public async Task<ResultView<LoginResultDTO>> CreateUserAsync(string token)
        {
            ResultView<LoginResultDTO> result = new();
            try
            {
                ResultView<VerifyRegisterTokenDTO> tokenVerificationResult = VerifyRegisterationToken(token);
                if (!tokenVerificationResult.IsSuccess)
                {
                    result.Msg = tokenVerificationResult.Msg;
                    return result;
                }
                VerifyRegisterTokenDTO verifyRegisterTokenDTO = tokenVerificationResult.Data!;
                User mappedUser = verifyRegisterTokenDTO.Adapt<User>();
                var userCreateResult = await _userManager.CreateAsync(mappedUser, verifyRegisterTokenDTO.Password);
                if (userCreateResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(mappedUser, verifyRegisterTokenDTO.Role.ToUpper());
                    User? newUser = await _userManager.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == verifyRegisterTokenDTO.Email);
                    if (newUser == null || !await _userManager.CheckPasswordAsync(newUser, verifyRegisterTokenDTO.Password))
                    {
                        result.Msg = "Invalid Credentials";
                        return result;
                    }
                    result.Msg = "Account successfully verified and created.";
                    LoginResultDTO loginResultDTO = new()
                    {
                        AccessToken = GenerateLoginToken(newUser.Id.ToString(), verifyRegisterTokenDTO.Email, mappedUser.UserName!, verifyRegisterTokenDTO.Role).Data!,
                        User = new()
                        {
                            Id = newUser.Id,
                            Email = verifyRegisterTokenDTO.Email,
                            FirstName = verifyRegisterTokenDTO.FirstName,
                            LastName = verifyRegisterTokenDTO.LastName
                        }
                    };
                    result.Data = loginResultDTO;
                    result.IsSuccess = true;
                    return result;
                }
                StringBuilder errorsStr = new();
                foreach (var err in userCreateResult.Errors)
                {
                    errorsStr.Append($"{err.Description} & ");
                }
                result.Msg = $"Failed to create user account. Because {string.Join("",errorsStr.ToString().SkipLast(3))}";
            }
            catch (SecurityTokenException)
            {
                result.Msg = "Invalid or expired token.";
            }
            return result;
        }


        //public Task<ResultView<GetAllAdminsDTO>> GetUserById(string id)
        //{
        //    ResultView<GetAllAdminsDTO> result = new();
        //    User? userExist = userManager.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(id));
        //    if (userExist is not null)
        //    {
        //        result.IsSuccess = true;
        //        result.Data = mapper.Map<GetAllAdminsDTO>(userExist);
        //        result.Msg = "User Fetched Successfully";
        //    }
        //    else
        //    {
        //        result.IsSuccess = false;
        //        result.Data = null;
        //        result.Msg = "User Not Found";
        //    }
        //    return Task.FromResult(result);
        //}

        //public async Task<ResultView<List<GetAllAdminsDTO>>> GetAllAsync(string role)
        //{
        //    ResultView<List<GetAllAdminsDTO>> result = new();
        //    IdentityRole<int>? adminRole = roleManager.Roles.FirstOrDefault(r => r.NormalizedName == role.ToUpper());
        //    if (adminRole is not null)
        //    {
        //        List<int> adminIdsList = [.. (await accountRoleRepository.GetAllAccountRolesAsync()).Where(ar => ar.RoleId == adminRole.Id).Select(ur => ur.UserId)];
        //        List<User> adminsList = [.. userManager.Users.Where(u => adminIdsList.Contains(u.Id))];
        //        List<GetAllAdminsDTO> getAllAdminsDTOs = mapper.Map<List<GetAllAdminsDTO>>(adminsList);
        //        result.IsSuccess = true;
        //        result.Data = getAllAdminsDTOs;
        //        result.Msg = "All Admins Fetched Successfully";
        //    }
        //    else
        //    {
        //        result.IsSuccess = false;
        //        result.Data = null;
        //        result.Msg = "No Role Named Admin Was Found";
        //    }
        //    return result;
        //}

        //public async Task<ResultView<EntityPaginated<GetAllAdminsDTO>>> GetAllPaginatedAsync(string role, int pageNumber, int pageSize)
        //{
        //    ResultView<EntityPaginated<GetAllAdminsDTO>> result = new();
        //    IdentityRole<int>? adminRole = roleManager.Roles.FirstOrDefault(r => r.NormalizedName == role.ToUpper());
        //    if (adminRole is not null)
        //    {
        //        List<int> adminIdsList = [.. (await accountRoleRepository.GetAllAccountRolesAsync()).Where(ar => ar.RoleId == adminRole.Id).Select(ur => ur.UserId)];
        //        List<User> adminsList = [.. userManager.Users.Where(u => adminIdsList.Contains(u.Id))];
        //        List<GetAllAdminsDTO> getAllAdminsDTOs = mapper.Map<List<GetAllAdminsDTO>>(adminsList);
        //        int totalUsers = userManager.Users.Where(u => adminIdsList.Contains(u.Id)).Count();
        //        result.IsSuccess = true;
        //        result.Data = new EntityPaginated<GetAllAdminsDTO>
        //        {
        //            Data = getAllAdminsDTOs,
        //            Count = totalUsers
        //        };
        //        result.Msg = "All Admins Fetched Successfully";
        //    }
        //    else
        //    {
        //        result.IsSuccess = false;
        //        result.Data = null;
        //        result.Msg = "No Role Named Admin Was Found";
        //    }
        //    return result;
        //}

        public List<IdentityRole<int>> GetRoles() => [.. _roleManager.Roles];

        // For Our Dashboard
        public async Task<ResultView<LoginResultDTO>> ModeratorLoginAsync(UserLoginDTO accountLoginDTO)
        {
            ResultView<LoginResultDTO> result = new();
            try
            {
                User? lookupUser = await _userManager.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == accountLoginDTO.Email);
                if (lookupUser == null || !await _userManager.CheckPasswordAsync(lookupUser, accountLoginDTO.Password))
                {
                    result.Msg = "Invalid Credentials";
                    return result;
                }
                List<IdentityRole<int>> allRoles = GetRoles();
                var clientRoleNames = new HashSet<string> { "BUYER", "SELLER" };
                var isMod = lookupUser.UserRoles?.Any(ur => !clientRoleNames.Contains(ur.Role.NormalizedName)) ?? false;
                if (!isMod)
                {
                    _logger.LogInformation("Access Denied: {FirstName} {LastName} is not a moderator", lookupUser.FirstName, lookupUser.LastName ?? "");
                    result.Msg = $"Access Denied: {lookupUser.FirstName} {lookupUser.LastName ?? ""} is not a moderator";
                    return result;
                }
                var roleNames = lookupUser.UserRoles?.Select(r => r?.Role?.Name).ToList();
                ResultView<string> tokenGenerationResult = GenerateLoginToken(lookupUser.Id.ToString(), lookupUser.Email!, lookupUser.UserName!, string.Join(" | ", roleNames??[""]));

                result.Data = new LoginResultDTO
                {
                    AccessToken = tokenGenerationResult.IsSuccess ? tokenGenerationResult.Data! : throw new Exception(tokenGenerationResult.Msg),
                    User = new SmallUserDTO
                    {
                        Id = lookupUser.Id,
                        FirstName = lookupUser.FirstName,
                        LastName = lookupUser.LastName,
                        Email = lookupUser.Email!
                    }
                };
                _logger.LogInformation("Moderator {FirstName} {LastName} Logged In Successfully", lookupUser.FirstName, lookupUser.LastName ?? "");
                result.IsSuccess = true;
                result.Msg = $"Moderator {lookupUser.FirstName} {lookupUser.LastName ?? ""} Logged In Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", accountLoginDTO.Email);
                result.Msg = "An error occurred during login. Please try again later.";
            }
            return result;
        }
        
        // For Sellers Dashboard
        public async Task<ResultView<LoginResultDTO>> SellerLoginAsync(UserLoginDTO accountLoginDTO)
        {
            ResultView<LoginResultDTO> result = new();
            try
            {
                User? lookupUser = await _userManager.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == accountLoginDTO.Email);
                if (lookupUser == null || !await _userManager.CheckPasswordAsync(lookupUser, accountLoginDTO.Password))
                {
                    result.Msg = "Invalid Credentials";
                    return result;
                }
                List<IdentityRole<int>> allRoles = GetRoles();
                var isSeller = lookupUser.UserRoles?.Any(ur => ur.Role?.NormalizedName == "SELLER") ?? false;
                if (!isSeller)
                {
                    _logger.LogInformation("Access Denied: {FirstName} {LastName} is not a seller", lookupUser.FirstName, lookupUser.LastName ?? "");
                    result.Msg = $"Access Denied: {lookupUser.FirstName} {lookupUser.LastName ?? ""} is not a seller";
                    return result;
                }
                ResultView<string> tokenGenerationResult = GenerateLoginToken(lookupUser.Id.ToString(), lookupUser.Email!, lookupUser.UserName!, "Seller");

                result.Data = new LoginResultDTO
                {
                    AccessToken = tokenGenerationResult.IsSuccess ? tokenGenerationResult.Data! : throw new Exception(tokenGenerationResult.Msg),
                    User = new SmallUserDTO
                    {
                        Id = lookupUser.Id,
                        FirstName = lookupUser.FirstName,
                        LastName = lookupUser.LastName,
                        Email = lookupUser.Email!
                    }
                };
                _logger.LogInformation("Seller {FirstName} {LastName} Logged In Successfully", lookupUser.FirstName, lookupUser.LastName ?? "");
                result.IsSuccess = true;
                result.Msg = $"Seller {lookupUser.FirstName} {lookupUser.LastName ?? ""} Logged In Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", accountLoginDTO.Email);
                result.Msg = "An error occurred during login. Please try again later.";
            }
            return result;
        }

        // For Clients
        public async Task<ResultView<LoginResultDTO>> BuyerLoginAsync(UserLoginDTO accountLoginDTO)
        {
            ResultView<LoginResultDTO> result = new();
            try
            {
                User? lookupUser = await _userManager.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == accountLoginDTO.Email);
                if (lookupUser == null || !await _userManager.CheckPasswordAsync(lookupUser, accountLoginDTO.Password))
                {
                    result.Msg = "Invalid Credentials";
                    return result;
                }
                List<IdentityRole<int>> allRoles = GetRoles();
                var isBuyer = lookupUser.UserRoles?.Any(ur => ur.Role.NormalizedName == "BUYER") ?? false;
                if (!isBuyer)
                {
                    _logger.LogInformation("Access Denied: {FirstName} {LastName} is not a buyer", lookupUser.FirstName, lookupUser.LastName ?? "");
                    result.Msg = $"Access Denied: {lookupUser.FirstName} {lookupUser.LastName ?? ""} is not a buyer";
                    return result;
                }
                ResultView<string> tokenGenerationResult = GenerateLoginToken(lookupUser.Id.ToString(), lookupUser.Email!, lookupUser.UserName!, "Buyer");
                
                result.Data = new LoginResultDTO
                {
                    AccessToken = tokenGenerationResult.IsSuccess ? tokenGenerationResult.Data! : throw new Exception(tokenGenerationResult.Msg),
                    User = new SmallUserDTO
                    {
                        Id = lookupUser.Id,
                        FirstName = lookupUser.FirstName,
                        LastName = lookupUser.LastName,
                        Email = lookupUser.Email!
                    }
                };
                _logger.LogInformation("Buyer {FirstName} {LastName} Logged In Successfully", lookupUser.FirstName, lookupUser.LastName ?? "");
                result.IsSuccess = true;
                result.Msg = $"Buyer {lookupUser.FirstName} {lookupUser.LastName ?? ""} Logged In Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", accountLoginDTO.Email);
                result.Msg = "An error occurred during login. Please try again later.";
            }
            return result;
        }

        //public async Task<ResultView<CUAccountDTO>> AddModerator(CUAccountDTO cUAccountDTO)
        //{
        //    ResultView<CUAccountDTO> resultView = new();
        //    try
        //    {
        //        User? findUserEmail = await userManager.FindByEmailAsync(cUAccountDTO.Email);
        //        if (findUserEmail is null)
        //        {
        //            User mappedUser = mapper.Map<User>(cUAccountDTO);
        //            mappedUser.NormalizedEmail = cUAccountDTO.Email.ToUpper();
        //            mappedUser.UserName = cUAccountDTO.Email.Split("@")[0];
        //            mappedUser.NormalizedUserName = mappedUser.UserName.ToUpper();
        //            IdentityResult? userToCreate = await userManager.CreateAsync(user: mappedUser, password: cUAccountDTO.Password);
        //            if (userToCreate.Succeeded)
        //            {
        //                User createdUser = userManager.Users.FirstOrDefault(u => u.Email == mappedUser.Email);
        //                IdentityResult roleToAdd = await userManager.AddToRoleAsync(mappedUser, "Admin"); // if didn't work replace mappedUser with createdUser
        //                if (roleToAdd.Succeeded)
        //                {
        //                    resultView.IsSuccess = true;
        //                    resultView.Data = cUAccountDTO;
        //                    resultView.Msg = $"Account ({cUAccountDTO.Email}) Created Successfully";
        //                }
        //                else
        //                {
        //                    resultView.IsSuccess = false;
        //                    resultView.Data = null;
        //                    resultView.Msg = $"Account ({findUserEmail.Email}) Not Created Because ({roleToAdd.Errors})";
        //                }
        //            }
        //            else
        //            {
        //                resultView.IsSuccess = false;
        //                resultView.Data = null;
        //                resultView.Msg = $"Account ({findUserEmail.Email}) Not Created Because ({userToCreate.Errors})";
        //            }
        //        }
        //        else
        //        {
        //            resultView.IsSuccess = false;
        //            resultView.Data = null;
        //            resultView.Msg = $"This Email ({findUserEmail.Email}) Already Exists, Please Login";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultView.IsSuccess = false;
        //        resultView.Data = null;
        //        resultView.Msg = $"Error Happen While Registering ({cUAccountDTO.Email}), {ex.Message}";
        //    }
        //    return resultView;
        //}

        //public async Task<ResultView<CUAccountDTO>> DeleteAsync(int userId)
        //{
        //    ResultView<CUAccountDTO> resultView = new();
        //    try
        //    {
        //        User? findUser = await userManager.FindByIdAsync($"{userId}");
        //        if (findUser is not null)
        //        {
        //            if ((await orderMasterRepository.GetAllAsync()).Any(om => om.ClientId == findUser.Id))
        //            {
        //                resultView.IsSuccess = false;
        //                resultView.Data = null;
        //                List<IdentityUserRole<int>> userRoles = [.. (await accountRoleRepository.GetAllAccountRolesAsync()).Where(iur => iur.RoleId != 4 && iur.UserId == findUser.Id)];
        //                if (userRoles.Count != 0)
        //                {
        //                    foreach (IdentityUserRole<int> role in userRoles)
        //                    {
        //                        await accountRoleRepository.DeleteAsync(role);
        //                    }
        //                }
        //                await accountRoleRepository.SaveChangesAsync();
        //                resultView.Msg = $"Cannot Delete Admin ({findUser.FirstName ?? findUser.UserName} {findUser.LastName ?? ""}) Because Of His/Her Orders Made Before, We Removed His/Her Role Instead";
        //                return resultView;
        //            }
        //            IdentityResult deleteUser = await userManager.DeleteAsync(findUser);
        //            if (deleteUser.Succeeded)
        //            {
        //                resultView.IsSuccess = true;
        //                resultView.Data = mapper.Map<CUAccountDTO>(findUser);
        //                resultView.Msg = $"Admin {findUser.FirstName ?? findUser.UserName} {findUser.LastName ?? ""} Deleted Successfully";
        //            }
        //            else
        //            {
        //                resultView.IsSuccess = false;
        //                resultView.Data = null;
        //                if (deleteUser.Errors.Any())
        //                {
        //                    StringBuilder stringBuilder = new StringBuilder();
        //                    foreach (IdentityError err in deleteUser.Errors)
        //                    {
        //                        stringBuilder.Append(err.Description + ',');
        //                    }
        //                    resultView.Msg = $"Couldn't Delete Admin {findUser.FirstName ?? findUser.UserName} {findUser.LastName ?? ""} Because Of The Following Errors: {stringBuilder}";
        //                }
        //                else
        //                {
        //                    resultView.Msg = $"Couldn't Delete Admin {findUser.FirstName ?? findUser.UserName} {findUser.LastName ?? ""}";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            resultView.IsSuccess = false;
        //            resultView.Data = null;
        //            resultView.Msg = $"User With Id ({userId}) Not Found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultView.IsSuccess = false;
        //        resultView.Data = null;
        //        resultView.Msg = $"Error Happened While Deleting User With Id ({userId}), {ex.Message}";
        //    }
        //    return resultView;
        //}

        // For Client
        
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public string GenerateRegistrationToken(ClientRegisterDTO clientRegisterDTO, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, clientRegisterDTO.FirstName),
                new Claim(ClaimTypes.Surname, clientRegisterDTO.LastName ?? string.Empty),
                new Claim(ClaimTypes.Email, clientRegisterDTO.Email),
                new Claim("Password", clientRegisterDTO.Password), // Store password as a custom claim
                new Claim(ClaimTypes.Role, role) // Store role as a custom claim
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:RegisterationTokenKey"])); // Use a secure key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10), // Token expires in 10 minutes
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ResultView<VerifyRegisterTokenDTO> VerifyRegisterationToken(string token)
        {
            ResultView<VerifyRegisterTokenDTO> result = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwt:RegisterationTokenKey"]);
            try
            {
                // Validate and decode the token
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                // Extract claims
                var firstName = principal.FindFirst(ClaimTypes.GivenName)?.Value;
                var lastName = principal.FindFirst(ClaimTypes.Surname)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var role = principal.FindFirst(ClaimTypes.Role)?.Value;
                var password = principal.FindFirst("Password")?.Value;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstName))
                {
                    return result;
                }
                result.Data = new VerifyRegisterTokenDTO()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Role = role
                };
                result.IsSuccess = true;
                return result;
            }
            catch (SecurityTokenException)
            {
                result.Msg = "Invalid or expired token.";
                return result;
            }
        }

        public ResultView<string> GenerateLoginToken(string id, string email, string username, string role)
        {
            ResultView<string> result = new();
            try
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, username),
                    new(ClaimTypes.Email, email),
                    new(ClaimTypes.PrimarySid, id),
                    new(ClaimTypes.Role, role)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["jwt:Issuer"],
                    audience: _configuration["jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                result.Data = new JwtSecurityTokenHandler().WriteToken(token);
                result.IsSuccess = true;
            }
            catch(Exception ex)
            {
                result.Msg = $"Unexpected error occured while generating token, {ex.Message}";
            }
            return result;
        }

        //public async Task<ResultView<ClientDetailsDTO>> GetClientDetails(int userId)
        //{
        //    ResultView<ClientDetailsDTO> result = new();
        //    try
        //    {
        //        User? userFindById = await userManager.FindByIdAsync($"{userId}");
        //        if (userFindById is not null)
        //        {
        //            result.IsSuccess = true;
        //            result.Data = mapper.Map<ClientDetailsDTO>(userFindById);
        //            result.Msg = "User Details Fetched Successfully";
        //        }
        //        else
        //        {
        //            result.Msg = "This User Doesn't Exist";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Msg = $"Error Happened While Getting Client Details, {ex.Message}.";
        //    }
        //    return result;
        //}
    }
}
