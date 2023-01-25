using System.ComponentModel.DataAnnotations;
using static Business.Constants;

namespace Business.Models.v1.Roles
{
    public class RoleRequest
    {
        [Display(Name = "Role name")]
        [StringLength(maximumLength: 50, ErrorMessage = ValidationMessage.RangeErrorMessage,MinimumLength = 3)]
        [Required(ErrorMessage = ValidationMessage.RequiredErrorMessage)]
        public string RoleName { get; set; }

    }
}
