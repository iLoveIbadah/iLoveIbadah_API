using iLoveIbadah.Application.DTOs.PermissionType;
using iLoveIbadah.Application.Features.PermissionTypes.Requests.Queries;
using iLoveIbadah.Application.Features.ProfilePictureTypes.Requests.Queries;
using iLoveIbadah.Application.DTOs.ProfilePictureType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePictureTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfilePictureTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProfilePictureTypeListDto>>> GetAll()
        {
            var profilePictureTypes = await _mediator.Send(new GetProfilePictureTypeListRequest());
            return profilePictureTypes;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [AllowAnonymous] //je sais pas si restreindre cela va faire ne sorte que ceux pas loged in (invité) ne veront pas les image dans le leaderboard des gens...
        public async Task<ActionResult<ProfilePictureTypeDto>> Get(int id) //Get or GetById??? check if issue! todo! error! debug! fix! test! review! verify! validate! confirm! check! inspect! examine! audit! revise! study! investigate! analyze! assess! evaluate! scrutinize! probe!
        {
            var profilePictureType = await _mediator.Send(new GetProfilePictureTypeDetailsRequest { Id = id });
            return Ok(profilePictureType);
        }

        // POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
