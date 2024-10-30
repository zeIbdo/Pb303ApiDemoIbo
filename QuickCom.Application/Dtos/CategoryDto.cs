using QuickCom.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Application.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class CategoryCreateDto
    {
        public string Name { get; set; } = null!;
    }
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
    }
    public class CategryListDto : BasePageableDto, IDto
    {
        public List<CategoryDto> Items { get; set; } = new List<CategoryDto>();
    }
}
