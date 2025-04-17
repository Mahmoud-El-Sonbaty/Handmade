using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.PaymentMethodDTOs
{
    public class CRUDPaymetMethodDTO
    {
        
        public int MethodId { get; set; }
        [Required]
        public string MethodName { get; set; }
        public string? Description { get; set; }
    

    }
}
