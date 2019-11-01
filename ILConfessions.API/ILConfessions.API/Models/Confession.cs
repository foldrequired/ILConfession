using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Models
{
    public class Confession
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
