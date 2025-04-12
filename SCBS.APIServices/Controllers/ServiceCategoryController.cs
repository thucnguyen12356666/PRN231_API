using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCBS.Repositories.Models;
using SCBS.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCBS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        // Lấy tất cả ServiceCategory
        [HttpGet]
      //  [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<ServiceCategory>> GetAll() => await _serviceCategoryService.GetAllAsync();

        // Lấy ServiceCategory theo ID
        [HttpGet("{id}")]
      //  [Authorize(Roles = "1,2")]
        public async Task<ServiceCategory?> GetById(Guid id) => await _serviceCategoryService.GetByIdAsync(id);

        // Tạo mới ServiceCategory
        [HttpPost]
       // [Authorize(Roles = "1")]
        public async Task<int> Create(ServiceCategory serviceCategory) => await _serviceCategoryService.Create(serviceCategory);

        // Cập nhật ServiceCategory
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceCategory serviceCategory)
        {
            if (id != serviceCategory.Id)
                return BadRequest("ID mismatch");

            var result = await _serviceCategoryService.Update(serviceCategory);
            return Ok(result);
        }


        // Xóa ServiceCategory theo ID
        [HttpDelete("{id}")]
       // [Authorize(Roles = "1")]
        public async Task<bool> Delete(Guid id) => await _serviceCategoryService.Delete(id);

        // Tìm kiếm ServiceCategory theo CategoryCode hoặc ServiceId
        [HttpGet("search")]
       // [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<ServiceCategory>> Search(string categoryCode)
            => await _serviceCategoryService.Search(categoryCode);
    }
}
