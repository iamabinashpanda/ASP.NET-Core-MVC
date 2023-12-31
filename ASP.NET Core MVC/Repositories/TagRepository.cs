using ASP.NET_Core_MVC.Data;
using ASP.NET_Core_MVC.Models.Domain;
using ASP.NET_Core_MVC.Models.ViewModels;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_MVC.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AspNetCoreMvcDbContext aspNetCoreMvcDbContext;

        public TagRepository(AspNetCoreMvcDbContext aspNetCoreMvcDbContext)
        {
            this.aspNetCoreMvcDbContext = aspNetCoreMvcDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await aspNetCoreMvcDbContext.Tags.AddAsync(tag);
            await aspNetCoreMvcDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await aspNetCoreMvcDbContext.Tags.FindAsync(id);
            if(existingTag != null)
            {
                aspNetCoreMvcDbContext.Tags.Remove(existingTag);
                await aspNetCoreMvcDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await aspNetCoreMvcDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
           return await aspNetCoreMvcDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await aspNetCoreMvcDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await aspNetCoreMvcDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
