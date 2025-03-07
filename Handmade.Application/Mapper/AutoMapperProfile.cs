using AutoMapper;
using Handmade.DTOs.ProductReviewDTOs;
using Handmade.DTOs.ProductDTOs;
using Handmade.Models;
using Handmade.Models.ProductH;

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
            #endregion
        }
    }
}
