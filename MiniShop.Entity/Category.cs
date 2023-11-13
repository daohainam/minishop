using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity
{
    public class Category: BaseEntity
    {
        public required string Name { get; set; }
    }
}
