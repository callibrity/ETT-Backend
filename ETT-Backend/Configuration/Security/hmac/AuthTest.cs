using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ETT_Backend.Configuration.Security.hmac
{
  [AttributeUsage(AttributeTargets.Method)]
  public class AuthTest : AuthorizationFilterAttribute
  {
    public override bool AllowMultiple => false;

    public override void OnAuthorization(HttpActionContext actionContext)
    {
      base.OnAuthorization(actionContext);
    }
  }
}
