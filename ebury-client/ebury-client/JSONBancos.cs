using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebury_client
{
    class JSONBancos
    {
        public string productNumber, productType, status, startDate, endDate;
        public List<accountHolder> accountHolder;

        public JSONBancos(string productNumber, string productType, string status, string startDate, string endDate)
        {
            this.productNumber = productNumber;
            this.status = status;
            this.productType = productType;
            this.startDate = startDate;
            this.endDate = endDate;
            accountHolder = new List<accountHolder>();
        }
    }
}