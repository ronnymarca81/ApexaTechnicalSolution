using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexaTechnicalApi.DTOs;
using ApexaTechnicalApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApexaTechnicalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvisorsController : ControllerBase
    {
        private readonly AdvisorService _advisorService;
        public AdvisorsController(AdvisorService advisorService)
        {
            _advisorService = advisorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvisor([FromBody] CreateAdvisorDto dto)
        {
            var advisor = await _advisorService.CreateAdvisorAsync(dto);
            return CreatedAtAction(nameof(GetAdvisor), new { id = advisor.Id }, advisor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdvisor(int id)
        {
            var advisor = await _advisorService.GetAdvisorByIdAsync(id);
            if (advisor == null)
            {
                return NotFound();
            }

            return Ok(advisor);
        }
        [HttpGet]
        public async Task<IActionResult> GetAdvisors()
        {
            var advisors = await _advisorService.GetAdvisorsAsync();
            return Ok(advisors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdvisor(int id, [FromBody] CreateAdvisorDto dto)
        {
            await _advisorService.UpdateAdvisorAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvisor(int id)
        {
            await _advisorService.DeleteAdvisorAsync(id);
            return NoContent();
        }

        [HttpGet("by-health/{status}")]
        public async Task<IActionResult> GetAdvisorsByHealthStatus(string status)
        {
            var advisors = await _advisorService.GetAdvisorsAsync();
            var filteredAdvisors = advisors.Where(a => a.HealthStatus!.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(filteredAdvisors);
        }

    }
}