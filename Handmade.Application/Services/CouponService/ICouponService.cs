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
    public interface ICouponService
    {
        Task<ResultView<CreateCouponDTO>> CreateCouponAsync(CreateCouponDTO entity);
        Task<ResultView<CRUDCouponDTO>> DeleteCouponAsync(CRUDCouponDTO entity);
        Task<ResultView<ICollection<CRUDCouponDTO>>> GetAllCouponAsync();
        Task<ResultView<UpdateCouponDTO>> UpdateCouponAsync(UpdateCouponDTO entity);
        Task<ResultView<CRUDCouponDTO>> GetCouponByIdAsync(int id); // resultView is wrapper class

        Task<ResultView<CRUDCouponDTO>> ValidateCouponAsync(string couponCode, int userId, decimal orderTotal);

    }
}
