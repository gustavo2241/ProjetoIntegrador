﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoIntegradorWeb.Models;

namespace ProjetoIntegradorWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly Autenticacao _autentica;

        public LoginController(Autenticacao autentica)
        {
            _autentica = autentica;
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([Bind] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string RegistroStatus = _autentica.RegistrarUsuario(usuario);
                if (RegistroStatus == "1")
                {
                    ModelState.Clear();
                    TempData["Sucesso"] = "Registro realizado com sucesso!";
                    return View();
                }
                else
                {
                    TempData["Falhou"] = "O Registro do usuário falhou.";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario([Bind] Usuario usuario)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                string LoginStatus = _autentica.ValidarLogin(usuario);

                if (LoginStatus != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Login)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    if (userIdentity.IsAuthenticated)
                        return RedirectToAction("UsuarioHome", "Usuario");
                    else
                    {
                        TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas " + User.Identity.Name;
                        return RedirectToAction("LoginUsuario", "Login");
                    }
                }
                else
                {
                    TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
