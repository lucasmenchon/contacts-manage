﻿using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //se usuario esta logado, redireciona para home
            if (_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult UserLogout()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult RedefinePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendLinkRedefinePw(RedefinePasswordModel redefinePassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _usuarioRepositorio.BuscarEmailLogin(redefinePassword.Email, redefinePassword.Login);
                    if (user != null)
                    {
                        string newPassword = user.MakeNewPassword();

                        string messageEmail = $"Sua nova senha é: {newPassword}";
                        bool sentEmail = _email.SendEmail(user.Email, "System Contacts - New Password", messageEmail);

                        if (sentEmail)
                        {
                            _usuarioRepositorio.Atualizar(user);
                            TempData["MsgSuccess"] = $"Enviamos para seu email cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MsgError"] = $"Não foi possível enviar o e-mail. Por favor, tente novamente.";
                        }
                        
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MsgError"] = $"Não foi possível redefinir sua senha. Por favor verifique os dados informados.";
                    return View("RedefinePassword");
                }
                return View("RedefinePassword");
            }
            catch (Exception)
            {

                TempData["MsgError"] = $"Ops!! Não foi possível redefinir sua senha, tente novamente ou entre em contato com o suporte.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _usuarioRepositorio.BuscarPorLogin(loginUser.Login);
                    if (user != null)
                    {
                        if (user.SenhaValida(loginUser.Senha))
                        {
                            _sessao.CriarSessaoUsuario(user);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                    }
                    TempData["MsgError"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }
                return View("Index");
            }
            catch (Exception error)
            {

                TempData["MsgError"] = $"Ops!! Não foi possível realizar seu login, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
