using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Quarterly_Sales.Models
{
    public class GridBuilder
    {

        public RouteDictionary routes { get; set; }
        public ISession session { get; set; }

        private const string key = "CurrentRoute";


        public GridBuilder(ISession sess)
        {
            session = sess;


            routes = session.GetObject<RouteDictionary>(key) ?? new RouteDictionary();
        }

        public GridBuilder(GridDTO vals, ISession sess)
        {
            session = sess;

            routes = new RouteDictionary();

            routes.PageNumber = vals.PageNumber;
            routes.PageSize = vals.PageSize;
            routes.SortField = vals.SortField;
            routes.SortDirection = vals.SortDirection;
            routes.Employee = vals.Employee;
            routes.Year = vals.Year;
            routes.Quarter = vals.Quarter;

            session.SetObject<RouteDictionary>(routes, key);
        }

        public void LoadFilterSegments(string[] filter)
        {
            routes.Employee = FilterPrefix.Employee + filter[0];
            routes.Year = FilterPrefix.Year + filter[1];
            routes.Quarter = FilterPrefix.Quarter + filter[2];


            session.SetObject<RouteDictionary>(routes, key);
        }


        public int GetTotalPages(int count)
        {
            int size = routes.PageSize;
            return (count + size - 1) / size;
        }

        public void Save() => session.SetObject<RouteDictionary>(routes, key);


        public bool isFilterbyEmployee => routes.Employee != GridDTO.Default;
        public bool isFilterbyYear => routes.Year != GridDTO.Default;
        public bool isFilterbyQuarter => routes.Quarter != GridDTO.Default;

    }
}
