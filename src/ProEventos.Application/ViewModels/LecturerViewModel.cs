using System.Collections.Generic;

namespace ProEventos.Application.ViewModels
{
    public class LecturerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<SocialNetworkViewModel> SocialNetworks { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}