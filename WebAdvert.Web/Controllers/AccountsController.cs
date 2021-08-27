using Amazon.Extensions.CognitoAuthentication;
using Amazon.AspNetCore.Identity.Cognito;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;  
        public AccountsController(SignInManager<CognitoUser> signInManager,
                                  UserManager<CognitoUser> userManager,
                                  CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }


        public  async Task<IActionResult> SignUp()
        {
            var model = new SignupModel(); 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupModel signup)
        {
            if(ModelState.IsValid)
            {
                var user = _pool.GetUser(signup.Email);
                if(user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "O usuário com esse email já existe");
                    return View(signup);
                }
                user.Attributes.Add(CognitoAttribute.Name.AttributeName, signup.Email);
                var createdUser = await _userManager.CreateAsync(user, signup.Password).ConfigureAwait(false);
                if(createdUser.Succeeded)
                {
                    RedirectToAction("Confirm");
                }
            }
            return View(signup);
        }

        public async Task<IActionResult> Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmModel confirm)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(confirm.Email);
                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "Não foi encontrado o usuário com este email");
                    return View(confirm);
                }
                var result = await _userManager.ConfirmEmailAsync(user, confirm.Code);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            return View(confirm);
        }

        [HttpGet]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login_Post(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false).ConfigureAwait(false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Usuário ou senha incorretos");
                }
            }
            return View("Login", model);
        }

    }
}
