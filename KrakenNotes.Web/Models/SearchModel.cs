using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrakenNotes.Web.Models
{
    public class SearchModel
    {
        public string Id { get; set; }

        public string SearchText { get; set; }

        public IEnumerable<NoteViewModel> SearchResult { get; set; }
    }
}
