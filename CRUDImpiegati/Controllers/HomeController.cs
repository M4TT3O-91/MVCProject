using CRUDImpiegati.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace CRUDImpiegati.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string connectionString = @"Server = ACADEMYNETUD07\SQLEXPRESS; Database = Impiegato; Trusted_Connection = True; ";
            List<ImpiegatoViewModel> impiegatiList = new List<ImpiegatoViewModel>();
            string sql = @"Select * from Impiegato";

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var impiegato = new ImpiegatoViewModel
                {
                    ID = Convert.ToInt32(reader["ID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Città = reader["Citta"].ToString(),
                    Salario = decimal.Parse(reader["Salario"].ToString()),
                };
                impiegatiList.Add(impiegato);
            }
            return View(impiegatiList);
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