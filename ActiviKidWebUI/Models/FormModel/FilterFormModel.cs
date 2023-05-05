using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.FormModel
{
    public class FilterFormModel
    {
        public int ColorId { get; set; }
        public int CategoryId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public bool BestSeller { get; set; }
        public bool MostLiked { get; set; }
        public bool Top { get; set; }
        public bool NewArrival { get; set; }
    }
}
