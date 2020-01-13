using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Web.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Core.Enum;

namespace CleanArchitecture.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("sort")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<ProductDTO>> GetUser(
            [FromQuery][Required]SortEnum sortOption,
            CancellationToken token)
        {
            var response = await _productService.GetProducts(sortOption, token);
            if (response.IsSuccess)
            {
                var mappedResponse = _mapper.Map<IList<ProductDTO>>(response.Value);
                return Ok(mappedResponse);
            }

            return StatusCode(500, "Error getting products");
        }
    }
}