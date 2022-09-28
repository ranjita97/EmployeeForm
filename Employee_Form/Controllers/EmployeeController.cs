using Employee_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Form.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly EmployeeDbContext _dbcontext ;
       
        public EmployeeController(EmployeeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _dbcontext.Employees.Add(employee);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult EditEmployee(int? id)
        {
            if(id ==null)
            {
                return BadRequest();
            }
            var emp = _dbcontext.Employees.FirstOrDefault(e => e.Id == id);
            
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            _dbcontext.Attach(employee).State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult EmployeeDetails(int id)
        {
            var emp =_dbcontext.Employees.FirstOrDefault(e => e.Id == id);
            return View(emp);
        }

        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            var emp = _dbcontext.Employees.Find(id);
            _dbcontext.Employees.Remove(emp);
            _dbcontext.SaveChanges();
            return View();
        }
    }
}
