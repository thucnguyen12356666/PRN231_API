using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCBS.Repositories.DTO;
using SCBS.Repositories.Models;
using SCBS.Services;

namespace SCBS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceServices _serviceService;
        public ServiceController(IServiceServices serviceService) => _serviceService = serviceService;
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Service>> GetAll() => await _serviceService.GetAllAsync();
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<Service?> GetById(Guid id) => await _serviceService.GetByIdAsync(id);
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<int> Create(ServiceDTO service) => await _serviceService.Create(service);
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<int> Update(Service service) => await _serviceService.Update(service);
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<bool> Delete(Guid id) => await _serviceService.Delete(id);
        [HttpGet("search")]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Service>> Search(string name, string description) => await _serviceService.Search(name, description);


    }
}
