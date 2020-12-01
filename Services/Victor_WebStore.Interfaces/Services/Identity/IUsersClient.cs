using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Interfaces.Services.Identity
{
    public interface IUsersClient :
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserClaimStore<User>,
        IUserLoginStore<User>
    {
    }
}
