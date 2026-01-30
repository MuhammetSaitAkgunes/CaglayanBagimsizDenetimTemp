using CaglayanBagimsizDenetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Domain.Entities
{
    public sealed class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string CoverImageUrl { get; set; }
        public int ViewCount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        private Article() { }

        public Article(string title, string content, string slug, string coverImageUrl, Guid categoryId)
        {
            Guard.AgainstNullOrEmpty(title, nameof(title), "Article title cannot be empty.");
            Guard.AgainstNullOrEmpty(content, nameof(content), "Article content cannot be empty.");
            Guard.AgainstNullOrEmpty(slug, nameof(slug), "Article slug cannot be empty.");
            Guard.AgainstNullOrEmpty(coverImageUrl, nameof(coverImageUrl), "Article cover image URL cannot be empty.");
            Guard.AgainstNullOrWhiteSpace(categoryId.ToString(), nameof(categoryId), "Article category ID cannot be empty.");
            Title = title;
            Content = content;
            Slug = slug;
            CoverImageUrl = coverImageUrl;
            CategoryId = categoryId;
        }

        private void MarkAsModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateTitle(string newTitle)
        {
            Guard.AgainstNullOrEmpty(newTitle, nameof(newTitle), "New article title cannot be empty.");
            Title = newTitle;
            MarkAsModified();
        }
        public void UpdateContent(string newContent)
        {
            Guard.AgainstNullOrEmpty(newContent, nameof(newContent), "New article content cannot be empty.");
            Content = newContent;
            MarkAsModified();
        }
        public void UpdateSlug(string newSlug)
        {
            Guard.AgainstNullOrEmpty(newSlug, nameof(newSlug), "New article slug cannot be empty.");
            Slug = newSlug;
            MarkAsModified();
        }
        public void UpdateCoverImageUrl(string newCoverImageUrl)
        {
            Guard.AgainstNullOrEmpty(newCoverImageUrl, nameof(newCoverImageUrl), "New article cover image URL cannot be empty.");
            CoverImageUrl = newCoverImageUrl;
            MarkAsModified();
        }
        public void UpdateCategoryId(Guid newCategoryId)
        {
            Guard.AgainstNullOrWhiteSpace(newCategoryId.ToString(), nameof(newCategoryId), "New article category ID cannot be empty.");
            CategoryId = newCategoryId;
            MarkAsModified();
        }

    }
}
