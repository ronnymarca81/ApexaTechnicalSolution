using ApexaTechnicalApi.Data;
using ApexaTechnicalApi.DTOs;
using ApexaTechnicalApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexaTechnicalApi.Services
{
    public class AdvisorService : IAdvisorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdvisorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdvisorDto?> GetAdvisorByIdAsync(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            return advisor == null ? null : _mapper.Map<AdvisorDto>(advisor);
        }

        public async Task<AdvisorDto> CreateAdvisorAsync(CreateAdvisorDto advisorDto)
        {
            var advisor = _mapper.Map<Advisor>(advisorDto);
            advisor.HealthStatus = GenerateRandomHealthStatus();

            _context.Advisors.Add(advisor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AdvisorDto>(advisor);
        }

        public async Task<List<AdvisorDto>> GetAdvisorsAsync()
        {
            var advisors = await _context.Advisors.ToListAsync();
            return _mapper.Map<List<AdvisorDto>>(advisors);
        }

        public async Task UpdateAdvisorAsync(int id, CreateAdvisorDto advisorDto)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
            {
                throw new KeyNotFoundException("Advisor not found.");
            }

            _mapper.Map(advisorDto, advisor);
            advisor.HealthStatus = GenerateRandomHealthStatus();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdvisorAsync(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
            {
                throw new KeyNotFoundException("Advisor not found.");
            }

            _context.Advisors.Remove(advisor);
            await _context.SaveChangesAsync();
        }

        private string GenerateRandomHealthStatus()
        {
            var random = new Random();
            var statuses = new[] { "Green", "Yellow", "Red" };
            return statuses[random.Next(statuses.Length)];
        }
    }
}
