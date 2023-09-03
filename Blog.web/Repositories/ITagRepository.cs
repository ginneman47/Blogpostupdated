using Bloggie.Web.Models.Domain;

namespace Blog.web.Repositories
{
    public interface ITagRepository
    {
       IEnumerable<Tag> GetAllTags();

       Tag GetTag(Guid id);

        Tag AddTag(Tag tag);

        Tag UpdateTag(Tag tag);

    }
}
