using System.ComponentModel.DataAnnotations;

namespace oleksandrbachkai.Models.Entities
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }        
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
