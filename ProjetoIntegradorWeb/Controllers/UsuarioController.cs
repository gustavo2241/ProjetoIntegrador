using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoIntegradorWeb.Models;

namespace ProjetoIntegradorWeb.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        public IActionResult UsuarioHome()
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
        [Route("Usuario/CadastraProduto")]
        public string cadastraProduto(EstoqueModel prod)
        {
            EstoqueCadastraContext context = HttpContext.RequestServices.GetService(typeof(ProjetoIntegradorWeb.Models.EstoqueCadastraContext)) as EstoqueCadastraContext;
            context.cadastraProduto(prod);
            return "OK";
        }

        [HttpGet]
        [Route("Usuario/BuscaProduto")]
        public List<EstoqueModel> buscaProduto()
        {
            EstoqueBuscaContext context = HttpContext.RequestServices.GetService(typeof(ProjetoIntegradorWeb.Models.EstoqueBuscaContext)) as EstoqueBuscaContext;
            var result = context.buscaProduto();
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoginUsuario", "Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
