using iLoveIbadah.Application.Constants;
using iLoveIbadah.Application.DTOs.Comment;
using iLoveIbadah.Application.Features.Comments.Requests.Commands;
using iLoveIbadah.Application.Features.Comments.Requests.Queries;
using iLoveIbadah.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iLoveIbadah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        [EndpointSummary("Get all Comments")]
        [EndpointDescription("Get A List of all the Comments")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentListDto>>> GetAll()
        {
            var comments = await _mediator.Send(new GetCommentListRequest());
            return Ok(comments);
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            var comment = await _mediator.Send(new GetCommentDetailsRequest { Id = id });
            return Ok(comment);
        }

        // POST api/<CommentsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateCommentDto comment)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Id.ToString())?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }

            comment.UserAccountId = int.Parse(userIdClaim);
            var command = new CreateCommentCommand { CommentDto = comment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<CommentsController>/5
        [HttpPut]//recolt id in body of request not here
        public async Task<ActionResult> Update([FromBody] UpdateCommentDto comment)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Id.ToString())?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }
            comment.UserAccountId = int.Parse(userIdClaim);
            var command = new UpdateCommentCommand { CommentDto = comment };
            await _mediator.Send(command);
            return NoContent();
        }

        //// DELETE api/<CommentsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
