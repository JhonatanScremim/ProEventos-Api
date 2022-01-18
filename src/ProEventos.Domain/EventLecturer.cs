namespace ProEventos.Domain
{
    public class EventLecturer
    {
        public int EventId { get; set; }
        public Event Event { get; set; }        
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
