using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ETT_Backend.Configuration.Security
{
  [AttributeUsage(AttributeTargets.Method)]
  public class HMACAuthenticationAttribute : ActionFilterAttribute
  {
    private string APPId = "4d53bce03ec34c0a911182d4c228ee6c";
    private string APIKey = "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=";
    private static Dictionary<string, string> allowedApps = new Dictionary<string, string>();
    public HMACAuthenticationAttribute() : base()
    {
      if (allowedApps.Count == 0)
      {
        allowedApps.Add(APPId, APIKey);
      }
    }    

    public override void OnActionExecuting(HttpActionContext context)
    {
      if (context.Request.Headers.TryGetValues("Authorization", out var rawAuthHeader))
      {
        var authorizationHeaderArray = GetAuthorizationHeaderValues(rawAuthHeader.ElementAt(0));
        if (authorizationHeaderArray != null && authorizationHeaderArray.Length == 4)
        {
          var APPId = authorizationHeaderArray[0];
          var incomingBase64Signature = authorizationHeaderArray[1];
          var nonce = authorizationHeaderArray[2];
          var requestTimeStamp = authorizationHeaderArray[3];

          var isValid = isValidRequest(context.Request, APPId, incomingBase64Signature, nonce, requestTimeStamp);
          if (isValid)
          {
            return;
          }
          else
          {
            // context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            throw new UnauthorizedAccessException();
          }
        }
        else
        {
          //context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
          throw new UnauthorizedAccessException();
        }
      }
      else
      {
        //context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        throw new UnauthorizedAccessException();
      }

      // return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }

    private string[] GetAuthorizationHeaderValues(string rawAuthzHeader)
    {
      var credArray = rawAuthzHeader.Split(':');
      if (credArray.Length == 4)
      {
        return credArray;
      }
      else
      {
        return null;
      }
    }
    
    private bool isValidRequest(HttpRequestMessage req, string APPId, string incomingBase64Signature, string nonce, string requestTimeStamp)
    {
      string requestContentBase64String = "";
      string requestUri = HttpUtility.UrlEncode(req.RequestUri.ToString().ToLower());
      string requestHttpMethod = req.Method.Method;

      if (!allowedApps.ContainsKey(APPId))
      {
        return false;
      }

      var sharedKey = allowedApps[APPId];

      byte[] hash = ComputeHash(req.Content);

      if (hash != null)
      {
        requestContentBase64String = Convert.ToBase64String(hash);
      }

      string data = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

      var secretKeyBytes = Convert.FromBase64String(sharedKey);

      byte[] signature = Encoding.UTF8.GetBytes(data);
      using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
      {
        byte[] signatureBytes = hmac.ComputeHash(signature);
        return (incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal));
      }

    }

    private static byte[] ComputeHash(HttpContent httpContent)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] hash = null;
        var content = httpContent.ReadAsByteArrayAsync().Result;
        if (content.Length != 0)
        {
          hash = md5.ComputeHash(content);
        }
        return hash;
      }
    }

    public override bool AllowMultiple
    {
      get { return false; }
    }
  }
}