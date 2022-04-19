using ServiceRec.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Business.Interfaces
{
    public interface IClientBusinessService
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(Client client);
        Task<bool> RemoveClientAsync(int id);
        Task<bool> ClientExistsAsync(int id);
    }
}
