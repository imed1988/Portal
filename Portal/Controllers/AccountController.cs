using Portal.Models.Account;
using Portal.Models.General;
using Portal.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                bool isAuthenticated = WebSecurity.Login(loginModel.UserName, loginModel.Password, loginModel.RememberMe);

                if (isAuthenticated)
                {
                    string returnUrl = Request.QueryString["ReturnUrl"];

                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return Redirect(Url.Content(returnUrl));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password are invalid");
                }
            }


            return View();
        }

        public ActionResult SignOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize(Roles="Administrator, Manager")]
        public ActionResult Register()
        {
            GetRolesForCurrentUser();

            return View();
        }

        private void GetRolesForCurrentUser()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = (int)Role.Administrator;
            else
                ViewBag.RoleId = (int)Role.NoRole;
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Register(RegisterModel registerModel)
        {
            GetRolesForCurrentUser();

            if(ModelState.IsValid)
            {
                bool isUserExists = WebSecurity.UserExists(registerModel.Username);

                if(isUserExists)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                }
                else
                {
                    WebSecurity.CreateUserAndAccount(registerModel.Username, registerModel.Password, new
                    {
                        FullName = registerModel.FullName,
                        Mail = registerModel.Mail
                    
                    });

                    Roles.AddUserToRole(registerModel.Username, registerModel.Role);

                    return RedirectToAction("Index", "Dashboard");

                }

            }


            return View();
        }

        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel cpm)
        {

             if(ModelState.IsValid)
            {
                bool isPasswordChanged=WebSecurity.ChangePassword(WebSecurity.CurrentUserName, cpm.OldPassword, cpm.NewPassword);
                if(isPasswordChanged)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is not correct");

                }


            }

            return View();
        }

        [HttpGet, Authorize]
        public ActionResult UserProfile()
        {
            UserProfileModel userProfileModel = AccountViewModel.GetUserProfileData(
                WebSecurity.CurrentUserId);
            return View(userProfileModel);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserProfileModel upm)
        {
           if(ModelState.IsValid)
            {
                AccountViewModel.UpdateUserProfile(upm);
                ViewBag.Message = "Profile is saved successfully.";
            }
          
            return View();
        }
    }
}