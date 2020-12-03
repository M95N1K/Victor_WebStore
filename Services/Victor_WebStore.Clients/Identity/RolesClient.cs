using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Victor_WebStore.Clients.Base;
using Victor_WebStore.Domain;
using Victor_WebStore.Interfaces.Services.Identity;


namespace Victor_WebStore.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(IConfiguration Configuration) : base(Configuration, WebApiAddress.Identity.Roles) { }

        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync(_ServiceAddress, role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PutAsync(_ServiceAddress, role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/Delete", role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleId", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleName", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{_ServiceAddress}/SetRoleName/{name}", role, cancel);
            role.Name = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetNormalizedRoleName", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{_ServiceAddress}/SetNormalizedRoleName/{name}", role, cancel);
            role.NormalizedName = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<IdentityRole> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{_ServiceAddress}/FindById/{id}", cancel);

        public async Task<IdentityRole> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{_ServiceAddress}/FindByName/{name}", cancel);

        #endregion
    }
}
