using CleanArchitecture.Core.Domain;
using CleanArchitecture.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;

namespace CleanArchitecture.Core.Services
{
    public class UserService: IUserService
    {
        public Task<Result<User>> GetUser(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return Results.Ok<User>(new User());
            });
        }
    }
}
