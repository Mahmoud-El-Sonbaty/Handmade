using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{

    public class PaymentMethod:BaseEntity<int>
    {
        public int MethodId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string MethodEnName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string MethodArName { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }

    }
}
