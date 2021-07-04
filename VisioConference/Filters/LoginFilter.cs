using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VisioConference.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        //Vérifier si le User est connecté ou pas, admin ou normal
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["userNormal"]== null)
            {
                //redirection => page login
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "Login"},
                        {"action", "AccessDenied"}
                    }
                );
            }

        }
    }
}