using Blog.web.Data;
using Bloggie.Web.Models.Domain;

namespace Blog.web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;   
        }
        public  Tag AddTag(Tag tag)
        {
             bloggieDbContext.Tags.Add(tag);
             bloggieDbContext.SaveChanges();
             return tag;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public Tag GetTag(Guid id)
        {
            throw new NotImplementedException();
        }

        public Tag UpdateTag(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
