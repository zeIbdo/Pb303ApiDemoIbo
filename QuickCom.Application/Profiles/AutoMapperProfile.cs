using AutoMapper;
using QuickCom.Application.Dtos;
using QuickCom.Domain.Entities;

namespace QuickCom.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, Product>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap().ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price.HasValue));
            CreateMap<Paginate<Product>, ProductListDto>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<Paginate<Category>, CategryListDto>().ReverseMap();
        }
    }
}
