using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class ProductCategory
    {
        public int ID { get; }
        public string Name { get; set; }

        public ProductCategory(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
