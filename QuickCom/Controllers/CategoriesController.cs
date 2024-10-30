using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCom.Application.Dtos;
using QuickCom.Application.Services.Abstractions;
using System.Data;

namespace QuickCom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPage([FromQuery] PageRequest pageRequest)
        {
            var categoryList = await _categoryService.GetListAsync(index: pageRequest.Index, size: pageRequest.Size);

            return Ok(categoryList);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return NotFound();

            var category = await _categoryService.GetAsync(id.Value);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto createDto)
        {
            var created= await _categoryService.AddAsync(createDto);

            return Ok(created);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, [FromBody] CategoryUpdateDto updateDto)
        {
            if (id == null) return NotFound();

            var updated= await _categoryService.UpdateAsync(id.Value, updateDto);

            return Ok(updated);
        }
    }
}
