using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaglayanBagimsizDenetim.Application.DTOs.ArticleDto;
using CaglayanBagimsizDenetim.Application.Wrappers;

namespace CaglayanBagimsizDenetim.Application.Interfaces
{
    public interface IArticleService
    {
        Task<ServiceResult<List<ArticleDto>>> GetAllArticlesAsync();
        Task<ServiceResult<ArticleDto>> GetArticleByIdAsync(Guid id);
        Task<ServiceResult<Guid>> CreateArticleAsync(CreateArticleDto request);
        Task<ServiceResult<Guid>> UpdateArticleAsync(UpdateArticleDto request);
        Task<ServiceResult<bool>> DeleteArticleAsync(Guid id);
    }
}
