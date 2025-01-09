using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.ProductReviewServices
{
    public class ProductReviewService(IProductReviewRepository productReviewRepository, IMapper mapper) : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository = productReviewRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultView<GCUProductReviewDTO>> CreateProductReviewAsync(GCUProductReviewDTO productReviewDTO)
        {
            try
            {
                if (productReviewDTO == null)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Invalid data" };
                }
                else if ((await _productReviewRepository.GetAllAsync()).Any(pr => pr.UserId == productReviewDTO.UserId && pr.ProductId == pr.ProductId))
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "User already reviewd this product" };
                }

                ProductReview productReview = _mapper.Map<ProductReview>(productReviewDTO);
                ProductReview createdReview = await _productReviewRepository.CreateAsync(productReview);
                await _productReviewRepository.SaveChangesAsync();
                GCUProductReviewDTO result = _mapper.Map<GCUProductReviewDTO>(createdReview);
                return new ResultView<GCUProductReviewDTO> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<GCUProductReviewDTO>> UpdateProductReviewAsync(GCUProductReviewDTO productReviewDTO)
        {
            try
            {
                if (productReviewDTO == null || productReviewDTO.Id <= 0)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Invalid data" };
                }
                else if ((await _productReviewRepository.GetAllAsync()).Any(pr => pr.UserId == productReviewDTO.UserId && pr.ProductId == pr.ProductId))
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "User already reviewd this product" };
                }

                ProductReview productReview = _mapper.Map<ProductReview>(productReviewDTO);
                ProductReview updatedReview = await _productReviewRepository.UpdateAsync(productReview);
                await _productReviewRepository.SaveChangesAsync();
                GCUProductReviewDTO result = _mapper.Map<GCUProductReviewDTO>(updatedReview);
                return new ResultView<GCUProductReviewDTO> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<GCUProductReviewDTO>> DeleteProductReviewAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Invalid ID" };
                }

                ProductReview? productReview = (await _productReviewRepository.GetAllAsync()).FirstOrDefault(pr => pr.Id == id);
                if (productReview == null)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Product review not found" };
                }

                await _productReviewRepository.DeleteAsync(productReview);
                await _productReviewRepository.SaveChangesAsync();
                GCUProductReviewDTO result = _mapper.Map<GCUProductReviewDTO>(productReview);
                return new ResultView<GCUProductReviewDTO> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<List<GCUProductReviewDTO>>> GetAllProductReviewsAsync()
        {
            try
            {
                List<ProductReview> productReviews = [.. (await _productReviewRepository.GetAllAsync())];
                List<GCUProductReviewDTO> result = _mapper.Map<List<GCUProductReviewDTO>>(productReviews);
                return new ResultView<List<GCUProductReviewDTO>> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<List<GCUProductReviewDTO>> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<GCUProductReviewDTO>> GetProductReviewByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Invalid ID" };
                }

                ProductReview? productReview = (await _productReviewRepository.GetAllAsync()).FirstOrDefault(pr => pr.Id == id);
                if (productReview == null)
                {
                    return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = "Product review not found" };
                }

                GCUProductReviewDTO result = _mapper.Map<GCUProductReviewDTO>(productReview);
                return new ResultView<GCUProductReviewDTO> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<GCUProductReviewDTO> { IsSuccess = false, Msg = ex.Message };
            }
        }

        public async Task<ResultView<EntityPaginated<GCUProductReviewDTO>>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var productReviews = (await _productReviewRepository.GetAllAsync()).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                int totalRecords = (await _productReviewRepository.GetAllAsync()).Count();
                EntityPaginated<GCUProductReviewDTO> result = new()
                {
                    Data = [.. _mapper.Map<List<GCUProductReviewDTO>>(productReviews)],
                    Count = totalRecords
                };
                return new ResultView<EntityPaginated<GCUProductReviewDTO>> { Data = result, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ResultView<EntityPaginated<GCUProductReviewDTO>> { IsSuccess = false, Msg = ex.Message };
            }
        }
    }
}
