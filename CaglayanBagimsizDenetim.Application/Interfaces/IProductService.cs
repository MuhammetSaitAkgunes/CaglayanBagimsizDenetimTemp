using CaglayanBagimsizDenetim.Application.DTOs;
using CaglayanBagimsizDenetim.Application.Parameters;
using CaglayanBagimsizDenetim.Application.Wrappers;

namespace CaglayanBagimsizDenetim.Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResult<PagedResult<ProductDto>>> GetProductsPagedAsync(PaginationParameters parameters);
        Task<ServiceResult<ProductDto>> GetProductByIdAsync(Guid id);
        Task<ServiceResult<Guid>> CreateProductAsync(CreateProductDto request);
    }
}