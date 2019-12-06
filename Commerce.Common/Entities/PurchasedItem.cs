using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Common.Entities
{
    public class PurchasedItem
    {
        public int Sku { get; set; }
        public double PurchasePrice { get; set; }
        public DateTime PurchasedOn { get; set; }
    }
}
