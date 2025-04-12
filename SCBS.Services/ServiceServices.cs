using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCBS.Repositories.Models;
using SCBS.Repositories;
using SCBS.Repositories.DTO;


namespace SCBS.Services
{
    public interface IServiceServices
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(Guid id);
        Task<List<Service>> Search(string name, string description);
        Task<int> Create(ServiceDTO service);
        Task<int> Update(Service service);
        Task<bool> Delete(Guid id);


    }
    public class ServiceServices : IServiceServices
    {
        private readonly ServiceRepository _serviceRepository;
        public ServiceServices() => _serviceRepository = new ServiceRepository();
        public async Task<int> Create(ServiceDTO service)
        {
            var serviceModel = new Service
            {
                Id = Guid.NewGuid(),
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration,
                Rating = service.Rating,
                Status = service.Status,
                CreatedAt = service.CreatedAt ?? DateTime.UtcNow,
                UpdatedAt = service.UpdatedAt,
                UserId = service.UserId
            };
            return await _serviceRepository.CreateAsync(serviceModel);
        }
        public async Task<bool> Delete(Guid id)
        {
            var item = await _serviceRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _serviceRepository.RemoveAsync(item);
            }
            return false;
        }
        public async Task<List<Service>> GetAllAsync()
        {
            return await _serviceRepository.GetAllAsync();
        }
        public async Task<Service?> GetByIdAsync(Guid id) => await _serviceRepository.GetByIdAsync(id);
        public async Task<List<Service>> Search(string name, string description)
        {
            return await _serviceRepository.Search(name, description);
        }
        public async Task<int> Update(Service service)
        {
            return await _serviceRepository.UpdateAsync(service);
        }
    }
}
