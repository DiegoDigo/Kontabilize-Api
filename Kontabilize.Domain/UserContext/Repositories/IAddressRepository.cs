using System;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Entities;

namespace Kontabilize.Domain.UserContext.Repositories
{
    public interface IAddressRepository
    {
        Task Save(Address address);
        Task Update(Address address);
        Task<Address> GetById(Guid id);
    }
}