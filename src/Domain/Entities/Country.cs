using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Country : BaseEntity
    {
        public required string Name { get; set; }
    }
}
