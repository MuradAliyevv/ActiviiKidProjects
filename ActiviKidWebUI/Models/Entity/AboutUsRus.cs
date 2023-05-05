using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class AboutUsRus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
