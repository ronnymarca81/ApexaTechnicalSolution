using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApexaTechnicalApi.Models
{
    public class Advisor
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string? Name { get; set; }

        [Required, StringLength(9, MinimumLength = 9)]
        public string? SIN { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [StringLength(8, MinimumLength = 8)]
        public string? Phone { get; set; }

        public string? HealthStatus { get; set; }

    }
}