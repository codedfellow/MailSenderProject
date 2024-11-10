using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly BulkMailDbContext context;
        private readonly CancellationTokenSource tokenSource;
        private readonly IOptionsMonitor<JwtOptions> jwtOptionsMonitor;
        CancellationToken token;
        private readonly JwtOptions _jwtOptions;
        public UserService(IPasswordHasher<User> passwordHasher, BulkMailDbContext _context, CancellationTokenSource _tokenSource, IOptionsMonitor<JwtOptions> _jwtOptionsMonitor)
        {
            _passwordHasher = passwordHasher;
            context = _context;
            tokenSource = _tokenSource;
            token = tokenSource.Token;
            jwtOptionsMonitor = _jwtOptionsMonitor;
            _jwtOptions = jwtOptionsMonitor.CurrentValue;
        }

        public async Task<string> Login(UserLoginDto model)
        {
            if (context.User is null)
            {
                throw new NullReferenceException("No users found. Kindly register to login");
            }
            var user = await context.User.FirstOrDefaultAsync(x => x.Email == model.Email,this.token);

            if (user is null) {
                throw new NullReferenceException("User not found");
            }

            var passwordVeificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (passwordVeificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception($"Invalid password");
            }

            var token = GenerateJwtToken(user);

            return token;
        }

        public async Task<bool> RegisterUser(UserRegistrationDto model)
        {
            if (context.User is null)
            {
                throw new NullReferenceException("No users found. Kindly register to login");
            }
            var existingUser = await context.User.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (existingUser is not null)
            {
                throw new NullReferenceException($"User with email {existingUser.Email} already exists");
            }

            User user = new User
            {
                Email = model.Email,
            };

            string passWordHash = _passwordHasher.HashPassword(user, model.Password);
            user.Password = passWordHash;
            user.CreatedAt = DateTime.Now;


            await context.AddAsync(user,token);
            await context.SaveChangesAsync(token);

            return true;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Role, user.UserDetail.Role.RoleName),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim("DateOfBirth", user.UserDetail.DateOfBirth.Value.ToString("dd-MM-yyyy")),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMinutes(_jwtOptions.JwtExpireMins);

            var token = new JwtSecurityToken(
                _jwtOptions.JwtIssuer,
                _jwtOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
