using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.ViewModel
{
    public class SiteInfoViewModel
    {
        public SiteInfo? SiteInfo { get; set; }
        public AboutUs? AboutUs { get; set; }
        public IEnumerable<SocialLink>? SocialLinks { get; set; }
        public IEnumerable<Services>? Services { get; set; }
        public List<ServicesRus>? ServicesRus { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public List<CategoryRus>? CategoryRus { get; set; }
        public IEnumerable<Color>? Colors { get; set; }
        public List<ColorRus>? ColorRus { get; set; }
        public List<News>? News { get; set; }
        public List<NewsRus>? NewsRus { get; set; }
        public List<Genders>? Genders { get; set; }
        public List<GendersRus>? GendersRus { get; set; }
    }
}
