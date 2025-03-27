using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class AdDetails : BaseEntity<int>
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(600)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public string? ImageUrl { get; set; }
    }
}
