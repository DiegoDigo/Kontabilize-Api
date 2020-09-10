using System;
using System.Threading.Tasks;
using Kontabilize.Shared.Command;

namespace Kontabilize.Domain.UserContext.Services
{
    public interface IProfileService
    {
        Task<CommandResult> GetProfileByIdUser(Guid id);
    }
}