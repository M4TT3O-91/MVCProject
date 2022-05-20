using CRUDImpiegati.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using CRUDImpiegati.Repository;

namespace CRUDImpiegati.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBManager dBManager; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            dBManager = new DBManager();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(dBManager.GetAllImpiegati());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var impiegato = dBManager.GetAllImpiegati().Where(x => x.ID == id).FirstOrDefault();
            return View(impiegato);
        }

        [HttpPost]
        public IActionResult Edit(ImpiegatoViewModel impiegato)
        {
            var res = dBManager.GetAllImpiegati().Where(x => x.ID == impiegato.ID).FirstOrDefault();
            if (res != null)
                dBManager.EditImpiegato(impiegato);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}