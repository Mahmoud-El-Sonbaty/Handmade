using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.BrandDTOs;
using Handmade.DTOs.ProductReviewDTOs;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRebository;
        private readonly IMapper _mapper; 
        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRebository = brandRepository;
            _mapper = mapper;
        }


        public async Task<ResultView<BrandDTO>> CreateAsync(CreateBrandDTO brandDTO)
        {
            ResultView<BrandDTO> result = new();
            try
            {
                // Check if a Brand with the same name exists
                bool exists = (await _brandRebository.GetSortedFilterAsync(p => p.Id, d => d.Name == brandDTO.Name)).Any();
                if (exists)
                {
                    result = new ResultView<BrandDTO>
                    {
                        IsSuccess = false,
                        Msg = "Brand with the same name already exists"
                    };
                    return result;
                }
                // Map DTO to brand entity and create it
                var brand = _mapper.Map<Brand>(brandDTO);
                var createdBrand = await _brandRebository.CreateAsync(brand);
                await _brandRebository.SaveChangesAsync();

                // Map back to DTO and return
                var returnBrand = _mapper.Map<BrandDTO>(createdBrand);
                result = new ResultView<BrandDTO>
                {
                    
                    Data = returnBrand,
                    IsSuccess = true,
                    Msg = "Brand created successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<BrandDTO>
                {
                    IsSuccess = false,
                    Msg = "Error occurred: " + ex.Message
                };
                return result;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var brand = await _brandRebository.GetOneAsync(id);
                if (brand == null)
                {
                    return false;
                }
                await _brandRebository.DeleteAsync(brand);
                await _brandRebository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResultView<List<BrandDTO>>> GetAllAsync()
        {
            var brands = await _brandRebository.GetAllAsync();
            var brandDTOs = _mapper.Map<List<BrandDTO>>(brands);

            return new ResultView<List<BrandDTO>>
            {
                IsSuccess = true,
                Msg = "Brands retrieved successfully",
                Data = brandDTOs
            };
        }


        public async Task<ResultView<BrandDTO>> GetByIdAsync(int id)
        {
            var result = new ResultView<BrandDTO>();

            try
            {
                var brand = await _brandRebository.GetOneAsync(id);

                if (brand == null)
                {
                    result.IsSuccess = false;
                    result.Msg = "Brand not found.";
                    return result;
                }

                // Map to DTO
                var brandDTO = _mapper.Map<BrandDTO>(brand);

                result.IsSuccess = true;
                result.Msg = "Brand retrieved successfully.";
                result.Data = brandDTO;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = $"An error occurred: {ex.Message}";
            }

            return result;
        }

        public async Task<ResultView<BrandDTO>> UpdateAsync(BrandDTO brandDTO)
        {
            var result = new ResultView<BrandDTO>();

            try
            {
                var existingBrand = await _brandRebository.GetOneAsync(brandDTO.Id);
                if (existingBrand == null)
                {
                    result.IsSuccess = false;
                    result.Msg = "Brand not found.";
                    return result;
                }

                existingBrand.Name = brandDTO.Name;

                var updatedBrand = await _brandRebository.UpdateAsync(existingBrand);

                var updatedBrandDTO = _mapper.Map<BrandDTO>(updatedBrand);

                result.IsSuccess = true;
                result.Msg = "Brand updated successfully.";
                result.Data = updatedBrandDTO;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = $"An error occurred: {ex.Message}";
            }

            return result;
        }

        public async Task<IQueryable<Brand>> GetSortedFilterAsync<TKey>(Expression<Func<Brand, TKey>> orderBy, Expression<Func<Brand, bool>> searchPredicate = null, bool ascending = true)
        {
            return await _brandRebository.GetSortedFilterAsync(orderBy, searchPredicate, ascending);

        }

    }
}
