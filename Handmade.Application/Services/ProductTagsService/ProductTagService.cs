using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.ProductTagsDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.ProductTagsService
{
    public class ProductTagService : IProductTagService
    {
        private readonly IProductTagRepository _productTagRepository;
        private readonly IMapper _mapper;
        public ProductTagService(IProductTagRepository productTagRepository, IMapper mapper)
        {
            _productTagRepository = productTagRepository;
            _mapper = mapper;
        }


        public async Task<ResultView<CRUDProductTagDTOs>> CreateProductTagAsync(CRUDProductTagDTOs entity)
        {
            var product = _mapper.Map<ProductTag>(entity);
            var created = await _productTagRepository.CreateAsync(product);
            await _productTagRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDProductTagDTOs>(created);
            return new ResultView<CRUDProductTagDTOs> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<CRUDProductTagDTOs>> DeleteProductTagAsync(CRUDProductTagDTOs entity)
        {
            var Product = _mapper.Map<ProductTag>(entity);
            var Deleted = _productTagRepository.DeleteAsync(Product);
            await _productTagRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDProductTagDTOs>(Deleted);
            return new ResultView<CRUDProductTagDTOs> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ICollection<CRUDProductTagDTOs>>> GetAllProductTagsAsync()
        {
            var productReviews = await _productTagRepository.GetAllAsync();
            var result = _mapper.Map<List<CRUDProductTagDTOs>>(productReviews);
            return new ResultView<ICollection<CRUDProductTagDTOs>> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<CRUDProductTagDTOs>> GetProductTagByIdAsync(int id)
        {
            var prouct = await _productTagRepository.GetByIdAsync(id);
            var result = _mapper.Map<CRUDProductTagDTOs>(prouct);
            return new ResultView<CRUDProductTagDTOs> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ICollection<CRUDProductTagDTOs>>> SearchProductTagByNameAsync(string name)
        {
            var Tag = await _productTagRepository.GetAllAsync();
            var search = Tag
                 .Where(b => b.TagName.Contains(name, StringComparison.OrdinalIgnoreCase)) //تحدد الاحرف   ان كانت حساسه ا 
                 .ToList();
            var result = _mapper.Map<ICollection<CRUDProductTagDTOs>>(search);

            return new ResultView<ICollection<CRUDProductTagDTOs>> { Data = result, IsSuccess = true }; ;
        }

        public async Task<ResultView<CRUDProductTagDTOs>> UpdateProductTagsAsync(CRUDProductTagDTOs entity)
        {
            var product = _mapper.Map<ProductTag>(entity);
            var updated = _productTagRepository.UpdateAsync(product);
            await _productTagRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDProductTagDTOs>(updated);
            return new ResultView<CRUDProductTagDTOs> { Data = result, IsSuccess = true };
        }
    }
}
