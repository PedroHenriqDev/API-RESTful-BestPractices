﻿using Bogus;
using Catalogue.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalogue.Tests.UnitTests;

public class TokenTests
{
    private readonly Mock<ILogger<TokenService>> _mockedLogger;
    private readonly string name;
    private readonly TokenService _tokenService;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly string jti;

    private IConfiguration configuration;

    public TokenTests()
    {
        _mockedLogger = new Mock<ILogger<TokenService>>();

        name = new Faker().Name.FirstName();

        _tokenService = new TokenService(_mockedLogger.Object);

        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Secret", "56da82d8b8309d2ae2b649032c90d145" },
            {"Jwt:ExpireMinutes", "60" }
        };

        _tokenHandler = new JwtSecurityTokenHandler();

        jti = Guid.NewGuid().ToString();

        configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    /// <summary>
    /// Verifies that the token generation process correctly creates a valid token with the expected claims.
    /// </summary>
    [Fact]
    public void GenerateToken_GivenAuthClaimsAndConfiguration_ReturnWriteTokenValid()
    {
        //Arrange

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
        };

        //Act
        string token = _tokenService.GenerateToken(authClaims, configuration);

        JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(token);

        //Assert
        Assert.NotNull(token);
        Assert.Contains(jwtToken.Claims, claim => claim.Type == "unique_name" && claim.Value == name);
        Assert.Contains(jwtToken.Claims, claim => claim.Type == JwtRegisteredClaimNames.Jti);
        Assert.True(jwtToken.ValidFrom > DateTime.Now);
    }

    /// <summary>
    /// Verifies that an 'ArgumentNullException' is thrown when the secret key is null or empty.
    /// </summary>
    ///<param name="secret">The secret key used for signing the token, which is either null or empty in this test case.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void SecretIsNullOrEmpty_GivenAuthClaimsAndConfiguration_ThrowsArgumentNullException(string secret)
    {
        //Arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Jwt:Secret", secret },
            { "Jwt:ExpireMinutes", "60" }
        };

        configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
        };

        //Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _tokenService.GenerateToken(authClaims, configuration));
    }

    /// <summary>
    /// Verifies that a generated token is valid according to the specified validation parameters.
    /// </summary>
    [Fact]
    public void ValidateToken_GivenValidToken_ReturnTrue()
    {
        //Arrange
        byte[] secretKey = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
        };            

        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };

        //Act
        string token = _tokenService.GenerateToken(authClaims, configuration);

        ClaimsPrincipal principal = _tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        //Assert 
        Assert.NotNull(principal);
        Assert.IsType<JwtSecurityToken>(validatedToken);
    }
}
