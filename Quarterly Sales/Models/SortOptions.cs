using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarterly_Sales.Models
{
    public class SortOptions : QueryOptions<Sale>
    {

        public void SortFilters(GridBuilder builder)
        {

            if (builder.isFilterbyEmployee)
            {
                Where = s => s.Employee.LastName == builder.routes.Employee;
            }
            if (builder.isFilterbyYear)
            {
                Where = s => s.Year.ToString() == builder.routes.Year;
            }
            if (builder.isFilterbyQuarter)
            {
                Where = s => s.Quarter.ToString() == builder.routes.Quarter;
            }
        }


    }
}
