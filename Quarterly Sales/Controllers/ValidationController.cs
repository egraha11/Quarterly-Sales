using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quarterly_Sales.Models;

namespace Quarterly_Sales.Controllers
{
    public class ValidationController : Controller
    {
        SalesContext context;
        public ValidationController(SalesContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult CheckManager(int ManagerId, string FirstName, string LastName, DateTime DOB)
        {
            Employee manager = context.Employees.Find(ManagerId);

            if(manager.FirstName == FirstName && manager.LastName == LastName && manager.DOB == DOB)
            {
                return Json($"Employee cannot be a manager");
            }
            else
            {
                return Json(true);
            }
        }


        public JsonResult CheckEmployee(DateTime DOB, string FirstName, string LastName)
        {

            var employee = context.Employees.FirstOrDefault(e => e.DOB == DOB && 
            e.FirstName == FirstName && e.LastName == LastName);

            if (employee != null)
            {
                return Json($"Employee already exists in database");
            }
            else
            {
                return Json(true);
            }
        }

        public JsonResult CheckSale(int EmployeeId, int Quarter, DateTime Year)
        {
            IQueryable<Sale> sale = context.Sales.Where(s => s.EmployeeId == EmployeeId && s.Quarter == Quarter && s.Year == Year);

            if (sale != null)
            {
                return Json($"This sale already exists in database");
            }
            else
            {
                return Json(true);
            }
        }

    }
}
