using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;
using System.Web.Http;
namespace ETT_Backend.Configuration.Security.basic
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class ApiAuth : AuthorizeAttribute, IAuthorizationFilter
  {

    public override bool AllowMultiple
    {
      get
      {
        return false;
      }
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      // Take the colon delimited string from the Authorization header
      if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var rawAuthHeader))
      {
        // Split the authorization string by the colon delimiters
        var userNamePassword = ExtractUsernamePassword(rawAuthHeader);
        if (userNamePassword != null && userNamePassword.Length == 2)
        {
          // Agreed upon position of the values. AppId:signature:nonce:timestamp
          var userName = userNamePassword[0];
          var password = userNamePassword[1];
          // Reconstruct the signature and compare to what was sent
          var isValid = isValidRequest(userName, password);
          if (isValid)
          {
            return;
          }
          else
          {
            context.Result = new UnauthorizedResult();
          }
        }
        else
        {
          context.Result = new UnauthorizedResult();
        }
      }
      else
      {
        context.Result = new UnauthorizedResult();
      }
    }

    private string[] ExtractUsernamePassword(string rawAuthzHeader)
    {
      string encodedUsernamePassword = rawAuthzHeader.Substring("Basic ".Length).Trim();
      Encoding encoding = Encoding.GetEncoding("iso-8859-1");
      string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
      var credArray = usernamePassword.Split(':');
      return credArray.Length == 2 ? credArray : null;
    }

    private bool isValidRequest(string userName, string password)
    {
      return (userName.Equals(Environment.GetEnvironmentVariable("API_USERNAME")) &&
              password.Equals(Environment.GetEnvironmentVariable("API_PASSWORD")));
    }
  }
}
