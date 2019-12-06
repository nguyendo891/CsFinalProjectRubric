using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Common.Entities
{
    public class Customer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public List<PurchasedItem> Purchases { get; set; }
    }
}
