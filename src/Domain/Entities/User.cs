using Domain.Entities.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Login { get; set; }
        public required Province Province { get; init; }
    }
}
