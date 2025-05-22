using iLoveIbadah.Application.DTOs.Tag;
using iLoveIbadah.Application.Features.Tags.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TagsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<TagsController>
        [HttpGet]
        [EndpointSummary("Get all tags")]
        [EndpointDescription("Get A List of all the tags")]
        [AllowAnonymous]
        public async Task<ActionResult<List<TagListDto>>> GetAll()
        {
            var tags = await _mediator.Send(new GetTagListRequest());
            return Ok(tags);
        }

        // GET api/<TagsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TagsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TagsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
