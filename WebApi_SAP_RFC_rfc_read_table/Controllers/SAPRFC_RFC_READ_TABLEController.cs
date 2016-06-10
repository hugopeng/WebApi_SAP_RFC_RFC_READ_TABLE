using System.Collections.Generic;
using System.Web.Http;
using WebApi_SAP_RFC_rfc_read_table.Models;

namespace WebApi_SAP_RFC_rfc_read_table.Controllers
{
    public class SAPRFC_RFC_READ_TABLEController : ApiController
    {
        public IEnumerable<SAPRFC_RFC_READ_TABLEModels> GetParameter(string QUERY_TABLE, string QUERY_FIELD)
        {
            Models.SAPRFC_RFC_READ_TABLEModels SAPRFC = new Models.SAPRFC_RFC_READ_TABLEModels();
            SAPRFC.iQueryTable = QUERY_TABLE;
            SAPRFC.iQueryField = QUERY_FIELD;

            System.Threading.Thread s = new System.Threading.Thread(new System.Threading.ThreadStart(SAPRFC.Main));
            s.SetApartmentState(System.Threading.ApartmentState.STA);
            s.Start();
            s.Join();

            yield return SAPRFC;

        }
    }
}
