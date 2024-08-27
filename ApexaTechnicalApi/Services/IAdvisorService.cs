using ApexaTechnicalApi.DTOs;
using ApexaTechnicalApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexaTechnicalApi.Services
{
    public interface IAdvisorService
    {
        Task<AdvisorDto?> GetAdvisorByIdAsync(int id);  // Nullable return type
        Task<AdvisorDto> CreateAdvisorAsync(CreateAdvisorDto advisorDto);
        Task<List<AdvisorDto>> GetAdvisorsAsync();
        Task UpdateAdvisorAsync(int id, CreateAdvisorDto advisorDto);
        Task DeleteAdvisorAsync(int id);
    }
}


