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
    }
}
