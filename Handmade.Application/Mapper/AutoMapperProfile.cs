using AutoMapper;
using Handmade.DTOs.CategoryDTOs;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.Models;

namespace Handmade.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Category

            CreateMap<CRUDCategoriesDTO, Category>().ReverseMap()
               .ForMember(dest => dest.EnName, opt => opt.MapFrom(src => src.EnName))
               .ForMember(dest => dest.IsParent, opt => opt.MapFrom(src => src.IsParent))
               .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
               .ForMember(dest => dest.ArName, opt => opt.MapFrom(src => src.ArName))
               .ForMember(dest => dest.CatogorylogoPath, opt => opt.MapFrom(src => src.CatogorylogoPath))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
            #endregion

            #region ProductReview
            CreateMap<GCUProductReviewDTO, ProductReview>().ReverseMap();
            #endregion
        }
    }
}
