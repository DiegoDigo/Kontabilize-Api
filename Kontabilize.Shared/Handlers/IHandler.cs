using System.Threading.Tasks;
using Kontabilize.Shared.Command;

namespace Kontabilize.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        Task<CommandResult> Handler(T command);
    }
}