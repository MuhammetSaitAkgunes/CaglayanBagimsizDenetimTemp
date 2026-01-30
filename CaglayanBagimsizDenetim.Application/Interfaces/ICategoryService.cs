using CaglayanBagimsizDenetim.Application.DTOs.CategoryDto;
using CaglayanBagimsizDenetim.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResult<CategoryDto>> GetCategoryByIdAsync(Guid id);
        Task<ServiceResult<Guid>> CreateCategoryAsync(CreateCategoryDto request);
        Task<ServiceResult<Guid>> UpdateCategoryAsync(UpdateCategoryDto request);
        Task<ServiceResult<bool>> DeleteCategoryAsync(Guid id);
    }
}
