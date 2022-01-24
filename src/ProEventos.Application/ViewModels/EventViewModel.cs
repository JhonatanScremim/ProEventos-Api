using System;

namespace ProEventos.Application.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public DateTime? EventDate { get; set; }
        public string Name { get; set; }
        public int QuantityPeople { get; set; }
        public string ImageUrl { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}