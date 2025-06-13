using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iLoveIbadah.Website.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: CommentController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        public async void Create(CreateCategoryVM comment)
        {
            //if (ModelState.IsValid)
            try
            {
                var response = await _commentService.Create(comment.CreateComment);
                if (response.Success)
                {
                    return;
                    //return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            //return RedirectToAction(nameof(Index));
            //return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }

        // GET: CommentController/Edit/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Update(UpdateCommentVM comment)
        {
            //if (ModelState.IsValid)
            try
            {
                var response = await _commentService.Update(comment);
                if (response.Success)
                {
                    return;
                    //return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
    }
}
