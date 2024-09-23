using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_03.DAL.Models;
using MVC_03.PL.Helpers;
using MVC_03.PL.ViewModels;
using System;
using System.Threading.Tasks;

namespace MVC_03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

		public AccountController(UserManager<ApplicationUser> manager,SignInManager<ApplicationUser> signInManager)
        {
			Manager = manager;
			this.signInManager = signInManager;
		}

		public UserManager<ApplicationUser> Manager { get; }

		#region SignUp
		public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpViewModel viewModel )
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0],
                    Email = viewModel.Email,
                    IsAgree= viewModel.IsAgree,
                    FName= viewModel.FName,
                    LName= viewModel.LName,


				};
                
                var result=  await   Manager.CreateAsync(user,viewModel.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var Erorr in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Erorr.Description);
                }
                
            }
            return View(viewModel);
        
        }
        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
     //   [Authorize]
        public  async Task <IActionResult> SignIn(SignInViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await Manager.FindByEmailAsync(viewModel.Email);
                if(user is not null)
                {
                    bool flag= await  Manager.CheckPasswordAsync(user, viewModel.Password);
                    if(flag)
                    {
				var result=	await	signInManager.PasswordSignInAsync(user, viewModel.Password,viewModel.RememberMe,false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");    
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invaled Login");
                
            }
            return View(viewModel);
        }
        #endregion
        #region SignOut
        public async Task <IActionResult> SignOut()
        {
          await  signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion
        #region ForGetPassword
        public IActionResult ForGetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> ForGetPassword(forGetPassword viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await Manager.FindByEmailAsync(viewModel.Email);
                if(user is not null)
                {
                    var token=await Manager.GeneratePasswordResetTokenAsync(user);
                    var ResetPassword = Url.Action("ResetPassword", "Account",new {email=viewModel.Email,Token=token});
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Reciepints = viewModel.Email,
                        Body = ResetPassword
                    };
                    EmailSeetings.sendEmail(email);
                    return RedirectToAction(nameof(checkYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(viewModel);
        }
        public IActionResult checkYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                var token = TempData["token"]as string;
                var user = await Manager.FindByEmailAsync(email);
                var result=await Manager.ResetPasswordAsync(user, token,viewModel.confirmPassworded);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(viewModel);
        }
        #endregion
    }
}
