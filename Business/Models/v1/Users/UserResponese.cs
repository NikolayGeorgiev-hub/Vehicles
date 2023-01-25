namespace Business.Models.v1.Users
{
    public class UserResponese : BaseResponse
    {
        public string Email { get; init; }

        public bool IsSignedIn { get; set; }
    }
}
