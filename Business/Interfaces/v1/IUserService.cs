using Business.Models.v1.Roles;
using Business.Models.v1.Users;

namespace Business.Interfaces.v1
{
    public interface IUserService
    {
        Task<UserResponese> CreateUserAsync(UserRegistrationRequest requestUser);

        Task<RoleResponse> CreateNewRoleAsync(RoleRequest requestRole);

        Task<AddToRoleResponse> SetUserRoleAsync(AddToRoleRequest roleRequest);

        Task<UserResponese> SignInAsync(UserLoginRequest loginRequest);
    }
}
