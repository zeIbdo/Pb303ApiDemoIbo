using QuickCom.Persistence.Repositories.Generic;

namespace QuickCom.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int Price { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductUpdateDto
    {
        public string? Name { get; set; } 
        public int Price { get; set; }
        public int CategoryId { get; set; }
    }
    public class ProductListDto : BasePageableDto, IDto
    {
        public List<ProductDto> Items { get; set; } = new List<ProductDto>();
    }
}
