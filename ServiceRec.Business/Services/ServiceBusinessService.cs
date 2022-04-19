using Microsoft.EntityFrameworkCore;
using ServiceRec.Business.Interfaces;
using ServiceRec.Data.Context;
using ServiceRec.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Business.Services
{
    public class ServiceBusinessService : IServiceBusinessService
    {
        private readonly RecDbContext _dbContext;
        public ServiceBusinessService(RecDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            _dbContext.Services.Add(service);
            await _dbContext.SaveChangesAsync();

            return service;
        }

        public async Task<List<Service>> GetAllServiceAsync()
        {
            return await _dbContext.Services.ToListAsync();
        }

        public async Task<Service> GetServiceAsync(int id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> RemoveServiceAsync(int id)
        {
            var service = await GetServiceAsync(id);
            _dbContext.Remove(service);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ServiceExistsAsync(int id)
        {
            return await _dbContext.Services.AnyAsync(s => s.Id == id);
        }

        public async Task<Service> UpdateServiceAsync(Service service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();

            return service;
        }
    }
}
