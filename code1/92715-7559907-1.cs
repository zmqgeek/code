    public class AuthorizedRedirect : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);
            return isAuthorized;
        }
    protect override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.RequestContext.RequestContext.RouteData.Values["controller"] = "error";
        filterContext.Result = new ViewResult { ViewName = "unauthorized" };
    }
    
