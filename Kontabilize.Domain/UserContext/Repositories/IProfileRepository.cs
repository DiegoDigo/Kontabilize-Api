using System;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Repositories
{
    public interface IProfileRepository
    {
        Task Save(Profile profile);
        Task<Profile> GetByUserId(Guid id);
        Task<Profile> GetById(Guid id);
        Task Update(Profile profile);
    }
}