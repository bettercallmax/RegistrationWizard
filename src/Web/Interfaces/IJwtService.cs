using Domain.Entities;

namespace Api.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(User user);
    }
}
