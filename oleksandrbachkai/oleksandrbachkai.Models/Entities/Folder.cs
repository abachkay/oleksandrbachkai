using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace oleksandrbachkai.Models.Entities
{
    public class Folder
    {
        [Key]
        public int FolderId { get; set; }     

        public string Name { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}