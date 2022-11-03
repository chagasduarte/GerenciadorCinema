using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Cinema.Filters
{
    public class UsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaousuario = context.HttpContext.Session.GetString("sessaoUsuario");
            if (string.IsNullOrEmpty(sessaousuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login" }, { "action", "Index"} });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaousuario);
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuted(context);
        }
    }
}
