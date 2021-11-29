using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarterly_Sales.Models
{
    public class SaleViewModel
    {

        public IEnumerable<Sale> Sales { get; set; }
        public RouteDictionary CurrentRoute { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<Employee> Employees {get;set;}

    }
}
