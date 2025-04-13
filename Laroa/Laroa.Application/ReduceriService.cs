using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;

namespace Laroa.Application
{
    public class ReduceriService : IReduceriService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReduceriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Reduceri> AddAsync(DateTime perioada, float procent, string tip)
        {
            var reduceri = new Reduceri
            {
                Perioada = perioada,
                Procent = procent,
                Tip = tip
            };

            await _unitOfWork.ReduceriRepository.AddAsync(reduceri);
            await _unitOfWork.Save();
            return reduceri;
        }

        public async Task<Reduceri> DeleteAsync(int reduceriId)
        {
            var searchedReducere = await _unitOfWork.ReduceriRepository.GetByIdAsync(reduceriId);

            if (searchedReducere == null)
                return null;

            await _unitOfWork.ReduceriRepository.DeleteAsync(searchedReducere);
            await _unitOfWork.Save();
            return searchedReducere;
        }

        public async Task<IList<Reduceri>> GetAllAsync()
        {
           var reduceriList =  await _unitOfWork.ReduceriRepository.GetAllAsync();
         // utilizeaza TPL pentru a efectua operatii in paralel
            var tasks = new List<Task>();

            foreach (var reduceri in reduceriList)
            {
                tasks.Add(ProcesareReducentaAsync(reduceri));
            }

            await Task.WhenAll(tasks);

            return reduceriList;
        }

        private async Task ProcesareReducentaAsync(Reduceri reduceri)
        {   
            foreach (var product in reduceri.Products)
            {
                product.PriceR = product.Price - (product.Price * reduceri.Procent / 100);
            }
            await Task.Delay(1000);
        }
        public async Task<Reduceri> GetByIdAsync(int reduceriId)
        {
            return await _unitOfWork.ReduceriRepository.GetByIdAsync(reduceriId);
        }


        public async Task<Reduceri> UpdateAsync(int reduceriId, DateTime? Perioada, float Procent, string tip)
        {
            var searchedReducere = await _unitOfWork.ReduceriRepository.GetByIdAsync(reduceriId);

            if (searchedReducere == null)
                return null;

            searchedReducere.Perioada = (DateTime)(Perioada ?? searchedReducere?.Perioada);

            await _unitOfWork.Save();

            return searchedReducere;
        }

        public async Task<Reduceri> AddReduceriToProduct(int reduceriId, int productId)
        {
            var reduceri = await _unitOfWork.ReduceriRepository.GetByIdAsync(reduceriId);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (reduceri == null || product == null)
                return null;
            //utilizeaza TPL pentru a ef ectua operatii in paralel

            var addTask = Task.Run(() => AddProductToReducer(reduceri, product));

            await addTask;

            //salveaza modificarile in baza de date

            await _unitOfWork.Save();

            return reduceri;
        }

        private void AddProductToReducer(Reduceri reduceri, Product product)
        {
            // verificam daca produsul este deja asociat cu reducerea
            if(!reduceri.Products.Contains(product))
            {
                //adauga produsul la lista de produse asociate reduceri
                reduceri.Products.Add(product);
                product.PriceR = product.Price - (product.Price * reduceri.Procent / 100);
            }
        }
        public async Task<Reduceri> RemoveProductFromReduceri(int reduceriId, int productId)
        {
            var reduceri = await _unitOfWork.ReduceriRepository.GetByIdAsync(reduceriId);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (reduceri == null || product == null)
                return null;
            // utilizare TPL

            var removeTask = Task.Run(() => RemoveProductFromReducer(reduceri, product));

            // asteapta terminarea operatiei de  eliminare in paralel 
            await removeTask;

            // salveaza modificarile 
            await _unitOfWork.Save();

            return reduceri;
        }
        private void RemoveProductFromReducer(Reduceri reduceri, Product product)
        {
            //verifica daca produsul este asociat reducerii
            if(reduceri.Products.Contains(product))
            {   
                //eliminam produsul din lista
                reduceri.Products.Remove(product);
            }
        }
    }
}
