namespace Persistence.Entities.v1
{
    public class InsurancePolicy
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Guid TownId { get; set; }

        public string Town { get; set; }

        public string Type { get; set; }

        public string Purpose { get; set; }

        public string EngineType { get; set; }

        public EngineGroup EngineGroup { get; set; }

        public int? OwnerAge { get; set; }

        public OwnerGroup OwnerGroup { get; set; }

        public int? DamageCount { get; set; }
    }
}
