using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<IdentityRole> { }
}
