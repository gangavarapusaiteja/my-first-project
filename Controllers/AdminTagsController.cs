using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = bloggieDbContext.Tags.Find(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }
            return View(null);

        }
        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
           var existingTag = bloggieDbContext.Tags.Find(editTagRequest.Id);
            if (existingTag != null) {
                existingTag.Name = editTagRequest.Name;
                existingTag.DisplayName = editTagRequest.DisplayName;
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit" , new {id = editTagRequest.Id});
        }
        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest) {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);
            if (tag != null) {
                bloggieDbContext.Tags.Remove(tag);
                bloggieDbContext.SaveChanges();

                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        
        }
                

    }
}
