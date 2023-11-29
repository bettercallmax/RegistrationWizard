using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Province : BaseEntity
    {
        public required string Name { get; set; }
        public required Country Country { get; set; }
    }
}
