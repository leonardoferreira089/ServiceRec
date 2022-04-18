using ServiceRec.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
