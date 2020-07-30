using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ETT_Backend.Configuration.Security
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class HMACAuth: AuthorizeAttribute
  {
    private string APPId = "4d53bce03ec34c0a911182d4c228ee6c";
    private string APIKey = "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=";

    public HMACAuth() : base()
    {
      var tmp = 0;
    }

    public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {
      return Task.FromResult(new HttpResponseMessage());
    }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
      return true;
    }

    public override void OnAuthorization(HttpActionContext context)
    {
      // Take the colon delimited string from the Authorization header
      if (context.Request.Headers.TryGetValues("Authorization", out var rawAuthHeader))
      {
        // Split the authorization string by the colon delimiters
        var authorizationHeaderArray = GetAuthorizationHeaderValues(rawAuthHeader.ElementAt(0));
        if (authorizationHeaderArray != null && authorizationHeaderArray.Length == 4)
        {
          // Agreed upon position of the values. AppId:signature:nonce:timestamp
          var APPId = authorizationHeaderArray[0];
          var incomingBase64Signature = authorizationHeaderArray[1];
          var nonce = authorizationHeaderArray[2];
          var requestTimeStamp = authorizationHeaderArray[3];
          // Reconstruct the signature and compare to what was sent
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
    
    private bool isValidRequest(HttpRequestMessage req, string appId, string incomingBase64Signature, string nonce, string requestTimeStamp)
    {
      string requestContentBase64String = "";
      string requestUri = HttpUtility.UrlEncode(req.RequestUri.ToString().ToLower());
      string requestHttpMethod = req.Method.Method;

      if (!APPId.Equals(appId)) // Ensure the public app Id is equal
      {
        return false;
      }

      var sharedKey = APIKey; // Coming from file for now, will come from db or google secret

      byte[] hash = ComputeHash(req.Content);

      if (hash != null)
      {
        requestContentBase64String = Convert.ToBase64String(hash);
      }

      string data = String.Format("{0}{1}{2}{3}{4}{5}", appId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);
      byte[] signature = Encoding.UTF8.GetBytes(data);

      var secretKeyBytes = Convert.FromBase64String(sharedKey);
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