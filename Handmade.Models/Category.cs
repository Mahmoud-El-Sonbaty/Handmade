using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Models
{
    public class Category :BaseEntity<int>
    {
        [MaxLength(50)]
        public string EnName { get; set; }
        [MaxLength(50)]
        public string ArName { get; set; }

        public bool IsParent { get; set; } =false;

        public int ParentCategoryId { get; set; } = 0;
        public int Level { get; set; } = 0;

        [MaxLength(100)]

        public string? CatogorylogoPath { get; set; }


        //public virtual ICollection<CategoryProduct>? Products { get; set; }
        //public virtual ICollection<CategoryBrands>? Brands { get; set; }


    }
}
