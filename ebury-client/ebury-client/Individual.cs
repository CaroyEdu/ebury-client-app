using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebury_client
{
    public class Individual
    {
        public string activeCustomer;
        public string dateOfBirth;
        public name name;
        public List<addresses> addresses;
        public List<products> products;

        public Individual(string firstName, string lastName, string dateOfBirth, string activeCustomer)
        {
            this.name = new name(firstName, lastName);
            this.dateOfBirth = dateOfBirth.Substring(0,10);
            this.activeCustomer = activeCustomer;
            addresses = new List<addresses>();
            products = new List<products>();
        }
    }
}
