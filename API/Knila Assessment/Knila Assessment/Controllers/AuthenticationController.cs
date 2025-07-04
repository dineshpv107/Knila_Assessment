using Knila.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Knila_Assessment.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class AuthenticationController : ControllerBase
        {
            private readonly IConfiguration _configuration;
            private readonly KnilaDbContext _context;
            private readonly IHttpClientFactory _httpClientFactory;

            public AuthenticationController(IConfiguration configuration, KnilaDbContext context, IHttpClientFactory httpClientFactory)
            {
                _configuration = configuration;
                _context = context;
                _httpClientFactory = httpClientFactory;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Post(string user,string password)
            {
                try
                {
                    if (user != null && !string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(password))
                    {
                        var userData = await GetUser(user, password);
                        var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                        if (userData != null && jwt != null)
                        {
                            var claims = new[]
                            {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userData.UserId.ToString()),
                        new Claim("UserName", userData?.UserName ?? ""),
                        new Claim("Password", userData?.Password ?? "")
                    };

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                jwt.Issuer,
                                jwt.Audience,
                                claims,
                                expires: DateTime.Now.AddHours(2),
                                signingCredentials: signIn
                            );

                            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                        }
                    }
                    return BadRequest("Invalid Credentials");
                }
                catch (Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }

            [HttpGet]
            public async Task<User> GetUser(string username, string password)
            {
                try
                {
                    return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        
            [HttpPost("ValidateToken")]
            public bool ValidateToken([FromBody] string token)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["csrJwt:key"])),
                    ValidIssuer = _configuration["csrJwt:Issuer"],
                    ValidAudience = _configuration["csrJwt:Audience"]
                };

                try
                {
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }
    }
