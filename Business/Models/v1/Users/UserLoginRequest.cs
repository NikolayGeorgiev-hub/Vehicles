using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using static Business.Constants.ValidationMessage;

namespace Business.Models.v1.Users
{
    public class UserLoginRequest
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = InvalidEmailErrorMessage)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Email { get; set; }

        [Display(Name = "Pasword")]
        [PasswordPropertyText]
        [StringLength(maximumLength: 50, ErrorMessage = RangeErrorMessage, MinimumLength = 5)]
        [Required(ErrorMessage = RequiredErrorMessage)]

        public string Password { get; set; }

    }
}
