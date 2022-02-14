using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<int>    
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TitleUser Title { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public override string Email { get; set; }
        public TypeUser TypeUser { get; set; }
        public string ImageUrl { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}