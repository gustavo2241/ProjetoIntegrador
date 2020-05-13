using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoIntegradorWeb.Models;

namespace ProjetoIntegradorWeb.Controllers
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
            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }


        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [Route("Home/CadastraProduto")]
        public string cadastraProduto(EstoqueModel prod)
        {
            EstoqueCadastraContext context = HttpContext.RequestServices.GetService(typeof(ProjetoIntegradorWeb.Models.EstoqueCadastraContext)) as EstoqueCadastraContext;
            context.cadastraProduto(prod);
            return "OK";
        }

        [HttpGet]
        [Route("Home/BuscaProduto")]
        public List<EstoqueModel> buscaProduto()
        {
            EstoqueBuscaContext context = HttpContext.RequestServices.GetService(typeof(ProjetoIntegradorWeb.Models.EstoqueBuscaContext)) as EstoqueBuscaContext;
            var result = context.buscaProduto();
            return result;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
