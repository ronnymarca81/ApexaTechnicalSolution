using Xunit;
using Moq;
using ApexaTechnicalApi.Services;
using ApexaTechnicalApi.Models;
using ApexaTechnicalApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexaTechnicalApi.DTOs;

namespace ApexaTechnicalApi.Tests.Controllers
{
    public class AdvisorsControllerTests
    {
        private readonly AdvisorsController _controller;
        private readonly Mock<IAdvisorService> _mockAdvisorService;

        public AdvisorsControllerTests()
        {
            _mockAdvisorService = new Mock<IAdvisorService>();
            _controller = new AdvisorsController(_mockAdvisorService.Object);
        }

        [Fact]
        public async Task CreateAdvisor_ShouldReturnCreatedAtActionResult()
        {
            var advisorDto = new CreateAdvisorDto { Name = "John Doe", SIN = "123456789" };
            var advisor = new AdvisorDto { Id = 1, Name = "John Martin", SIN = "123456789" };

            _mockAdvisorService.Setup(s => s.CreateAdvisorAsync(advisorDto))
                               .ReturnsAsync(advisor);

            var result = await _controller.CreateAdvisor(advisorDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<AdvisorDto>(createdResult.Value);
            Assert.Equal(advisor.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetAdvisor_ShouldReturnOkObjectResult_WhenAdvisorExists()
        {
            var advisor = new AdvisorDto { Id = 1, Name = "John Doe", SIN = "123456789" };

            _mockAdvisorService.Setup(s => s.GetAdvisorByIdAsync(1))
                               .ReturnsAsync(advisor);

            var result = await _controller.GetAdvisor(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AdvisorDto>(okResult.Value);
            Assert.Equal(advisor.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetAdvisor_ShouldReturnNotFoundResult_WhenAdvisorDoesNotExist()
        {
            _mockAdvisorService.Setup(s => s.GetAdvisorByIdAsync(1))
                               .ReturnsAsync((AdvisorDto?)null);

            var result = await _controller.GetAdvisor(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAdvisor_ShouldReturnNoContentResult_WhenAdvisorIsUpdated()
        {
            var advisorDto = new CreateAdvisorDto { Name = "Jane Doe", SIN = "987654321" };

            _mockAdvisorService.Setup(s => s.UpdateAdvisorAsync(1, advisorDto))
                               .Returns(Task.CompletedTask);

            var result = await _controller.UpdateAdvisor(1, advisorDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAdvisor_ShouldReturnNoContentResult_WhenAdvisorIsDeleted()
        {
            _mockAdvisorService.Setup(s => s.DeleteAdvisorAsync(1))
                               .Returns(Task.CompletedTask);

            var result = await _controller.DeleteAdvisor(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAdvisors_ShouldReturnOkObjectResult_WithListOfAdvisors()
        {
            var advisors = new List<AdvisorDto>
            {
                new AdvisorDto { Id = 1, Name = "John Doe", SIN = "123456789" },
                new AdvisorDto { Id = 2, Name = "Jane Martin", SIN = "987654321" }
            };

            _mockAdvisorService.Setup(s => s.GetAdvisorsAsync())
                               .ReturnsAsync(advisors);

            var result = await _controller.GetAdvisors();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<AdvisorDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}

