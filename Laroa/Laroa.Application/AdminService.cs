using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;
using System.Xml.Linq;

namespace Laroa.Application
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Admin> AddAsync(string nume, string prenume, string email, string permisiuni)
        {
            var admin = new Admin
            {
                Nume = nume,
                Prenume = prenume,
                Email = email,
                Permisiuni = permisiuni
            };

            await _unitOfWork.AdminRepository.AddAsync(admin);
            await _unitOfWork.Save();

            return admin;
        }

       

        public async Task<Admin> DeleteAsync(int id)
        {
            var searchedAdmin = await _unitOfWork.AdminRepository.GetByIdAsync(id);

            if (searchedAdmin == null)
                return null;

            await _unitOfWork.AdminRepository.DeleteAsync(searchedAdmin);
            await _unitOfWork.Save();

            return searchedAdmin;
        }

        public async Task<IList<Admin>> GetAllAsync()
        {
            return await _unitOfWork.AdminRepository.GetAllAsync();
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            return await _unitOfWork.AdminRepository.GetByIdAsync(id);
        }


        public async Task<Admin> UpdateAsync(int id, string nume, string prenume, string email, string permisiuni)
        {
            var searchedAdmin = await _unitOfWork.AdminRepository.GetByIdAsync(id); // <-- This line is removed

            if (searchedAdmin == null)
                return null;

            searchedAdmin.Nume = nume ?? searchedAdmin.Nume;
            searchedAdmin.Prenume = prenume ?? searchedAdmin.Prenume;
            searchedAdmin.Email = email ?? searchedAdmin.Email;
            searchedAdmin.Permisiuni = permisiuni ?? searchedAdmin.Permisiuni;

            await _unitOfWork.Save();
            return searchedAdmin;
        }


    }
}
