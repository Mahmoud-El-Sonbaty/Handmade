using Handmade.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.SharedDTOs
{
    public class ResultView<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Msg { get; set; }

        public static implicit operator List<T>(ResultView<List<GetAllCategoriesDTO>> v)
        {
            throw new NotImplementedException();
        }
    }
}
