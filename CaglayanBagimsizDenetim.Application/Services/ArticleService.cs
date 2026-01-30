using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CaglayanBagimsizDenetim.Application.DTOs.ArticleDto;
using CaglayanBagimsizDenetim.Application.Interfaces;
using CaglayanBagimsizDenetim.Application.Interfaces.Repositories;
using CaglayanBagimsizDenetim.Application.Wrappers;
using CaglayanBagimsizDenetim.Domain.Entities;

namespace CaglayanBagimsizDenetim.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private const string CacheKeyPrefix = "articles";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ServiceResult<List<ArticleDto>>> GetAllArticlesAsync()
        {
            var cachedArticles = await _cache.GetAsync<List<ArticleDto>>($"{CacheKeyPrefix}:all");
            if (cachedArticles != null)
            {
                return ServiceResult<List<ArticleDto>>.Success(cachedArticles);
            }
            var articles = await _articleRepository.GetAllAsync();
            var articleDtos = _mapper.Map<List<ArticleDto>>(articles);
            await _cache.SetAsync($"{CacheKeyPrefix}:all", articleDtos, CacheExpiration);
            return ServiceResult<List<ArticleDto>>.Success(articleDtos);
        }

        public async Task<ServiceResult<ArticleDto>> GetArticleByIdAsync(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return ServiceResult<ArticleDto>.Failure("Article not found.", 404);
            }
            var articleDto = _mapper.Map<ArticleDto>(article);
            return ServiceResult<ArticleDto>.Success(articleDto);
        }

        public async Task<ServiceResult<Guid>> CreateArticleAsync(CreateArticleDto request)
        {
            var articleEntity = _mapper.Map<Article>(request);
            await _articleRepository.AddAsync(articleEntity);
            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:*");

            return ServiceResult<Guid>.Success(articleEntity.Id, 201);
        }

        public async Task<ServiceResult<Guid>> UpdateArticleAsync(UpdateArticleDto request)
        {
            var articleEntity = await _articleRepository.GetByIdAsync(request.Id);
            if (articleEntity == null)
            {
                return ServiceResult<Guid>.Failure("Article not found.", 404);
            }

            articleEntity.UpdateTitle(request.Title);
            articleEntity.UpdateContent(request.Content);
            articleEntity.UpdateSlug(request.Slug);
            articleEntity.UpdateCoverImageUrl(request.CoverImageUrl);
            articleEntity.UpdateCategoryId(request.CategoryId);

            await _articleRepository.UpdateAsync(articleEntity);
            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:*");
            return ServiceResult<Guid>.Success(articleEntity.Id);
        }

        public async Task<ServiceResult<bool>> DeleteArticleAsync(Guid id)
        {
            await _articleRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:{id}");

            return ServiceResult<bool>.Success(true);
        }
    }
}
