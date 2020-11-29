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
        public PaginationModel PaginationModel { get; set; }
        public string? id { get; set; }
        public string? username { get; set; }

        public CategoryFilterModel(int? categoryId = null, bool? isIntern = null, string? searchedWords = null)
        {
            this.CategoryId = categoryId;
            this.IsIntern = isIntern;
            this.SearchedWords = searchedWords;
        }
    }
}
