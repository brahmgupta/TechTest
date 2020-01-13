using CleanArchitecture.Core.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.SharedKernel.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> GetUser(CancellationToken cancellationToken);
    }
}
