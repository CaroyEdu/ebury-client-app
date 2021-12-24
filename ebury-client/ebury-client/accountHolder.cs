using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebury_client
{
    class accountHolder
    {
        public string activeCustomer;
        public string accountType;
        public name name;
        public List<addresses> addresses;

        public accountHolder(string activeCustomer, string accountType, string firstName, string lastName)
        {
            this.activeCustomer = activeCustomer;
            this.accountType = accountType;
            this.name = new name(firstName, lastName);
            this.addresses = new List<addresses>();
        }
    }
}
