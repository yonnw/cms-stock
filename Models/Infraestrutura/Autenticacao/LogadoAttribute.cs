﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms_stock.Models.Infraestrutura.Autenticacao
{
    public class LogadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["adm_cms_dv"]))
            {
                filterContext.HttpContext.Response.Redirect("/login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
