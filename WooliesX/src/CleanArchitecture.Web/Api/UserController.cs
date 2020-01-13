using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.SharedKernel.Interfaces;
using CleanArchitecture.Web.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("user")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<UserDTO>> GetUser(CancellationToken token)
        {
            var response = await _userService.GetUser(token);

            if (response.IsSuccess)
            {
                var mappedResponse = _mapper.Map<UserDTO>(response.Value);
                return Ok(mappedResponse);
            }

            return StatusCode(500, "Error getting User");
        }
    }
}