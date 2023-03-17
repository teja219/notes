using System.ComponentModel.DataAnnotations;

namespace KrakenNotes.Web.Models
{
    public class NoteCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
