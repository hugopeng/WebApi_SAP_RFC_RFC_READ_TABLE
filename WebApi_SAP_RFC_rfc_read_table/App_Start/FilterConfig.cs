using System.Web;
using System.Web.Mvc;

namespace WebApi_SAP_RFC_rfc_read_table
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
