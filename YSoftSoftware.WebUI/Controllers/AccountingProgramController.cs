using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.WebUI.Controllers
{
    public class AccountingProgramController : Controller
    {
        private IAccountingProgramRepository repository;

        public AccountingProgramController(IAccountingProgramRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            return View(repository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AccountingProgram entity)
        {
            if (ModelState.IsValid)
            {
                repository.Add(entity);
                repository.Save();

                return RedirectToAction("Index");
            }

            return View(entity);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(repository.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(AccountingProgram entity)
        {
            if (ModelState.IsValid)
            {
                repository.Update(entity);
                repository.Save();

                return RedirectToAction("Index");
            }

            return View(entity);
        }

        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}
