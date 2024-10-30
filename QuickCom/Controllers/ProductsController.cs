using Microsoft.AspNetCore.Mvc;
using QuickCom.Application.Dtos;
using QuickCom.Application.Services.Abstractions;

namespace QuickCom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService  _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPage([FromQuery] PageRequest pageRequest)
        {
            var prodList = await _productService.GetListAsync(index: pageRequest.Index, size: pageRequest.Size);

            return Ok(prodList);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return NotFound();

            var prod = await _productService.GetAsync(id.Value);

            return Ok(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto createDto)
        {
            var created = await _productService.AddAsync(createDto);

            return Ok(created);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put(int? id, [FromBody] ProductUpdateDto updateDto)
        {
            if (id == null) return NotFound();

            var updated = await _productService.UpdateAsync(id.Value, updateDto);

            return Ok(updated);
        }
    }
}

