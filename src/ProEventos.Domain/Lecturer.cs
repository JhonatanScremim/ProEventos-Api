using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventLecturer> EventLecturers { get; set; }
    }
}
