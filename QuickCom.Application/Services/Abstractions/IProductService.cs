using Microsoft.EntityFrameworkCore.Query;
using QuickCom.Application.Dtos;
using QuickCom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Application.Services.Abstractions
{
    public interface IProductService
    {
        Task<ProductDto?> GetAsync(int id);

        Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);

        Task<ProductListDto> GetListAsync(Expression<Func<Product, bool>>? predicate = null,
                                        Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
                                        Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
                                        int index = 0, int size = 10, bool enableTracking = true);

        Task<ProductDto> AddAsync(ProductCreateDto createDto);
        Task<ProductDto> UpdateAsync(int id, ProductUpdateDto updateDto);
        Task<ProductDto> DeleteAsync(int id);
    }
}
