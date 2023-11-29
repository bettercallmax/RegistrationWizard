using DomainUser = Domain.Entities.User;

namespace Api.Models.User
{
    public record UserModel(string Login, string Province, string Token)
    {
        public static UserModel FromDomain(DomainUser user, string token) => new(user.Login, user.Province.Name, token);
    }
}
