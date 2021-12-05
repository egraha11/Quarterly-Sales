using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarterly_Sales.Models
{
    public class RouteDictionary : Dictionary<string, string>
    {

        public int PageNumber
        {
            get => int.Parse(Get("PageNumber"));
            set => this["PageNumber"] = value.ToString();
        }

        public int PageSize
        {
            get => int.Parse(Get("PageSize"));
            set => this["PageSize"] = value.ToString();
        }

        public string SortField
        {
            get => Get("SortField");
            set => this["SortField"] = value.ToString();
        }

        public string SortDirection
        {
            get => Get("SortDirection");
            set => this["SortDirection"] = value.ToString();
        }

        public string Employee
        {
            get => Get("Employee");
            set => this["Employee"] = value.ToString();
        }

        public string Year
        {
            get => Get("Year");
            set => this["Year"] = value.ToString();
        }

        public string Quarter
        {
            get => Get("Quarter");
            set => this["Quarter"] = value.ToString();
        }


        public string Get(string key) => key.Contains(key) ? this[key] : null; 


        public void SetSortFieldAndDirection(string fieldName, RouteDictionary currentRoute)
        {

            this["SortField"] = fieldName;

            if(fieldName == currentRoute.SortField && currentRoute.SortDirection == "asc")
            {
                this["SortDirection"] = "desc";
            }
            else
            {
                this["SortDirection"] = "asc";
            }
        }

        public RouteDictionary Clone()
        {

            var clone = new RouteDictionary();

            foreach(var key in Keys)
            {
                clone.Add(key, this[key]);
            }

            return clone;
        }

        public void ClearFilters()
        {
            Employee = GridDTO.Default;
            Year = GridDTO.Default;
            Quarter = GridDTO.Default;
        }
    }
}
