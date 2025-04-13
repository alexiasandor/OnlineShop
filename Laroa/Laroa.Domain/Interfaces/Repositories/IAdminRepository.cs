namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<IList<Admin>> GetAllAsync();
        Task<Admin> GetByIdAsync(int id);
        Task AddAsync(Admin admin);
        Task DeleteAsync(Admin admin);
        
       
    }
}
