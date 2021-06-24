using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ProjectController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            return View(unitOfWork.Projects.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Manager = new SelectList(unitOfWork.Personels.GetByDepartment("Manager").Where(p => p.Status), "PersonelId", "Name");
            ViewBag.Programmer = unitOfWork.Personels.GetByDepartment("Programmer").Where(p=>p.Status).ToList();
            ViewBag.Designer = unitOfWork.Personels.GetByDepartment("Designer").Where(p => p.Status).ToList();
            ViewBag.Analyst = unitOfWork.Personels.GetByDepartment("Analyst").Where(p => p.Status).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Project project, int[] Personel)
        {
            if (ModelState.IsValid)
            {
                project.Personels = unitOfWork.Personels.GetByIds(Personel);

                unitOfWork.Projects.Add(project);
                unitOfWork.Projects.Save();
                return RedirectToAction("Index");
            }

            return Create();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Manager = new SelectList(unitOfWork.Personels.GetByDepartment("Manager").Where(p => p.Status), "PersonelId", "Name");
            ViewBag.Programmer = unitOfWork.Personels.GetByDepartment("Programmer").Where(p => p.Status).ToList();
            ViewBag.Designer = unitOfWork.Personels.GetByDepartment("Designer").Where(p => p.Status).ToList();
            ViewBag.Analyst = unitOfWork.Personels.GetByDepartment("Analyst").Where(p => p.Status).ToList();
            return View(unitOfWork.Projects.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Project project, int[] Personel)
        {
            if (ModelState.IsValid)
            {
                project.Personels = unitOfWork.Personels.GetByIds(Personel);

                unitOfWork.Projects.Update(project);
                unitOfWork.Projects.Save();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        public IActionResult Details(int id)
        {
            ViewBag.Manager = unitOfWork.Personels.GetByDepartmentAndProject("Manager", id).ToList();
            ViewBag.Programmer = unitOfWork.Personels.GetByDepartmentAndProject("Programmer", id).ToList();
            ViewBag.Designer = unitOfWork.Personels.GetByDepartmentAndProject("Designer", id).ToList();
            ViewBag.Analyst = unitOfWork.Personels.GetByDepartmentAndProject("Analyst", id).ToList();
            return View(unitOfWork.Projects.GetById(id));
        }

        public IActionResult Search(string q)
        {
            return View("Index",unitOfWork.Projects.SearchProject(q));
        }

        public IActionResult Delete(int id)
        {
            unitOfWork.Projects.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
