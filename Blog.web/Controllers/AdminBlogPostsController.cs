using Blog.web.Data;
using Blog.web.Models.Domain;
using Blog.web.Models.ViewModels;
using Blog.web.Repositories;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Blog.web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly BloggieDbContext bloggieDbContext;
        public AdminBlogPostsController(ITagRepository tagRepository, BloggieDbContext bloggieDbContext)
        {
            this.tagRepository = tagRepository;
            this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            
            var tags = bloggieDbContext.Tags.ToList();
            var model = new AddBlogPostRequest {
               Tags = tags.Select(x=>new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString()})
               
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Console.WriteLine(addBlogPostRequest.SelectedTag);
            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = bloggieDbContext.Tags.Find(selectedTagIdGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription  = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl  = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                visible = addBlogPostRequest.Visible,
                Tags = selectedTags

            };

            bloggieDbContext.Add(blogPost);
            bloggieDbContext.SaveChanges();
            return View("Add");
        }
        [HttpGet]
        public IActionResult List()
        {
            var listOfBlogs = bloggieDbContext.BlogPosts.ToList();
            ListBlogRequest blogs = new ListBlogRequest(listOfBlogs);
            return View(blogs);
        }
    }
}
