using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Models.Validate
{
    public class ValidateSession: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session.GetString("usuario") == null)
            {
                filterContext.Result = new RedirectResult("~/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
