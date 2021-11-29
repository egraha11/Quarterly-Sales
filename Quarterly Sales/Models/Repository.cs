using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quarterly_Sales.Models
{
    public class Repository <T> where T: class
    {

        public SalesContext context { get; set; }
        public DbSet<T> dbset { get; set; }


        public Repository(SalesContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        private int? count;
        public int Count => count ?? dbset.Count();

        public IEnumerable<T> BuildQuery(QueryOptions<T> options)
        {

            IQueryable<T> query = dbset;


            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasWhere)
            {
                foreach(var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }

                count = query.Count();
            }

            if (options.HasOrderBy)
            {
                if(options.OrderByDirection == "asc")
                {
                    query = query.OrderBy(options.OrderBy);
                }
                else
                {
                    query = query.OrderByDescending(options.OrderBy);
                }
            }

            if (options.HasPaging)
            {
                query = query.PageBy(options.PageNumber, options.PageSize);
            }

            return query.ToList();

        }

        public T Get(int id) => dbset.Find(id);
        public void Update(T entity) => dbset.Update(entity);
        public void Add(T entity) => dbset.Add(entity);
        public void Delete(T entity) => dbset.Remove(entity);
        public void Save() => context.SaveChanges();

    }
}

