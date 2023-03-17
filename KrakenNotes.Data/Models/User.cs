using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KrakenNotes.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Image { get; set; }

        public int ColorMode { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
