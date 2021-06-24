using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;
using YSoftSoftware.WebUI.Models;

namespace YSoftSoftware.WebUI.Controllers
{
    public class PersonelController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PersonelController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.Personels.GetAll().Where(p=>p.Status));
        }

        public IActionResult Create()
        {
            ViewBag.Program = new SelectList(unitOfWork.Programs.GetAll(), "AccountingProgramId", "Name");
            ViewBag.Department = new SelectList(unitOfWork.Departments.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Personel personel)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Personels.Add(personel);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personel);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Program = new SelectList(unitOfWork.Programs.GetAll(), "AccountingProgramId", "Name");
            ViewBag.Department = new SelectList(unitOfWork.Departments.GetAll(), "DepartmentId", "DepartmentName");
            return View(unitOfWork.Personels.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Personel personel)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Personels.Update(personel);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personel);
        }

        public IActionResult Delete(int Id)
        {
            unitOfWork.Personels.Delete(Id);
            unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(unitOfWork.Personels.GetById(id));
        }

        public IActionResult Dismiss(int personelId)
        {
            unitOfWork.Personels.Dismiss(personelId);
            var personel = unitOfWork.Personels.GetById(personelId);

            unitOfWork.Compensations.Add(personelId, CompensationCalculate.Calculate(personel));
            unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetDismiss()
        {
            return View(unitOfWork.Compensations.GetAll());
        }
    }
}
