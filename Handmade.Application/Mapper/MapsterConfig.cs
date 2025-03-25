using Handmade.DTOs.AuthDTOs;
using Handmade.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Mapper
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<ClientRegisterDTO, User>.NewConfig()
                .Map(dest => dest.NormalizedEmail, src => src.Email.ToUpper())
                .Map(dest => dest.UserName, src => src.Email.Split('@', StringSplitOptions.None).FirstOrDefault())
                .Map(dest => dest.NormalizedUserName, src => src.Email.Split('@', StringSplitOptions.None).FirstOrDefault()!.ToUpper());
            TypeAdapterConfig<VerifyRegisterTokenDTO, User>.NewConfig()
                .Map(dest => dest.NormalizedEmail, src => src.Email.ToUpper())
                .Map(dest => dest.UserName, src => src.Email.Split('@', StringSplitOptions.None).FirstOrDefault())
                .Map(dest => dest.NormalizedUserName, src => src.Email.Split('@', StringSplitOptions.None).FirstOrDefault()!.ToUpper());
            //TypeAdapterConfig<GCUProductReviewDTO, ProductReview>.NewConfig().TwoWays();
            //    TypeAdapterConfig<GetAllProductsDTOs, Product>.NewConfig().TwoWays();
            //    TypeAdapterConfig<GetOneProductDTOs, Product>.NewConfig().TwoWays();
            //    TypeAdapterConfig<CRUDProductDTOs, Product>.NewConfig()
            //        .Map(dest => dest.ProductTagMappings, src => src.Tags.Select(tagName => new ProductTagMapping { TagName = tagName }))
            //        .TwoWays();

            //    TypeAdapterConfig<ProductImageDTO, ProductImage>.NewConfig()
            //        .Map(dest => dest.ProductName, src => src.Product.Name)
            //        .TwoWays();

            //    TypeAdapterConfig<ProductTag, CRUDProductTagDTOs>.NewConfig()
            //        .Map(dest => dest.Products, src => src.ProductTagMappings.Select(ptm => ptm.Product));

            //    TypeAdapterConfig<ProductTagMapping, ProductTagMappingDTO>.NewConfig().TwoWays();
            //    TypeAdapterConfig<CRUDCouponDTO, Coupon>.NewConfig().TwoWays();
            //    TypeAdapterConfig<UpdateCouponDTO, Coupon>.NewConfig().TwoWays();
            //    TypeAdapterConfig<CreateCouponDTO, Coupon>.NewConfig().TwoWays();
            //    TypeAdapterConfig<Brand, BrandDTO>.NewConfig().TwoWays();
            //    TypeAdapterConfig<Brand, CreateBrandDTO>.NewConfig().TwoWays();
            //    TypeAdapterConfig<BrandDTO, CreateBrandDTO>.NewConfig().TwoWays();
            //}
        }
    }
}
