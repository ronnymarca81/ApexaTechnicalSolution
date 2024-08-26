using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApexaTechnicalApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(255), EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}