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
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Service Name")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(50, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string ServiceName { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(50, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public decimal Price { get; set; }
        [DisplayName("Type")]
        [StringLength(30, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public ServiceType Type { get; set; }
    }
}
