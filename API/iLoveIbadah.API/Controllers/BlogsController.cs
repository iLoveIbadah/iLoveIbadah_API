using iLoveIbadah.Application.Constants;
using iLoveIbadah.Application.DTOs.Blog;
using iLoveIbadah.Application.Features.Blogs.Requests.Commands;
using iLoveIbadah.Application.Features.Blogs.Requests.Queries;
using iLoveIbadah.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<BlogsController>
        [HttpGet]
        [EndpointSummary("Get all Blogs")]
        [EndpointDescription("Get A List of all the Blogs")]
        [AllowAnonymous]
        public async Task<ActionResult<List<BlogListDto>>> GetAll()
        {
            var blogs = await _mediator.Send(new GetBlogListRequest());
            return Ok(blogs);
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogDto>> GetById(int id)
        {
            var blog = await _mediator.Send(new GetBlogDetailsRequest { Id = id });
            return Ok(blog);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateBlogDto blog)
        {
            var command = new CreateBlogCommand { BlogDto = blog };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<BlogsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BlogsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
