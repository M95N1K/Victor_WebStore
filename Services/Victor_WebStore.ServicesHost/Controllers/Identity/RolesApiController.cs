using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Victor_WebStore.Domain;
using Victor_WebStore.DAL;
using WebStore.Domain;
using Victor_WebStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebApiAddress.Identity.Roles)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<IdentityRole> _RoleStore;

        public RolesApiController(WebStoreContext db)
        {
            _RoleStore = new RoleStore<IdentityRole>(db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<IdentityRole>> GetAllRoles() => await _RoleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(IdentityRole role)
        {
            var creation_result = await _RoleStore.CreateAsync(role);
            return creation_result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(IdentityRole role)
        {
            var uprate_result = await _RoleStore.UpdateAsync(role);
            return uprate_result.Succeeded;
        }

        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(IdentityRole role)
        {
            var delete_result = await _RoleStore.DeleteAsync(role);
            return delete_result.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] IdentityRole role) => await _RoleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] IdentityRole role) => await _RoleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync(IdentityRole role, string name)
        {
            await _RoleStore.SetRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role) => await _RoleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync(IdentityRole role, string name)
        {
            await _RoleStore.SetNormalizedRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<IdentityRole> FindByIdAsync(string id) => await _RoleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<IdentityRole> FindByNameAsync(string name) => await _RoleStore.FindByNameAsync(name);
    }
}
