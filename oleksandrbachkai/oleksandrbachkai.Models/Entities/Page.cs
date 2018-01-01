using System.ComponentModel.DataAnnotations;

namespace oleksandrbachkai.Models.Entities
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]        
        public string Content { get; set; }
    }
}
