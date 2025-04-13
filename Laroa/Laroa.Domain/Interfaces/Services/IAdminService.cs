namespace Laroa.Domain.Interfaces.Services
{
    public interface IAdminService
    {
        Task<Admin> AddAsync(string nume, string prenume, string email, string permisiuni);
        Task<Admin> DeleteAsync(int id);
        Task<Admin> GetByIdAsync(int id);
        Task<IList<Admin>> GetAllAsync();
        Task<Admin> UpdateAsync(int id, string? nume, string? prenume, string? email, string? permisiuni);
    }
}
