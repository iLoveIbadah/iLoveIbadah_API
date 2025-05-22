using iLoveIbadah.Application.DTOs.Category;
using iLoveIbadah.Application.Features.Categories.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoriesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [EndpointSummary("Get all categories")]
        [EndpointDescription("Get A List of all the categories")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryListDto>>> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoryListRequest());
            return Ok(categories);
        }

        // GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<CategoryDto>> GetById(int id)
        //{
        //    var category = await _mediator.Send(new GetCategoryDetailsRequest { Id = id });
        //    return Ok(category);
        //}

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
