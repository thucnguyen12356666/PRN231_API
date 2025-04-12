using SCBS.Repositories;
using SCBS.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBS.Services
{
    public interface IServiceCategoryService
    {
        Task<List<ServiceCategory>> GetAllAsync();
        Task<ServiceCategory?> GetByIdAsync(Guid id);
        Task<List<ServiceCategory>> Search(string categoryCode);
        Task<int> Create(ServiceCategory serviceCategory);
        Task<int> Update(ServiceCategory serviceCategory);
        Task<bool> Delete(Guid id);
    }

    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly ServiceCategoryRepository _serviceCategoryRepository;
        public ServiceCategoryService() => _serviceCategoryRepository = new ServiceCategoryRepository();

        public async Task<int> Create(ServiceCategory serviceCategory)
        {
            return await _serviceCategoryRepository.CreateAsync(serviceCategory);
        }

        public async Task<bool> Delete(Guid id)
        {
            var item = await _serviceCategoryRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _serviceCategoryRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<ServiceCategory>> GetAllAsync()
        {
            return await _serviceCategoryRepository.GetAllAsync();
        }

        public async Task<ServiceCategory?> GetByIdAsync(Guid id) => await _serviceCategoryRepository.GetByIdAsync(id);

        public async Task<List<ServiceCategory>> Search(string categoryCode)
        {
            return await _serviceCategoryRepository.Search(categoryCode);
        }

        public async Task<int> Update(ServiceCategory serviceCategory)
        {
            return await _serviceCategoryRepository.UpdateAsync(serviceCategory);
        }
    }
}
