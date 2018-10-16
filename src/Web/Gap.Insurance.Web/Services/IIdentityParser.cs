using System.Security.Principal;

namespace Gap.Insurance.Web.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}