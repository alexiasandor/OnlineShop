using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;
using Laroa.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Laroa.Application
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext dataContext, IUserRepository userRepository, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // stergerea unui user
        public async Task<User?> DeleteClientAsync(int id)
        {
            var searchedUser = await _userRepository.GetByIdAsync(id);

            if (searchedUser == null)
            {
                return null;
            }

            await _userRepository.DeleteAsync(searchedUser);
            await _dataContext.SaveChangesAsync();

            return searchedUser;
        }

        // afisarea tuturor clientilor
        public async Task<IList<User>> GetClientsAsync()
        {
            return await _userRepository.GetAllClientsAsync();
        }

        // partea de login in site
        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var searchedByEmailUser = await _userRepository.GetByEmailAsync(email);

            // caz eroare
            if (searchedByEmailUser == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "User not found!"
                };
            }

            var searchedByEmailAndPasswordUser = await _userRepository.GetByEmailAndPasswordAsync(email, EncryptPassword(password));

            // caz de eroare
            if (searchedByEmailAndPasswordUser == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid password!"
                };
            }

            // generarea de token personalizat
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>().Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, searchedByEmailAndPasswordUser.Email.ToString()),
                    new Claim(ClaimTypes.Role, searchedByEmailAndPasswordUser.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),   // data de expirare a tokenului
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // mesajele de afisare in urma logarii cu succes
            return new LoginResponse
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                Email = searchedByEmailAndPasswordUser.Email,
                Role = searchedByEmailAndPasswordUser.Role,
                Message = "Login successful!"
            };
        }

        // inregistrarea adminului pe site
        public async Task<RegisterResponse> RegisterAsync(string name, string birthday, string email, string password, bool isAdmin)
        {
            var searchedUser = await _userRepository.GetByEmailAsync(email);

            if (searchedUser != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "User already exists!"
                };
            }

            var user = new User
            {
                Name = name,
                Birthday = birthday,
                Email = email,
                Password = EncryptPassword(password),
                Role = isAdmin ? UserRole.Admin : UserRole.Client  // se verifica daca s a inregistrat la admin, daca da, se selecteaza rolul respectiv
            };

            await _userRepository.CreateAsync(user);
            await _dataContext.SaveChangesAsync();

            var createdUser = await _userRepository.GetByEmailAsync(email);

            return new RegisterResponse
            {
                Success = true,
                UserId = createdUser.Id,
                Message = "User created successfully!"
            };
        }

        // aici se face update pentru clienti
        public async Task<User?> UpdateClientAsync(int id, string name, string email)
        {
            var searchedUser = await _userRepository.GetByIdAsync(id);

            if (searchedUser == null)
            {
                return null;
            }

            searchedUser.Name = name ?? searchedUser.Name;
            searchedUser.Email = email ?? searchedUser.Email;

            await _dataContext.SaveChangesAsync();

            return searchedUser;
        }

        // criptarea parolei
        private string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = SHA256.Create().ComputeHash(passwordBytes);

            return Encoding.UTF8.GetString(passwordHash);
        }
    }
}
