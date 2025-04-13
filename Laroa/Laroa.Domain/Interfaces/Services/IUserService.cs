namespace Laroa.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAsync(string name, string birthday, string email, string password, bool isAdmin);
        Task<LoginResponse> LoginAsync(string email, string password);
        Task<User> UpdateClientAsync(int id, string name, string email);
        Task<User> DeleteClientAsync(int id);
        Task<IList<User>> GetClientsAsync();
    }

}
