using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.ViewModel
{
    public class FooterViewModel
    {
        public SiteInfo SiteInfo { get; set; }
        public Newsletter Newsletter { get; set; }
        public IEnumerable<dynamic> Services { get; set; }
    }
}
