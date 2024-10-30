using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using QuickCom.Application.Dtos;
using QuickCom.Application.Services.Abstractions;
using QuickCom.Domain.Entities;
using QuickCom.Persistence.Repositories.Abstraction;
using System.Linq.Expressions;

namespace QuickCom.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddAsync(CategoryCreateDto createDto)
        {

            var entity = _mapper.Map<Category>(createDto);
            var created = await _categoryRepository.AddAsync(entity);
            return _mapper.Map<CategoryDto>(created);
        }

        public async Task<CategoryDto> DeleteAsync(int id)
        {
            var exist = await _categoryRepository.GetAsync(id);

            if (exist == null) throw new Exception("Not found");

            var deleted = await _categoryRepository.DeleteAsync(exist);

            return _mapper.Map<CategoryDto>(deleted);
        }

        public async Task<CategoryDto?> GetAsync(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);

            if (entity == null) throw new Exception("Not found");

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryDto?> GetAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
        {
            var entity = await _categoryRepository.GetAsync(predicate, include);

            if (entity == null) throw new Exception("Not found");

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategryListDto> GetListAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
        {
            var listEntity = await _categoryRepository.GetListAsync(predicate, orderBy, include, index, size, enableTracking);

            if (listEntity == null) throw new Exception("Not found");

            return _mapper.Map<CategryListDto>(listEntity);
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateDto)
        {
            var exist = await _categoryRepository.GetAsync(id);

            if (exist == null) throw new Exception("Not found");

            exist = _mapper.Map(updateDto, exist);

            var updated = await _categoryRepository.UpdateAsync(exist);

            return _mapper.Map<CategoryDto>(updated);
        }
    }
}
