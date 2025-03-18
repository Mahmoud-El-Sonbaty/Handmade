using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
        //public ICollection<Product> Products { get; set; }


    }
}
