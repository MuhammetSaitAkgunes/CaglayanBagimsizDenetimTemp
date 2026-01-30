using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Application.DTOs.ArticleDto
{
    public class CreateArticleDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Slug { get; set; }
        public required string CoverImageUrl { get; set; }
        public required Guid CategoryId { get; set; }
    }
}
