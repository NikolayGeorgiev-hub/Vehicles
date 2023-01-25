namespace Business.Models.v1.Roles
{
    public class AddToRoleResponse : BaseResponse
    {
        public string RoleName { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> UserRoles { get; set; }

    }
}
