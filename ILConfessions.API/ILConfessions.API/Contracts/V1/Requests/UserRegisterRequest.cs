using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Contracts.V1.Requests
{
    public class UserRegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Country { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public UserRegisterRequest()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
