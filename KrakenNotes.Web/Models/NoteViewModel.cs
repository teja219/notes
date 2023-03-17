using System.ComponentModel.DataAnnotations;

namespace KrakenNotes.Web.Models
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
