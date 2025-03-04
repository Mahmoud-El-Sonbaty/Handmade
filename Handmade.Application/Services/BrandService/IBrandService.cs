using Handmade.DTOs.BrandDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.BrandService
{
    public interface IBrandService
    {
        Task<ResultView<BrandDTO>> CreateAsync(CreateBrandDTO brandDTO);
        Task<ResultView<BrandDTO>> UpdateAsync(BrandDTO brandDTO);
        Task<bool> DeleteAsync(int id);
        Task<ResultView<List<BrandDTO>>> GetAllAsync();
        Task<ResultView<BrandDTO>> GetByIdAsync(int id);
        public Task<IQueryable<Brand>> GetSortedFilterAsync<TKey>(Expression<Func<Brand, TKey>> orderBy, Expression<Func<Brand, bool>> searchPredicate = null, bool ascending = true);

    }
}
