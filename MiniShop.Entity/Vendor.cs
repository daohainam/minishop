using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity
{
    public class Vendor: BaseEntity
    {
        public required string Name { get; set; }
    }
}
