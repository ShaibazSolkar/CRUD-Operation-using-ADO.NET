using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudOperationAdo.Models;
using PagedList.Mvc;
using PagedList;

namespace CrudOperationAdo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EmployeeContext db = new EmployeeContext();
        public ActionResult Index(int ? i)
        {
            return View(db.GetEmployees().ToList().ToPagedList(i ?? 1,11));
        }
        public ActionResult Create()
        {
            return View();
        }
[HttpPost]
        public ActionResult Create(EmployeeModel emp)
        {
            int i = db.SaveEmployee(emp);
            if(i>0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int ? Id)
        {
            EmployeeModel emp = db.GetEmployeesById(Id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel emp)
        {
            int i = db.SaveEmployee(emp);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int? Id)
        {
            EmployeeModel emp = db.GetEmployeesById(Id);
            return View(emp);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ? id)
        {
            int i = db.DeleteEmployee(id);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}