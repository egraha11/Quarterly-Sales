using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quarterly_Sales.Models;
using Microsoft.AspNetCore.Mvc;

namespace Quarterly_Sales.Controllers
{
    public class EmployeeController : Controller
    {

        private SalesContext context { get; set; }
        public IActionResult Index()
        {


            return View();
        }

        public EmployeeController(SalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();

            return View("Add", new Employee());
        }




        [HttpPost]
        public IActionResult Add(Employee employee)
        {

            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Add", employee);
            }  
        }
    }
}
