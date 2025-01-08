using AutoMapper;

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
        }
    }
}
