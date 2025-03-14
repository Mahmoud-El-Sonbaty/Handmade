using AutoMapper;
using Handmade.Application.Contracts;
using Handmade.DTOs.ProductTagsDTOs;
using Handmade.DTOs.RefundsDTOs;
using Handmade.DTOs.SharedDTOs;
using Handmade.Models;
using Handmade.Models.ProductH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.RefundsServices
{
    public class RefundService : IRefundService
    {
        private readonly IRefundsRepository _refundsRepository;
        private readonly IMapper _mapper;

        public RefundService(IRefundsRepository refundsRepository , IMapper mapper)
        {
            _refundsRepository = refundsRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<RefundCreateDto>>CreateRefundAsync(RefundCreateDto refundDto)
        {
            var refund = _mapper.Map<Refund>(refundDto);
            var created = await _refundsRepository.CreateAsync(refund);
            await _refundsRepository.SaveChangesAsync();
            var result = _mapper.Map<RefundCreateDto>(created);
            return new ResultView<RefundCreateDto> { Data = result, IsSuccess = true };
        }

        public async Task<bool> DeleteRefundAsync(int id)
        {
            var refund = await _refundsRepository.GetRefundByIdAsync(id);
            if (refund == null)
                return false;

            await _refundsRepository.DeleteAsync(refund);
            return true;
        }

        public async Task<ResultView<ICollection<RefundReadDto>>>GetAllRefundsAsync()
        {
            var Refunds = await _refundsRepository.GetAllAsync();
            var result = _mapper.Map<List<RefundReadDto>>(Refunds);
            return new ResultView<ICollection<RefundReadDto>> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<RefundReadDto>> GetRefundByIdAsync(int id)
        {
            var refund = await _refundsRepository.GetRefundByIdAsync(id);
            var result = _mapper.Map<RefundReadDto>(refund);
            return new ResultView<RefundReadDto> { Data = result, IsSuccess = true };

        }

        public async Task<ResultView<ICollection<RefundReadDto>>> GetRefundsByOrderIdAsync(int orderId)
        {
            var refunds = await _refundsRepository.GetRefundsByOrderIdAsync(orderId);
            var result = _mapper.Map<ICollection<RefundReadDto>>(refunds);
            return new ResultView<ICollection<RefundReadDto>> { Data = result, IsSuccess = true };
        }

        public async Task<ResultView<ICollection<RefundReadDto>>> GetRefundsByUserIdAsync(int userId)
        {
            var refunds = await _refundsRepository.GetRefundsByUserIdAsync(userId);
            var result = _mapper.Map<ICollection<RefundReadDto>>(refunds);
            return new ResultView<ICollection<RefundReadDto>> { Data = result, IsSuccess = true };

        }
    }

}
