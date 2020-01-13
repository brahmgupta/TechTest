using System.Collections.Generic;

namespace CleanArchitecture.Core.Domain
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
