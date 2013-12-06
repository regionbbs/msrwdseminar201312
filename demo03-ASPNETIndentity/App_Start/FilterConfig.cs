using System.Web;
using System.Web.Mvc;

namespace demo03_ASPNETIndentity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
