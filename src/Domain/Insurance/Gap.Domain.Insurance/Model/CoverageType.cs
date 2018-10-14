using Gap.Infrastructure.DDD;
using System.Collections.Generic;

namespace Gap.Domain.Insurance.Model
{
    // this is an entity because it might exist a CRUD, for this example the values are created via ef seed.
    public class CoverageType : Entity
    {
        // EF doesn't support auto-properties readonly to run migrations
        private string _name;
        private string _description;
        private List<InsuranceCoverage> _insuranceCoverages;

        public string Name => _name;

        public string Description => _description;

        // ef navigation property
        public List<InsuranceCoverage> InsuranceCoverages => _insuranceCoverages;

        internal CoverageType(int id, string name, string description)
        {
            Id = id;
            _name = name;
            _description = description;
        }
    }
}
