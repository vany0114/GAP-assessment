using Gap.Infrastructure.DDD;
using System.Collections.Generic;

namespace Gap.Domain.Insurance.Model
{
    // this is an entity because it might exist a CRUD, for this example the values are created via ef seed.
    public class CoverageType : Entity
    {
        public string Name { get; }

        public string Description { get; }

        // ef navigation property
        public List<InsuranceCoverage> InsuranceCoverages { get; }

        internal CoverageType(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
