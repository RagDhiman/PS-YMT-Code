using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_CustomerManager_API.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int CustomerId { get; set; }
    }
}
