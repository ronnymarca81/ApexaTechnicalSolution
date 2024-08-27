using ApexaTechnicalApi.Data;
using ApexaTechnicalApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IConfiguration> _mockConfiguration;

    public AuthServiceTests()
    {
        // Configuración del DbContext con InMemoryDatabase
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);

        // Inicialización de la clase AuthService con el DbContext en memoria
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("MySuperSecretKey123AndMySuperSecretKey123");
        _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("yourdomain.com");
        _mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("yourdomain.com");

        _authService = new AuthService(_dbContext, _mockConfiguration.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldRegisterUser()
    {
        // Arrange
        var email = "newuser@example.com";
        var password = "Password123!";

        // Act
        var token = await _authService.RegisterUserAsync(email, password);

        // Assert
        Assert.NotNull(token); // Ensure that a token is returned
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        Assert.NotNull(user); // Ensure that a user was added
        Assert.Equal(email, user.Email); // Ensure the user's email is correct
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrowExceptionIfEmailExists()
    {
        // Arrange
        var email = "existinguser@example.com";
        var password = "Password123!";

        // Register the user first
        await _authService.RegisterUserAsync(email, password);

        // Act & Assert: Verify that an exception is thrown when trying to register the same email
        await Assert.ThrowsAsync<Exception>(() => _authService.RegisterUserAsync(email, password));
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnTokenForValidCredentials()
    {
        // Arrange
        var email = "validuser@example.com";
        var password = "Password123!";

        // Register the user first
        await _authService.RegisterUserAsync(email, password);

        // Act
        var token = await _authService.LoginUserAsync(email, password);


        // Assert
        Assert.NotNull(token); // Ensure that a token is returned
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnNullForInvalidCredentials()
    {
        // Arrange
        var email = "validuser1@example.com";
        var password = "Password123!";
        var wrongPassword = "WrongPassword!";

        // Register the user first
        await _authService.RegisterUserAsync(email, password);

        // Act
        var token = await _authService.LoginUserAsync(email, wrongPassword);

        // Assert
        Assert.Null(token); // Ensure that no token is returned for invalid credentials
    }
}







