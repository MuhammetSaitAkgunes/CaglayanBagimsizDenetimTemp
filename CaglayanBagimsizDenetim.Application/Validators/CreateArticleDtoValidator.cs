using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaglayanBagimsizDenetim.Application.DTOs.ArticleDto;
using FluentValidation;

namespace CaglayanBagimsizDenetim.Application.Validators
{
    public class CreateArticleDtoValidator : AbstractValidator<CreateArticleDto>
    {
        public CreateArticleDtoValidator()
        {
            RuleFor(a => a.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(a => a.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(a => a.CategoryId).NotEmpty().WithMessage("CategoryId is required.");
            RuleFor(a => a.CoverImageUrl).NotEmpty().WithMessage("CoverImageUrl is required.");
        }
    }
}
