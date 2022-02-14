using System.Collections.Generic;
using ProEventos.Domain.Identity;

namespace ProEventos.Domain
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventLecturer> EventLecturers { get; set; }
    }
}
