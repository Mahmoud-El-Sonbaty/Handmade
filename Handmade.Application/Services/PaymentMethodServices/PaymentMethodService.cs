using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.PaymentMethodDTOs;
using Handmade.DTOs.ProductCategoryDTO;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.PaymentMethodServices
{
    internal class PaymentMethodService(IPaymentMethodRepository PaymentMethodRepository, IMapper mapper) : IPaymentMethodService
    {

        public async Task<ResultView<CRUDPaymetMethodDTO>> CreateAsync(CRUDPaymetMethodDTO PaymentDTO)
        {
            try
            {

                if (PaymentDTO == null)
                {
                    return new ResultView<CRUDPaymetMethodDTO> { IsSuccess = false, Msg = "Invalid data" };
                }

                if (string.IsNullOrWhiteSpace(PaymentDTO.MethodName))
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = "Method name is required."
                    };
                }

                var existingPayment = (await PaymentMethodRepository.FindAsync(
                 pc => pc.MethodEnName.ToLower() == PaymentDTO.MethodName.ToLower() || pc.Id == PaymentDTO.MethodId));


                if (existingPayment != null)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Payment Method with Name '{PaymentDTO.MethodName}' already exists."
                    };
                }


                PaymentMethod paymentMethod = mapper.Map<PaymentMethod>(PaymentDTO);
                PaymentMethod CreatedMethod = await PaymentMethodRepository.CreateAsync(paymentMethod);
                await PaymentMethodRepository.SaveChangesAsync();
                CRUDPaymetMethodDTO resultDto = mapper.Map<CRUDPaymetMethodDTO>(CreatedMethod);
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    Data = resultDto,
                    IsSuccess = true,
                    Msg = $"Payment Method '{resultDto.MethodName}' created successfully."
                };
            }

            catch (Exception ex)
            {

                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while creating Payment Method: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResultView<List<CRUDPaymetMethodDTO>>> GetAllAsync()
        {
            try
            {
                var productCategories = (await PaymentMethodRepository.GetAllAsync()).Where(pc => !pc.IsDeleted);
                List<CRUDPaymetMethodDTO> result = mapper.Map<List<CRUDPaymetMethodDTO>>(productCategories);
                return new ResultView<List<CRUDPaymetMethodDTO>>
                {
                    Data = result,
                    IsSuccess = true,
                    Msg = "Payment Methods retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<List<CRUDPaymetMethodDTO>>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while retrieving Payment Methods: {ex.Message}"
                };
            }
        }

        public async Task<ResultView<CRUDPaymetMethodDTO>> GetOneAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Invalid ID: {id}. ID must be greater than zero."
                    };
                }

                var PaymetMethod = await PaymentMethodRepository.FindAsync(pc => pc.Id == id && !pc.IsDeleted);
                if (PaymetMethod == null)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Payment Method with ID {id} not found or has been deleted."
                    };
                }

                CRUDPaymetMethodDTO result = mapper.Map<CRUDPaymetMethodDTO>(PaymetMethod);
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    Data = result,
                    IsSuccess = true,
                    Msg = $"Payment Method with ID {id} retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while retrieving Payment Method with ID {id}: {ex.Message}"
                };
            }

        }


        public async Task<ResultView<CRUDPaymetMethodDTO>> HeardDeleteAsync(CRUDPaymetMethodDTO Entity)
        {
            try
            {
                if (Entity == null || Entity.MethodId < 1)
                {

                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = "Invalid payment method data. MethodId is required and must be greater than zero."
                    };
                }

                PaymentMethod existPaymentMthod = await PaymentMethodRepository.FindAsync(pc => pc.Id == Entity.MethodId);

                if (existPaymentMthod == null)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Payment method with ID '{Entity.MethodId}' does not exist."
                    };
                }

                var delete = await PaymentMethodRepository.DeleteAsync(existPaymentMthod);
                await PaymentMethodRepository.SaveChangesAsync();
                

                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = true,
                    Msg = $"Payment method with ID '{Entity.MethodId}' deleted successfully.",
                    Data = Entity

                };
            
            }
            catch (Exception ex) 
            {
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while creating Payment Method: {ex.Message}",
                    Data = null
                };


            }

         

        }

        public async Task<ResultView<CRUDPaymetMethodDTO>> SoftDeleteAsync(CRUDPaymetMethodDTO Entity)
        {
            try
            {

                if (Entity == null || Entity.MethodId <= 0)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = "Invalid Method or ID"
                    };
                }

                // ابحث عن الكائن
                var existingMethod = await PaymentMethodRepository.FindAsync(pc => pc.Id == Entity.MethodId && !pc.IsDeleted);
                if (existingMethod == null)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Product Category with ID {Entity.MethodId} not found or already deleted"
                    };
                }


                existingMethod.IsDeleted = true;
                await PaymentMethodRepository.UpdateAsync(existingMethod);
                await PaymentMethodRepository.SaveChangesAsync();


                var resultDto = mapper.Map<CRUDPaymetMethodDTO>(existingMethod);
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    Data = resultDto,
                    IsSuccess = true,
                    Msg = $"Product Category with ID {Entity.MethodId} soft deleted successfully"
                };
            }
            catch (Exception ex)
            {

                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while soft deleting Product Category with ID {Entity.MethodId}: {ex.Message}"
                };

            }
        }




        public async Task<ResultView<CRUDPaymetMethodDTO>> UpdateAsync(CRUDPaymetMethodDTO entity)
        {
            try
            {
                if (entity == null || entity.MethodId <= 0)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = "Invalid entity or ID"
                    };
                }
                if (string.IsNullOrWhiteSpace(entity.MethodName))
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = "Payment method name is required"
                    };
                }

                var existingMethod = await PaymentMethodRepository.FindAsync(pc => pc.Id == entity.MethodId && !pc.IsDeleted);
                if (existingMethod == null)
                {
                    return new ResultView<CRUDPaymetMethodDTO>
                    {
                        IsSuccess = false,
                        Msg = $"Product Category with ID {entity.MethodId} not found or has been deleted"
                    };
                }



                mapper.Map(entity, existingMethod);
                await PaymentMethodRepository.UpdateAsync(existingMethod);
                await PaymentMethodRepository.SaveChangesAsync();

                var resultDto = mapper.Map<CRUDPaymetMethodDTO>(existingMethod);
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    Data = resultDto,
                    IsSuccess = true,
                    Msg = $"Product Category with ID {entity.MethodId} updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<CRUDPaymetMethodDTO>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while updating Product Category with ID {entity.MethodId}: {ex.Message}"
                };
            }
        }



        public async Task<ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                    {
                        IsSuccess = false,
                        Msg = "Page number and page size must be greater than zero"
                    };
                }

                var query = (await PaymentMethodRepository.GetAllAsync())
                    .Where(pc => !pc.IsDeleted)
                    .OrderBy(pc => pc.MethodId);

                int totalCount = await query.CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (pageNumber > totalPages && totalCount > 0)
                {
                    return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                    {
                        IsSuccess = false,
                        Msg = $"Page number {pageNumber} exceeds total pages ({totalPages})"
                    };
                }

                if (totalCount == 0)
                {
                    return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                    {
                        Data = new EntityPaginated2<CRUDPaymetMethodDTO>
                        {
                            Items = new List<CRUDPaymetMethodDTO>(),
                            TotalCount = 0,
                            PageNumber = pageNumber,
                            PageSize = pageSize
                        },
                        IsSuccess = true,
                        Msg = "No Payment Methods found"
                    };
                }

                var paginatedItems = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();


                var itemsDto = mapper.Map<List<CRUDPaymetMethodDTO>>(paginatedItems);

                var paginatedResult = new EntityPaginated2<CRUDPaymetMethodDTO>
                {
                    Items = itemsDto,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                {
                    Data = paginatedResult,
                    IsSuccess = true,
                    Msg = $"Page {pageNumber} of Payment Methods retrieved successfully 😎"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while retrieving paginated Payment Methods: {ex.Message} 💀"
                };
            }
        }


        public async Task<ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>> GetPaginatedAsync(int pageNumber, int pageSize, int MethodId)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                    {
                        IsSuccess = false,
                        Msg = "Page number and page size must be greater than zero"
                    };
                }
                if (MethodId < 1)
                {
                    return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                    {
                        IsSuccess = false,
                        Msg = "Payment Method Id  must be greater than zero"
                    };
                }

                var query = (await PaymentMethodRepository.GetAllAsync())
                    .Where(pc => !pc.IsDeleted && MethodId == pc.MethodId);

                int totalCount = await query.CountAsync();

                var paginatedItems = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();


                var itemsDto = mapper.Map<List<CRUDPaymetMethodDTO>>(paginatedItems);

                var paginatedResult = new EntityPaginated2<CRUDPaymetMethodDTO>
                {
                    Items = itemsDto,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                {
                    Data = paginatedResult,
                    IsSuccess = true,
                    Msg = $"Page {pageNumber} of Payment Methods retrieved successfully 😎"
                };
            }
            catch (Exception ex)
            {
                return new ResultView<EntityPaginated2<CRUDPaymetMethodDTO>>
                {
                    IsSuccess = false,
                    Msg = $"Error occurred while retrieving paginated Payment Methods: {ex.Message} 💀"
                };
            }
        }

            


    }
}
