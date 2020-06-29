using AM_CustomerManager_Core;

namespace AM_CustomerManager_Core
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int CustomerId { get; set; }
    }
}
