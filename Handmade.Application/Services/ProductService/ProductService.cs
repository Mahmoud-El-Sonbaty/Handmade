using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.Models.ProductH;
using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.Products
{
    public class ProductService : IproductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository , IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

      

        public async Task<ResultView<CRUDProductDTOs>> CreateProductAsync(CRUDProductDTOs productDTO)
        {
          
                var product = _mapper.Map<Product>(productDTO);
                var created = await _productRepository.CreateAsync(product);
                await _productRepository.SaveChangesAsync();
                var result = _mapper.Map<CRUDProductDTOs>(created);
                return new ResultView<CRUDProductDTOs> { Data = result, IsSuccess = true };

            }

      public  async Task<ResultView<CRUDProductDTOs>> DeleteProductAsync(CRUDProductDTOs entity)
        {
            var Product = _mapper.Map<Product>(entity);
            var Deleted = _productRepository.DeleteAsync(Product);
            await _productRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDProductDTOs>(Deleted);
            return new ResultView<CRUDProductDTOs> { Data = result, IsSuccess = true };
        }


     public async Task<ResultView<ICollection<GetAllProductsDTOs>>> GetAllProductAsync()
        {
                  var productReviews = await _productRepository.GetAllAsync();
                var result = _mapper.Map<List<GetAllProductsDTOs>>(productReviews);
                return new ResultView<ICollection<GetAllProductsDTOs>> { Data = result, IsSuccess = true };
   
        }

        public async Task<ResultView<ICollection<CRUDProductDTOs>>> GetByNameAsync(string name)
        {
            var book = await _productRepository.GetAllAsync();
            var search = book
                 .Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) //تحدد الاحرف   ان كانت حساسه ا 
                 .ToList();
            var result = _mapper.Map<ICollection<CRUDProductDTOs>>(search);

            return new ResultView<ICollection<CRUDProductDTOs>> { Data = result, IsSuccess = true }; ;

        }

       public async Task<ResultView<CRUDProductDTOs>> UpdateProductAsync(CRUDProductDTOs entity)
        {
            var product = _mapper.Map<Product>(entity);
            var updated = _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDProductDTOs>(updated);
            return new ResultView<CRUDProductDTOs> { Data = result, IsSuccess = true };
        }

      public async Task<ResultView<ICollection<GetAllProductsDTOs>>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var GetByPrice =await  _productRepository.GetAllAsync();
            var filteredProducts = GetByPrice
                    .Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            var result = _mapper.Map<ICollection<GetAllProductsDTOs>>(filteredProducts);

            return new ResultView<ICollection<GetAllProductsDTOs>> { Data = result, IsSuccess = true };

        }

      public async Task<ResultView<GetOneProductDTOs>> GetProductByIdAsync(int id)
        {
            var prouct = await _productRepository.GetByID(id);
           var result=  _mapper.Map<GetOneProductDTOs>(prouct);
            return new ResultView<GetOneProductDTOs> { Data = result, IsSuccess = true };

        }

        public async Task<ResultView<EntityPaginated<GetOneProductDTOs>>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetAllAsync();

            var count = products.Count();
            int totalCount = count;

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            var result = _mapper.Map <EntityPaginated<GetOneProductDTOs>>(paginatedProducts);
        
         
            return new ResultView<EntityPaginated<GetOneProductDTOs>> { Data = result, IsSuccess = true };
        }



    }
}
