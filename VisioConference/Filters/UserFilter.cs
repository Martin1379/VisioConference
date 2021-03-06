using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisioConference.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        //Vérifier si le User est connecté ou pas, admin ou normal
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["userAdmin"] == null)
            {
                //redirection => page login
                filterContext.HttpContext.Response.Redirect("~/Login/Index");
            }

        }
    }
}