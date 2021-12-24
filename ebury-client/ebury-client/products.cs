using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebury_client
{
    public class products
    {
        public string productNumber;
        public string status;

        public products(string productNumber, string status)
        {
            this.productNumber = productNumber;
            this.status = status;
        }
    }
}
