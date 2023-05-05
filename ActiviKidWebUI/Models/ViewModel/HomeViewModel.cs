using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<dynamic> Category { get; set; }
        public IEnumerable<dynamic> Product { get; set; }
        public IEnumerable<OurClients> OurClients { get; set; }
    }
}
