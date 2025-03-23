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
        public bool IsSuccess { get; set; } = false;
        public string? Msg { get; set; }
    }
}
