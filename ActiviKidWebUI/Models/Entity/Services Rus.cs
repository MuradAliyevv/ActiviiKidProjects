using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class ServicesRus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string? Name { get; set; }

    }
}
