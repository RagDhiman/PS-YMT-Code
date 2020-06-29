using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
