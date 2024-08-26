using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexaTechnicalApi.DTOs
{
    public class AdvisorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SIN { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? HealthStatus { get; set; }


    }
}