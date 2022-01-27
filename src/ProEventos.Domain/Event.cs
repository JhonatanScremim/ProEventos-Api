using System.Collections.Generic;
using System;

namespace ProEventos.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public DateTime? EventDate { get; set; }
        public string Name { get; set; }
        public int QuantityPeople { get; set; }
        public string ImageUrl { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<Batch> Batches { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventLecturer> EventLecturers { get; set; }
    }
}
