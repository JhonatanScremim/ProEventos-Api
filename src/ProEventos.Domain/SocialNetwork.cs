
namespace ProEventos.Domain
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
        public int? LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
