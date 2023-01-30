using Business.Interfaces.v1;
using Microsoft.Extensions.Logging;
using Persistence.Entities.v1;
using Persistence.Interfaces.v1;
using Vehicles.Data.Interfaces.v1;

using static Business.Constants;

namespace Business.Implementations.v1
{
    public class InsuranceService : IInsuaranceService
    {
        private readonly ILogger<InsuranceService> _logger;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IInsuaranceRepository _insuaranceRepository;

        public InsuranceService(
            ILogger<InsuranceService> logger,
            IVehicleRepository vehicleRepository, IInsuaranceRepository insuaranceRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
            _insuaranceRepository = insuaranceRepository;
        }

        public async Task<InsurancePolicy> CreateAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle is null)
            {
                _logger.Log(LogLevel.Information, string.Format(VehicleMessages.NotFound, id));
                throw new ArgumentNullException(string.Format(VehicleMessages.NotFound, id));
            }

            var engineCategory = this.CreateEngineCategory(vehicle.EngineCapacity, vehicle.VehicleType);

            var insuarance = new InsurancePolicy
            {
                Number = this.CreateRandomNUmber(),
                CreatedOn = DateTime.Now,
                ExpiresOn = DateTime.Now.AddYears(1),
                TownId = vehicle.TownId,
                Town = vehicle.Town.Name,
                Type = vehicle.VehicleType.Type,
                Purpose = vehicle.Purpose.ToString(),
                EngineType = engineCategory.Description,
                EngineGroup = engineCategory.Group,
                OwnerAge = 30,
                OwnerGroup = this.CreateOwnerCategory(30).OwnerGroup
            };

            await _insuaranceRepository.AddAsync(insuarance);
            await _insuaranceRepository.SaveAsync();

            _logger.Log(LogLevel.Information, string.Format(VehicleMessages.SuccessfulCreate, insuarance.Number));
            return insuarance;


        }

        private string CreateRandomNUmber()
        {
            Random generator = new Random();
            string policyNumber = "380003562";
            policyNumber += generator.Next(0, 0000000).ToString("D" + (13 - policyNumber.Length).ToString());

            return policyNumber;
        }

        private EngineCategory CreateEngineCategory(int engineCapacity, VehicleType vehicleType)
        {
            var engineCategory = new EngineCategory();

            if (engineCapacity > 1500 && engineCapacity <= 2000)
            {
                engineCategory.Description = $"{vehicleType.Type} over 1500 to 2000";
                engineCategory.Group = EngineGroup.Medium;

            }

            if (engineCapacity <= 1500)
            {
                engineCategory.Description = $"{vehicleType.Type} to 1500";
                engineCategory.Group = EngineGroup.Small;
            }

            if (engineCapacity > 2000)
            {
                engineCategory.Description = $"{vehicleType.Type} over 2000";
                engineCategory.Group = EngineGroup.Large;

            }

            return engineCategory;
        }

        private OwnerCategory CreateOwnerCategory(int? ownerAge)
        {
            var ownerCategory = new OwnerCategory();

            if (ownerAge is null)
            {
                ownerCategory.OwnerGroup = OwnerGroup.None;
                return ownerCategory;
            }

            if (ownerAge > 18 && ownerAge <= 25)
            {
                ownerCategory.OwnerGroup = OwnerGroup.First;
            }

            if (ownerAge > 25 && ownerAge <= 50)
            {
                ownerCategory.OwnerGroup = OwnerGroup.Second;
            }

            if (ownerAge > 50)
            {
                ownerCategory.OwnerGroup = OwnerGroup.Third;
            }

            return ownerCategory;
        }
    }

    public class EngineCategory
    {
        public string Description { get; set; }

        public EngineGroup Group { get; set; }
    }

    public class OwnerCategory
    {
        public OwnerGroup OwnerGroup { get; set; }
    }
}
