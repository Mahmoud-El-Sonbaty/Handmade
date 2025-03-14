using AutoMapper;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.ProductDTOs;
using Handmade.Models;
using Handmade.Models.ProductH;
using Handmade.DTOs.ProductImagesDTOs;
using Handmade.DTOs.ProductTagsDTOs;
using Handmade.DTOs.CouponsDTOs;

namespace Handmade.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Category
            //CreateMap<CUCategoryDTO, Category>().ReverseMap();
            //CreateMap<GetAllCategoryDTO, Category>().ReverseMap();
            //CreateMap<GetOneCategoryDTO, Category>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region ProductReview
            CreateMap<GCUProductReviewDTO, ProductReview>().ReverseMap();
            #endregion

            #region Product
            CreateMap<GetAllProductsDTOs, Product>().ReverseMap();
            CreateMap<GetOneProductDTOs, Product>().ReverseMap();
            CreateMap<Product, CRUDProductDTOs>().ReverseMap()
             .ForMember(dest => dest.ProductTagMappings, opt => opt.MapFrom(src => src.Tags.Select(ptm => ptm.TagName)));
            #endregion

            #region ProductImage
            CreateMap<ProductImageDTO, ProductImage>().ReverseMap().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            #endregion

            #region ProductTag
            CreateMap<ProductTag, CRUDProductTagDTOs>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductTagMappings.Select(ptm => ptm.Product)));
            #endregion

            #region ProductTagMapping
            CreateMap<ProductTagMapping, ProductTagMappingDTO>().ReverseMap();
            #endregion

            #region Coupon 
            CreateMap<CRUDCouponDTO, Coupon>().ReverseMap();
            CreateMap<UpdateCouponDTO, Coupon>().ReverseMap();
            CreateMap<CreateCouponDTO, Coupon>().ReverseMap();
            #endregion
        }
    }
}
