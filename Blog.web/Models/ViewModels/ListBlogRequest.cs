using Blog.web.Models.Domain;

namespace Blog.web.Models.ViewModels
{
    public class ListBlogRequest
    {
        public List<BlogPost> listOfBlogs { get; set; }

        public ListBlogRequest(List<BlogPost> posts)
        {
            listOfBlogs = new List<BlogPost>();
            listOfBlogs = posts;

        }
    }
}
