using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.CouponsDTOs;
using Handmade.DTOs.ProductDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.CouponService
{
    class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ICouponUsageRepository _couponUsageRepository;
        private readonly IMapper _mapper;
        public CouponService(ICouponRepository couponRepository,ICouponUsageRepository couponUsageRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _couponUsageRepository = couponUsageRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<CreateCouponDTO>> CreateCouponAsync(CreateCouponDTO entity)
        {
            var coupon = _mapper.Map<Coupon>(entity);
            var created = await _couponRepository.CreateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            var result = _mapper.Map<CreateCouponDTO>(created);
            return new ResultView<CreateCouponDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<CRUDCouponDTO>> DeleteCouponAsync(CRUDCouponDTO entity)
        {
            var coupon = _mapper.Map<Coupon>(entity);
            var Deleted = _couponRepository.DeleteAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            var result = _mapper.Map<CRUDCouponDTO>(Deleted);
            return new ResultView<CRUDCouponDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ICollection<CRUDCouponDTO>>> GetAllCouponAsync()
        {
            var coupon = await _couponRepository.GetAllAsync();
            var result = _mapper.Map<List<CRUDCouponDTO>>(coupon);
            return new ResultView<ICollection<CRUDCouponDTO>> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<CRUDCouponDTO>> GetCouponByIdAsync(int id)
        {
            var coupon = await _couponRepository.GetByID(id);
            var result = _mapper.Map<CRUDCouponDTO>(coupon);
            return new ResultView<CRUDCouponDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<UpdateCouponDTO>> UpdateCouponAsync(UpdateCouponDTO entity)
        {
            var coupon = _mapper.Map<Coupon>(entity);
            var updated = _couponRepository.UpdateAsync(coupon);
            await _couponRepository.SaveChangesAsync();
            var result = _mapper.Map<UpdateCouponDTO>(updated);
            return new ResultView<UpdateCouponDTO> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<CRUDCouponDTO>> ValidateCouponAsync(string couponCode, int userId, decimal orderTotal)
        {
            throw new NotImplementedException();
        }

        
    }
}
 