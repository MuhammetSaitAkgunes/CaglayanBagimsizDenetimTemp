using CaglayanBagimsizDenetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }

        private Category() { }

        public Category(string name, string description)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name), "Category name cannot be empty.");
            Guard.AgainstNullOrEmpty(description, nameof(description), "Category description cannot be empty.");
            Name = name;
            Description = description;
        }
        private void MarkAsModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateName(string newName)
        {
            Guard.AgainstNullOrEmpty(newName, nameof(newName), "New category name cannot be empty.");
            Name = newName;
            MarkAsModified();
        }
        public void UpdateDescription(string newDescription)
        {
            Guard.AgainstNullOrEmpty(newDescription, nameof(newDescription), "New category description cannot be empty.");
            Description = newDescription;
            MarkAsModified();
        }
    }
}
