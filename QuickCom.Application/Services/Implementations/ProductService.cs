using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using QuickCom.Application.Dtos;
using QuickCom.Application.Services.Abstractions;
using QuickCom.Domain.Entities;
using QuickCom.Persistence.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository  _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(ProductCreateDto createDto)
        {
            var entity = _mapper.Map<Product>(createDto);
            var createdProduct= await _productRepository.AddAsync(entity);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto> DeleteAsync(int id)
        {
            var existProd= await _productRepository.GetAsync(id);

            if (existProd == null) throw new Exception("Not found");

            var deletedProduct= await _productRepository.DeleteAsync(existProd);

            return _mapper.Map<ProductDto>(deletedProduct);
        }

        public async Task<ProductDto?> GetAsync(int id)
        {
            var entity = await _productRepository.GetAsync(id);

            if (entity == null) throw new Exception("Not found");

            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
        {
            var entity = await _productRepository.GetAsync(predicate, include);

            if (entity == null) throw new Exception("Not found");

            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductListDto> GetListAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
        {
            var listEntity = await _productRepository.GetListAsync(predicate, orderBy, include, index, size, enableTracking);

            if (listEntity == null) throw new Exception("Not found");

            return _mapper.Map<ProductListDto>(listEntity);
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductUpdateDto updateDto)
        {
            var existProd= await _productRepository.GetAsync(id);

            if (existProd == null) throw new Exception("Not found");

            existProd = _mapper.Map(updateDto, existProd);

            var updatedProd= await _productRepository.UpdateAsync(existProd);

            return _mapper.Map<ProductDto>(updatedProd);
        }
    }
}
