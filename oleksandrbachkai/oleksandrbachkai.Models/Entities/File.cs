using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oleksandrbachkai.Models.Entities
{
    public class File
    {
        [Key]
        public int FileId { get; set; }

        public string DriveId { get; set; }

        public string FileName { get; set; }        

        public string Url { get; set; }

        [ForeignKey(nameof(Folder))]
        public int FolderId { get; set; }

        public Folder Folder { get; set; }
    }
}