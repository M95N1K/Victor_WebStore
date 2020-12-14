using Microsoft.AspNetCore.Identity;

namespace Victor_WebStore.Domain.DTO.Identity
{
    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
