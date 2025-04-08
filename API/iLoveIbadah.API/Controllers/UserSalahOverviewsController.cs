using iLoveIbadah.Application.Features.UserSalahOverviews.Requests.Queries;
using iLoveIbadah.Domain;
using iLoveIbadah.Application.Constants;
using iLoveIbadah.Application.DTOs.UserSalahOverview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSalahOverviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSalahOverviewsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<UserSalahOverviewsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<UserSalahOverviewListDto>>> GetAll()
        {
            var userSalahOverviews = await _mediator.Send(new GetUserSalahOverviewListRequest());
            return userSalahOverviews;
        }

        // GET api/<UserSalahOverviewsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSalahOverviewDto>> GetById(int id)
        {
            var userSalahOverview = await _mediator.Send(new GetUserSalahOverviewDetailsRequest { Id = id });
            return Ok(userSalahOverview);
        }

        // GET api/<UserSalahOverviewsController>/getbyuseraccount
        [HttpGet("getbyuseraccount")]
        [Authorize]
        public async Task<ActionResult<UserSalahOverviewDto>> GetByUserAccount()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Id.ToString())?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }
            var userSalahOverview = await _mediator.Send(new GetUserSalahOverviewByUserAccountDetailsRequest { UserAccountId = int.Parse(userIdClaim) });
            return Ok(userSalahOverview);
        }

        // POST api/<UserSalahOverviewsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserSalahOverviewsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserSalahOverviewsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
