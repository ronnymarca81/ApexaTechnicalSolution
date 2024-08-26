using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexaTechnicalApi.Data;
using ApexaTechnicalApi.DTOs;
using ApexaTechnicalApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApexaTechnicalApi.Services
{
    public class AdvisorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdvisorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdvisorDto> CreateAdvisorAsync(CreateAdvisorDto dto)
        {
            var advisor = _mapper.Map<Advisor>(dto);
            advisor.HealthStatus = GenerateHealthStatus();

            _context.Advisors.Add(advisor);
            await _context.SaveChangesAsync();

            return _mapper.Map<AdvisorDto>(advisor);
        }

        public async Task<AdvisorDto?> GetAdvisorByIdAsync(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            return advisor == null ? null : _mapper.Map<AdvisorDto>(advisor);
        }

        public async Task<List<AdvisorDto>> GetAdvisorsAsync()
        {
            var advisors = await _context.Advisors.ToListAsync();
            return _mapper.Map<List<AdvisorDto>>(advisors);
        }

        public async Task UpdateAdvisorAsync(int id, CreateAdvisorDto dto)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor != null)
            {
                _mapper.Map(dto, advisor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAdvisorAsync(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor != null)
            {
                _context.Advisors.Remove(advisor);
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateHealthStatus()
        {
            var rand = new Random();
            int value = rand.Next(100);
            if (value < 60) return "Green";
            if (value < 80) return "Yellow";
            return "Red";
        }

    }
}