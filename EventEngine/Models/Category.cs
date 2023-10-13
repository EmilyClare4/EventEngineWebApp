using System.ComponentModel.DataAnnotations;

namespace EventEngine.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Events { get; set;}
    }
}
