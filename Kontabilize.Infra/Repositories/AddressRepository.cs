using System;
using System.Linq;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Entities;
using Kontabilize.Domain.UserContext.Queries;
using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kontabilize.Infra.Repositories
{
    public class AddressRepository: IAddressRepository
    {
        private readonly KontabilizeContext _context;

        public AddressRepository(KontabilizeContext context)
        {
            _context = context;
        }
        public async Task<Address> GetById(Guid id) =>
            await _context.Addresses.Where(AddressQuery.FindById(id)).AsNoTracking().FirstOrDefaultAsync();

        public async Task Save(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
    }
}