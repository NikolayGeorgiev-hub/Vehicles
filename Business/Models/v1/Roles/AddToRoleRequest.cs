using System.ComponentModel.DataAnnotations;
using static Business.Constants;

namespace Business.Models.v1.Roles
{
    public class AddToRoleRequest
    {
        [Display(Name = "Role name")]
        [StringLength(maximumLength: 50, ErrorMessage = ValidationMessage.RangeErrorMessage, MinimumLength = 3)]
        [Required(ErrorMessage = ValidationMessage.RequiredErrorMessage)]
        public string Name { get; init; }

        [Display(Name = "User")]
        [Required(ErrorMessage = ValidationMessage.RequiredErrorMessage)]
        public Guid UserId { get; set; }
    }
}
