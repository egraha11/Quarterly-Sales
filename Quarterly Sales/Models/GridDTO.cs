using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarterly_Sales.Models
{
    public class GridDTO
    {

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string SortField { get; set; } = "LastName";
        public string SortDirection { get; set; } = "asc";

        public string Employee { get; set; } = Default;

        public string Year { get; set; } = Default;

        public string Quarter { get; set; } = Default;

        public const string Default = "all";
    }
}
