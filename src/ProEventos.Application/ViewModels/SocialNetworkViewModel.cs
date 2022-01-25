namespace ProEventos.Application.ViewModels
{
    public class SocialNetworkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? EventId { get; set; }
        public EventViewModel Event { get; set; }
        public int? LecturerId { get; set; }
        public LecturerViewModel Lecturer { get; set; }
    }
}