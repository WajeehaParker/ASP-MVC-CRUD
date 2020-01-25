using System;
using System.Collections.Generic;
using System.Linq;  
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeData employeeDB = new EmployeeData();
        public IActionResult Index()
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            empList = employeeDB.getAllEmployee().ToList();
            return View(empList);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] EmployeeInfo emp)
        {
            if(ModelState.IsValid)
            {
                employeeDB.addEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeInfo emp = employeeDB.getEmployeeByID(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] EmployeeInfo info)
        {
            if(ModelState.IsValid)
            {
                employeeDB.updateEmployee(info);
                return RedirectToAction("Index");
            }
            return View(employeeDB);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            EmployeeInfo emp = employeeDB.getEmployeeByID(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        public ActionResult Delete(int id)
        {
            EmployeeInfo emp = employeeDB.getEmployeeByID(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int id)
        {
            employeeDB.deleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}