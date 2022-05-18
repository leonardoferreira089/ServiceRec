using ServiceRec.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Data.Entities
{
    public class Client
    {
        public Client()
        {

        }
        public Client(int id, string clientName, string email, DateTime createdAt, Status status, Service service)
        {
            Id = id;
            ClientName = clientName;
            Email = email;
            CreatedAt = createdAt;
            Status = status;
            Service = service;
        }

        [Key]
        public int Id { get; set; }
        [DisplayName("Client Name")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(50, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string ClientName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(50, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string Email { get; set; }
        [DisplayName("Created At")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Status")]
        public Status Status { get; set; }
        public int ServiceId { get; set; }
        [DisplayName("Service")]
        public Service Service { get; set; }
    }
}
