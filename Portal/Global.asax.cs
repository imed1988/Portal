using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitializeAuthenticationProcess();
        }

        private void InitializeAuthenticationProcess()
        {
           if(!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("dbx", "Users", "UserId", "Username", true);

                //WebSecurity.CreateUserAndAccount("administrateur", "admin33");
                //Roles.CreateRole("Administrator");
                //Roles.CreateRole("Manager");
                //Roles.CreateRole("User");

                //Roles.AddUserToRole("administrateur", "Administrator");

            }
        }
    }
}
