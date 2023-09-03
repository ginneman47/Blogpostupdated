using Blog.web.Data;
using Blog.web.Models.ViewModels;
using Blog.web.Repositories;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Blog.web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext _bloggieDbContext;
        private readonly ITagRepository tagRepository;
        public AdminTagsController(BloggieDbContext bloggieDbContext,ITagRepository tagRepository)
        {
                _bloggieDbContext = bloggieDbContext;
                this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //mapping the addTagRequest to the tag domain model
            var tag = new Tag {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
           // _bloggieDbContext.Tags.Add(tag);
           //_bloggieDbContext.SaveChanges();
            tagRepository.AddTag(tag);
            return View("Add");
        }
        [HttpGet]
        public IActionResult List() {
            var tags = _bloggieDbContext.Tags.ToList();
            ListTagRequest parameterTags = new ListTagRequest(tags);
            foreach (var tag in tags)
            {
                Console.WriteLine(tag.Name);
            }
            return View(parameterTags);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = _bloggieDbContext.Tags.Find(id);
            Console.WriteLine($"{id.ToString()}");

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest { 
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName 
                };
                return View(editTagRequest);
            }
            
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = _bloggieDbContext.Tags.Find(tag.Id);
            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                _bloggieDbContext.SaveChanges();
                //show success
                return RedirectToAction("List");
            }
            else
            {
                Console.WriteLine("NULL tag");
            }
            //show failure
            return RedirectToAction("Edit", new {id = editTagRequest.Id });

        }
    }
}
