using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class AboutUs 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
