using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using static Business.Constants.ValidationMessage;

namespace Business.Models.v1.Users
{
    public class UserRegistrationRequest
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(maximumLength: 50, ErrorMessage = RangeErrorMessage, MinimumLength = 3)]
        public string UserName { get; set; }

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
