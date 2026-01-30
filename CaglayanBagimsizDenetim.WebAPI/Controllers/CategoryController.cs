using CaglayanBagimsizDenetim.Application.DTOs.CategoryDto;
using CaglayanBagimsizDenetim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CaglayanBagimsizDenetim.WebAPI.Controllers
{
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return CreateActionResult(await _categoryService.GetCategoryByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto request)
        {
            return CreateActionResult(await _categoryService.CreateCategoryAsync(request));
        }

        [HttpPut
            ]
        public async Task<IActionResult> Update(UpdateCategoryDto request)
        {
            return CreateActionResult(await _categoryService.UpdateCategoryAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CreateActionResult(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
