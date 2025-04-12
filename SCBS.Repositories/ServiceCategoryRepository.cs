using SCBS.Repositories.Base;
using SCBS.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCBS.Repositories
{
    public class ServiceCategoryRepository : GenericRepository<ServiceCategory>
    {
        public ServiceCategoryRepository() { }

        public new async Task<List<ServiceCategory>> GetAllAsync()
        {
            return await _context.ServiceCategories.Include(sc => sc.Service).Include(sc => sc.Service).Include(sc  =>  sc.Category ).ToListAsync();
        }

        public new async Task<ServiceCategory?> GetByIdAsync(Guid id)
        {
            return await _context.ServiceCategories.Include(sc => sc.Service).Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<List<ServiceCategory>> Search(string categoryCode)
        {
            return await _context.ServiceCategories
                .Where(sc => string.IsNullOrEmpty(categoryCode) || sc.CategoryCode == categoryCode)
                .Include(sc => sc.Service)
                .Include(sc => sc.Category)
                .ToListAsync();
        }
    }
}
