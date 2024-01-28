using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_Core_MVC.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public String Heading { get; set; }
        public String PageTitle { get; set; }
        public String Content { get; set; }
        public String ShortDescription { get; set; }
        public String FeaturedImageUrl { get; set; }
        public String UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //collect tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
