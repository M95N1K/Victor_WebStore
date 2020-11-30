using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain;
using Victor_WebStore.DAL;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.DTO.Identity;
using Microsoft.Extensions.Logging;

namespace Victor_WebStore.ServicesHost.Controllers.Identity
{
    [Route(WebApiAddress.Identity.User)]
    [ApiController]
    public class UsersApiController : ControllerBase
{
        private readonly UserStore<User, IdentityRole, WebStoreContext> _UserStore;
        private readonly ILogger<UsersApiController> _logger;

        public UsersApiController(WebStoreContext db, ILogger<UsersApiController> logger)
        {
            _UserStore = new UserStore<User, IdentityRole, WebStoreContext>(db);
            _logger = logger;
        }

        [HttpGet("all")] // api/users/all
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = await _UserStore.Users.ToArrayAsync();
            _logger.LogDebug($"Get All Users: Count - {result.Length}");
            return result;
        }

        #region Users

        [HttpPost("UserId")] // POST: api/users/UserId
        public async Task<string> GetUserIdAsync([FromBody] User user)
        {
            var respone = await _UserStore.GetUserIdAsync(user);
            _logger.LogDebug($"Get UserID: User - {user.UserName}, ID - {(respone)}");
            return respone;
        }

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user)
        {
            var respone = await _UserStore.GetUserNameAsync(user);
            _logger.LogDebug($"Get UserName: User - {user.UserName}, UserName - {(respone)}");

            return respone;
        }

        [HttpPost("UserName/{name}")] // api/users/UserName/TestUser
        public async Task<string> SetUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetUserNameAsync(user, name);
            var respone = await _UserStore.UpdateAsync(user);
            if (!respone.Succeeded)
            {
                _logger.LogError($"Error Set UserName\n Old name: {user.UserName}, New name: {name}");
                foreach (var item in respone.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else
            {
                _logger.LogInformation($"Set user name OK!");
            }

            return user.UserName;
        }

        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user)
        {
            var respone = await _UserStore.GetNormalizedUserNameAsync(user);
            _logger.LogDebug($"Get Normalize UserName: User - {user.UserName}, NormalizeUserName - {respone}");
            return respone;
        }

        [HttpPost("NormalUserName/{name}")]
        public async Task<string> SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            await _UserStore.SetNormalizedUserNameAsync(user, name);
            var respone = await _UserStore.UpdateAsync(user);

            if (!respone.Succeeded)
            {
                _logger.LogError($"Error Set NormalizedUserName\n Old name: {user.UserName}, New name: {(name)}");
                foreach (var item in respone.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else _logger.LogInformation($"Set NormalizedUserName OK!");

            return user.NormalizedUserName;
        }

        [HttpPost("User")] // api/users/user
        public async Task<bool> CreateAsync([FromBody] User user)
        {
            var creation_result = await _UserStore.CreateAsync(user);
            // добавление ошибок создания нового пользователя в журнал

            if (!creation_result.Succeeded)
            {
                _logger.LogError($"Error create user\n Old name: {user.UserName}");
                foreach (var item in creation_result.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else _logger.LogInformation($"Create user OK!");

            return creation_result.Succeeded;
        }

        [HttpPut("User")]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            var update_result = await _UserStore.UpdateAsync(user);

            if(!update_result.Succeeded)
            {
                _logger.LogError($"Error Update user: User - {user.UserName}");
                foreach (var item in update_result.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else _logger.LogInformation($"Update user OK!");

            return update_result.Succeeded;
        }

        [HttpPost("User/Delete")]
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            var delete_result = await _UserStore.DeleteAsync(user);

            if (!delete_result.Succeeded)
            {
                _logger.LogError($"Error Delete user: User - {user.UserName}");
                foreach (var item in delete_result.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else _logger.LogInformation($"Delete user OK!");

            return delete_result.Succeeded;
        }

        [HttpGet("User/Find/{id}")] // api/users/user/Find/9E5CB5E7-41DE-4449-829E-45F4C97AA54B
        public async Task<User> FindByIdAsync(string id)
        {
            User result = await _UserStore.FindByIdAsync(id);
            _logger.LogDebug($"Find By ID: ID - {id}, Found User - {result.UserName}");
            return result;
        }

        [HttpGet("User/Normal/{name}")] // api/users/user/Normal/TestUser
        public async Task<User> FindByNameAsync(string name)
        {
            User result = await _UserStore.FindByNameAsync(name);
            _logger.LogDebug($"Find By Name: Name - {name}, Found User - {result.UserName}");
            return result;
        }

        [HttpPost("Role/{role}")]
        public async Task AddToRoleAsync([FromBody] User user, string role, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddToRoleAsync(user, role);
            int result = await db.SaveChangesAsync();
            _logger.LogDebug($"Add Role - {role} to User - {user.UserName} : Code {result}");
        }

        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveFromRoleAsync(user, role);
            int result = await db.SaveChangesAsync();
            _logger.LogDebug($"Add Role - {role} to User - {user.UserName} : Code {result}");
        }

        [HttpPost("Roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody] User user)
        {
            IList<string> result = await _UserStore.GetRolesAsync(user);
            _logger.LogDebug($"Get Roles: Count {result.Count}");
            return result;
        }

        [HttpPost("InRole/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role)
        {
            bool result = await _UserStore.IsInRoleAsync(user, role);
            _logger.LogDebug($"Is In Role: User - {user.UserName}, Role - {role}");
            return result;
        }

        [HttpGet("UsersInRole/{role}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string role)
        {
            IList<User> result = await _UserStore.GetUsersInRoleAsync(role);
            _logger.LogDebug($"Users In Role: Role - {role}, Count Users - {result.Count}");
            return result;
        }

        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user)
        {
            string result = await _UserStore.GetPasswordHashAsync(user);
            _logger.LogDebug($"Get Password Hash: User - {user.UserName}");
            return result;
        }

        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody] PasswordHashDTO hash)
        {
            await _UserStore.SetPasswordHashAsync(hash.User, hash.Hash);
            IdentityResult result = await _UserStore.UpdateAsync(hash.User);
            if(!result.Succeeded)
            {
                _logger.LogError($"Error SetPasswordHash: User - {hash.User.UserName}, Hash - {hash.Hash}");
                foreach (var item in result.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else
            {
                _logger.LogInformation($"SetPasswordHash OK!: User - {hash.User.UserName}");
            }
            return hash.User.PasswordHash;
        }

        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user)
        {
            bool result = await _UserStore.HasPasswordAsync(user);
            _logger.LogDebug($"HasPassword: User ({user.UserName}) - {result}");
            return result;
        }

        #endregion

        #region Claims

        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user)
        {
            IList<Claim> result = await _UserStore.GetClaimsAsync(user);
            _logger.LogDebug($"GetClaims: User - {user.UserName}, Count - {result.Count}");
            return result;
        }

        [HttpPost("AddClaims")]
        public async Task AddClaimsAsync([FromBody] AddClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            var respone = await db.SaveChangesAsync();
            _logger.LogDebug($"Add Claims: Code - {respone}");
        }

        [HttpPost("ReplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.ReplaceClaimAsync(ClaimInfo.User, ClaimInfo.Claim, ClaimInfo.NewClaim);
            var respone = await db.SaveChangesAsync();
            _logger.LogDebug($"Replace Claim: Code - {respone}");
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimsAsync([FromBody] RemoveClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            var respone = await db.SaveChangesAsync();
            _logger.LogDebug($"Remove Claimss Code - {respone}");
        }

        [HttpPost("GetUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim)
        {
            IList<User> result = await _UserStore.GetUsersForClaimAsync(claim);
            _logger.LogDebug($"GetUsersForClaim: Count User - {result.Count}");
            return result;
        }

        #endregion

        #region TwoFactor

        [HttpPost("GetTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody] User user)
        {
            bool result = await _UserStore.GetTwoFactorEnabledAsync(user);
            _logger.LogDebug($"GetTwoFactorEnabled: User - {user.UserName}, IsEnabled - {result}");
            return result;
        }

        [HttpPost("SetTwoFactor/{enable}")]
        public async Task<bool> SetTwoFactorEnabledAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetTwoFactorEnabledAsync(user, enable);
            IdentityResult result = await _UserStore.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError($"SetTwoFactor: User - {user.UserName}, Enable - {enable}");
                foreach (var item in result.Errors)
                {
                    _logger.LogError($"\tError: {item.Code} - {item.Description}");
                }
            }
            else
            {
                _logger.LogInformation($"SetTwoFactor OK!: User - {user.UserName}, Enable - {enable}");
            }
            return user.TwoFactorEnabled;
        }

        #endregion

        /*------------------------------------------------------------------------------------------*/

        #region Email/Phone

        [HttpPost("GetEmail")]
        public async Task<string> GetEmailAsync([FromBody] User user) => await _UserStore.GetEmailAsync(user);

        [HttpPost("SetEmail/{email}")]
        public async Task<string> SetEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
            return user.Email;
        }

        [HttpPost("GetEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody] User user) => await _UserStore.GetEmailConfirmedAsync(user);

        [HttpPost("SetEmailConfirmed/{enable}")]
        public async Task<bool> SetEmailConfirmedAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetEmailConfirmedAsync(user, enable);
            await _UserStore.UpdateAsync(user);
            return user.EmailConfirmed;
        }

        [HttpGet("UserFindByEmail/{email}")]
        public async Task<User> FindByEmailAsync(string email) => await _UserStore.FindByEmailAsync(email);

        [HttpPost("GetNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody] User user) => await _UserStore.GetNormalizedEmailAsync(user);

        [HttpPost("SetNormalizedEmail/{email?}")]
        public async Task<string> SetNormalizedEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetNormalizedEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
            return user.NormalizedEmail;
        }

        [HttpPost("GetPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody] User user) => await _UserStore.GetPhoneNumberAsync(user);

        [HttpPost("SetPhoneNumber/{phone}")]
        public async Task<string> SetPhoneNumberAsync([FromBody] User user, string phone)
        {
            await _UserStore.SetPhoneNumberAsync(user, phone);
            await _UserStore.UpdateAsync(user);
            return user.PhoneNumber;
        }

        [HttpPost("GetPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user) =>
            await _UserStore.GetPhoneNumberConfirmedAsync(user);

        [HttpPost("SetPhoneNumberConfirmed/{confirmed}")]
        public async Task<bool> SetPhoneNumberConfirmedAsync([FromBody] User user, bool confirmed)
        {
            await _UserStore.SetPhoneNumberConfirmedAsync(user, confirmed);
            await _UserStore.UpdateAsync(user);
            return user.PhoneNumberConfirmed;
        }

        #endregion

        #region Login/Lockout

        [HttpPost("AddLogin")]
        public async Task AddLoginAsync([FromBody] AddLoginDTO login, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddLoginAsync(login.User, login.UserLoginInfo);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveLogin/{LoginProvider}/{ProviderKey}")]
        public async Task RemoveLoginAsync([FromBody] User user, string LoginProvider, string ProviderKey, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveLoginAsync(user, LoginProvider, ProviderKey);
            await db.SaveChangesAsync();
        }

        [HttpPost("GetLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user) => await _UserStore.GetLoginsAsync(user);

        [HttpGet("User/FindByLogin/{LoginProvider}/{ProviderKey}")]
        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey) => await _UserStore.FindByLoginAsync(LoginProvider, ProviderKey);

        [HttpPost("GetLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user) => await _UserStore.GetLockoutEndDateAsync(user);

        [HttpPost("SetLockoutEndDate")]
        public async Task<DateTimeOffset?> SetLockoutEndDateAsync([FromBody] SetLockoutDTO LockoutInfo)
        {
            await _UserStore.SetLockoutEndDateAsync(LockoutInfo.User, LockoutInfo.LockoutEnd);
            await _UserStore.UpdateAsync(LockoutInfo.User);
            return LockoutInfo.User.LockoutEnd;
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
        {
            var count = await _UserStore.IncrementAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
            return count;
        }

        [HttpPost("ResetAccessFailedCount")]
        public async Task<int> ResetAccessFailedCountAsync([FromBody] User user)
        {
            await _UserStore.ResetAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
            return user.AccessFailedCount;
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody] User user) => await _UserStore.GetAccessFailedCountAsync(user);

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody] User user) => await _UserStore.GetLockoutEnabledAsync(user);

        [HttpPost("SetLockoutEnabled/{enable}")]
        public async Task<bool> SetLockoutEnabledAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetLockoutEnabledAsync(user, enable);
            await _UserStore.UpdateAsync(user);
            return user.LockoutEnabled;
        }

        #endregion
    }
}
