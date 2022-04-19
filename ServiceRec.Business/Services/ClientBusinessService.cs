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
    public class ClientBusinessService : IClientBusinessService
    {
        private readonly RecDbContext _dbContext;
        public ClientBusinessService(RecDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ClientExistsAsync(int id)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClientAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> RemoveClientAsync(int id)
        {
            var client = await GetClientAsync(id);
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _dbContext.Update(client);
            await _dbContext.SaveChangesAsync();

            return client;

        }
    }
}
