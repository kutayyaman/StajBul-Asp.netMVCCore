using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class CategoryFilterModel
    {
        public int? CategoryId { get; set; }
        public bool? IsIntern { get; set; }
        public string? SearchedWords { get; set; }

        public CategoryFilterModel(int? categoryId, bool? isIntern, string? searchedWords)
        {
            this.CategoryId = categoryId;
            this.IsIntern = isIntern;
            this.SearchedWords = searchedWords;
        }
    }
}
