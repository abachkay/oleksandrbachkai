using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oleksandrbachkai.Models.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
      
        public string Text { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
