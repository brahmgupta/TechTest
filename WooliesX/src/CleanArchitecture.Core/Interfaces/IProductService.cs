using CleanArchitecture.Core.Domain;
using CleanArchitecture.Core.Enum;
using FluentResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Product>>> GetProducts(SortEnum sortOption, CancellationToken cancellationToken);
    }
}
