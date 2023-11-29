
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    internal class ApplicationIdentityUser : IdentityUser
    {
        public required User DomainUser { get; set; }
    }
}
