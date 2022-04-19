using ServiceRec.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Business.Interfaces
{
    public interface IServiceBusinessService
    {
        Task<List<Service>> GetAllServiceAsync();
        Task<Service> GetServiceAsync(int id);
        Task<Service> CreateServiceAsync(Service service);
        Task<Service> UpdateServiceAsync(Service service);
        Task<bool> RemoveServiceAsync(int id);
        Task<bool> ServiceExistsAsync(int id);
    }
}
