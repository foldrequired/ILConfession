using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string KnownAs { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
