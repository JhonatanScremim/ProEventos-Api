using System.ComponentModel.DataAnnotations;

namespace ProEventos.Repository.Models
{
    public class PageParams
    {
        [Range(1, 50)]
        public int PageNumber { get; set; }
        [Range(1, 50)]
        public int PageSize { get; set; }
    }
}