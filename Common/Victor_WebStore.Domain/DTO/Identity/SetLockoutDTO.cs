using System;

namespace Victor_WebStore.Domain.DTO.Identity
{
    public class SetLockoutDTO : UserDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
