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
    public class ProfileRepository : IProfileRepository
    {
        private readonly KontabilizeContext _context;

        public ProfileRepository(KontabilizeContext context)
        {
            _context = context;
        }
        public async Task<Profile> GetByUserId(Guid id) => await _context.Profiles.Where(ProfileQuery.FindByUserId(id)).AsNoTracking().Include(x => x.Address).FirstOrDefaultAsync();

        public async Task<Profile> GetById(Guid id) => await _context.Profiles.Where(ProfileQuery.FindById(id)).AsNoTracking().FirstOrDefaultAsync();


        public async Task Save(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
        }
    }
}