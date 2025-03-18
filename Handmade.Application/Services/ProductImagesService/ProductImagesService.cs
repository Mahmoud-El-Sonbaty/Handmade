using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.Application.Services.Products;
using Handmade.Models.ProductH;
using Handmade.DTOs.ProductImagesDTOs;
using Handmade.DTOs.SharedDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handmade.DTOs.ProductDTOs;

namespace Handmade.Application.Services.ProductImages
{
    public class ProductImagesService : IProductImagesService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImagesService(IProductImageRepository productImageRepository , IMapper mapper )
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;


        }
        public async Task<ResultView<ProductImageDTO>> CreateProductImageAsync(ProductImageDTO entity)
        {
            var product = _mapper.Map<ProductImage>(entity);
            var created = await _productImageRepository.CreateAsync(product);
            await _productImageRepository.SaveChangesAsync();
            var result = _mapper.Map<ProductImageDTO>(created);
            return new ResultView<ProductImageDTO> { Data = result, IsSuccess = true };

        }

        public async Task<ResultView<ProductImageDTO>> DeleteProductImageAsync(ProductImageDTO entity)
        {
            var Product = _mapper.Map<ProductImage>(entity);
            var Deleted = _productImageRepository.DeleteAsync(Product);
            await _productImageRepository.SaveChangesAsync();
            var result = _mapper.Map<ProductImageDTO>(Deleted);
            return new ResultView<ProductImageDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ICollection<ProductImageDTO>>> GetAllProductImagesAsync()
        {
            var productReviews = await _productImageRepository.GetAllAsync();
            var result = _mapper.Map<List<ProductImageDTO>>(productReviews);
            return new ResultView<ICollection<ProductImageDTO>> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ProductImageDTO>> GetProductImageByIdAsync(int id)
        {
            var prouct = await _productImageRepository.GetByIdAsync(id);
            var result = _mapper.Map<ProductImageDTO>(prouct);
            return new ResultView<ProductImageDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ProductImageDTO>> UpdateProductImageAsync(ProductImageDTO entity)
        {
            var product = _mapper.Map<ProductImage>(entity);
            var updated = _productImageRepository.UpdateAsync(product);
            await _productImageRepository.SaveChangesAsync();
            var result = _mapper.Map<ProductImageDTO>(updated);
            return new ResultView<ProductImageDTO> { Data = result, IsSuccess = true };
        }
    }
}
