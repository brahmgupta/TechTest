using CleanArchitecture.Core.Api;
using CleanArchitecture.Core.Domain;
using CleanArchitecture.SharedKernel.AppSettings;
using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CleanArchitecture.Core.Enum;
using System.Linq;

namespace CleanArchitecture.Core.Services
{
    public class ProductService : BaseHttp, IProductService
    {
        private ProductAPISettings _productAPISettings;
        public ProductService(HttpClient client, IOptions<ProductAPISettings> productAPISettings) : base(client)
        {
            _productAPISettings = productAPISettings.Value;
        }

        public async Task<Result<IEnumerable<Product>>> GetProducts(SortEnum sortOption, CancellationToken cancellationToken)
        {
            if (sortOption.Equals(SortEnum.Recommended)) {
                return await GetRecommendedProducts(cancellationToken);
            }

            return await GetAllProducts(sortOption, cancellationToken);
        }

        public async Task<Result<IEnumerable<Product>>> GetRecommendedProducts(CancellationToken cancellationToken)
        {
            var httpResponse = await base.Get(_productAPISettings.ShopperHistoryUri, cancellationToken);

            try
            {
                var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(httpResponse);

                var products = customers
                    .SelectMany(c => c.Products)
                    .GroupBy(p => p.Name)
                    .Select(group => new { key = group.Key, Count = group.Count(), product = group.FirstOrDefault() })
                    .OrderByDescending(c => c.Count)
                    .Select(p => p.product);

                return Results.Ok(products);
            }
            catch (Exception ex)
            {
                return Results.Fail<IEnumerable<Product>>(new Error(ex.Message));
            }
        }

        public async Task<Result<IEnumerable<Product>>> GetAllProducts(SortEnum sortOption, CancellationToken cancellationToken)
        {
            var httpResponse = await base.Get(_productAPISettings.ProductsUri, cancellationToken);

            try
            {
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpResponse);
                var sortedProducts = SortedProducts(sortOption, products);

                return Results.Ok(sortedProducts);
            }
            catch (Exception ex)
            {
                return Results.Fail<IEnumerable<Product>>(new Error(ex.Message));
            }
        }

        public IEnumerable<Product> SortedProducts(SortEnum sortOption, IEnumerable<Product> products)
        {
            switch (sortOption)
            {
                case SortEnum.Low: return products.OrderBy(p => p.Price);
                case SortEnum.High: return products.OrderByDescending(p => p.Price);
                case SortEnum.Ascending: return products.OrderBy(p => p.Name);
                case SortEnum.Descending: return products.OrderByDescending(p => p.Name);
                default: return products;
            }
        }
    }
}

