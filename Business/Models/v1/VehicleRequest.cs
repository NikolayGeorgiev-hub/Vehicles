using Persistence.Entities.v1;
using System.ComponentModel.DataAnnotations;
using static Business.Constants;

namespace Business.Models.v1
{
    public class VehicleRequest
    {
        [Display(Name = "Engine Capacity")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(minimum: 800, maximum: 5000, ErrorMessage = RangeErrorMessage)]
        public int EngineCapacity { get; set; }

        [Display(Name = "Vehicle Age")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(minimum: 1, maximum: 50, ErrorMessage = RangeErrorMessage)]
        public int VehicleAge { get; set; }

        [Display(Name = "Vehical Type")]
        [Required(ErrorMessage = RangeErrorMessage)]
        public Guid VehicleTypeId { get; set; }

        [Display(Name = "Town ID")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public Guid TownId { get; set; }


        [Display(Name = "Purpose")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [EnumDataType(typeof(Purpose),ErrorMessage = EnumTypeErrorMessage)]
        public Purpose Purpose { get; set; }

    }
}
