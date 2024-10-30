using AutoMapper;
using QuickCom.Application.Dtos;
using QuickCom.Domain.Entities;

namespace QuickCom.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            //CreateMap<Paginate<Product>, ProductListDto>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            //CreateMap<Paginate<Category>, CategryListDto>().ReverseMap();
        }
    }
}
