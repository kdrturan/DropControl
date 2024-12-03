using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayControl
{
    public class Ebay_Control
    {
        public int Id { get; set; }
        public string AmazonLink { get; set; }
        public string ASINCode { get; set; }
        public decimal AmazonPrice { get; set; }
        public decimal EbayPrice { get; set; }
        public decimal AmazonLastPrice { get; set; }
        public bool InStock { get; set; }
        public DateTime LastControl { get; set; }

    }
}
