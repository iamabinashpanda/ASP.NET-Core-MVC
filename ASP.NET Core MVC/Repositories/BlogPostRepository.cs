using ASP.NET_Core_MVC.Data;
using ASP.NET_Core_MVC.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_MVC.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AspNetCoreMvcDbContext aspNetCoreMvcDbContext;

        public BlogPostRepository(AspNetCoreMvcDbContext aspNetCoreMvcDbContext)
        {
            this.aspNetCoreMvcDbContext = aspNetCoreMvcDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await aspNetCoreMvcDbContext.BlogPosts.AddAsync(blogPost);
            await aspNetCoreMvcDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await aspNetCoreMvcDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await aspNetCoreMvcDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog = await aspNetCoreMvcDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==blogPost.Id);
            if(existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading= blogPost.Heading;
                existingBlog.Author= blogPost.Author;
                existingBlog.PageTitle= blogPost.PageTitle;
                existingBlog.Content= blogPost.Content;
                existingBlog.ShortDescription= blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl= blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle= blogPost.UrlHandle;
                existingBlog.Visible= blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;   
                existingBlog.PublishedDate = blogPost.PublishedDate;

                await aspNetCoreMvcDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
