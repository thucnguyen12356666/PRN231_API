using SCBS.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCBS.Repositories.Models;
using SCBS.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace SCBS.Repositories
{
    public class ServiceRepository : GenericRepository<Service>
    {
        public ServiceRepository() { }
        public new async Task<List<Service>> GetAllAsync()
        {
            var services = await _context.Services.Include(s => s.ServiceCategories).Include(s => s.User).ToListAsync();
            return services;
        }
        public new async Task<Service?> GetByIdAsync(Guid id) => await _context.Services.Include(s => s.ServiceCategories).FirstOrDefaultAsync(s => s.Id == id);
        public async Task<List<Service>> Search(string name, string description)
        {
            var services = await _context.Services
                .Where(s => (string.IsNullOrEmpty(name) || s.Name == name)
                            && (string.IsNullOrEmpty(description) || s.Description == description))
                .Include(s => s.ServiceCategories)
                .ToListAsync();
            return services;
        }
    }
}
