using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public DateTime? EventDate { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }
        
        [Range(1, 20000)]
        public int QuantityPeople { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$")]
        public string ImageUrl { get; set; }

        [Required, Phone]
        public string Telephone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public IEnumerable<BatchViewModel> Batches { get; set; }
        public IEnumerable<SocialNetworkViewModel> SocialNetworks { get; set; }
        public IEnumerable<LecturerViewModel> Lecturers { get; set; }
    }
}