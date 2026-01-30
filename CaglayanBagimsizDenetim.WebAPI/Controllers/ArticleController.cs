using CaglayanBagimsizDenetim.Application.DTOs.ArticleDto;
using CaglayanBagimsizDenetim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CaglayanBagimsizDenetim.WebAPI.Controllers
{
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class ArticleController : BaseApiController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _articleService.GetAllArticlesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return CreateActionResult(await _articleService.GetArticleByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleDto request)
        {
            return CreateActionResult(await _articleService.CreateArticleAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateArticleDto request)
        {
            return CreateActionResult(await _articleService.UpdateArticleAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CreateActionResult(await _articleService.DeleteArticleAsync(id));
        }
    }
}
