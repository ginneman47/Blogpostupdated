using Bloggie.Web.Models.Domain;

namespace Blog.web.Models.ViewModels
{
    public class ListTagRequest
    {
        public List<Tag> tagsList { get; set; }
        public ListTagRequest(List<Tag> tags)
        {
            tagsList = new List<Tag>();
            tagsList = tags;
        }
    }
}
