using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebury_client
{
    public class addresses
    {
        public string city;
        public string street;
        public string number;
        public string postalCode;
        public string country;

        public addresses (string city, string street, string number, string postalCode, string country){
            this.city = city;
            this.street = street;
            this.number = number;
            this.postalCode = postalCode;
            this.country = country;
        }
    }
}
