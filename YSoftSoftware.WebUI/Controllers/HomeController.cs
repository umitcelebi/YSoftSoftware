using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSoftSoftware.Data.Abstract;

namespace YSoftSoftware.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IProjectRepository repository;

        public HomeController(IProjectRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View(repository.GetAll().Where(p=>p.Status));
        }
    }
}
