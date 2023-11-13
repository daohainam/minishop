using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.UseCase
{
    public class CatalogPage<T>
    {
        public required IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}
