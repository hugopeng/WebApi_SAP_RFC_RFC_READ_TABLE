using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace WebApi_SAP_RFC_rfc_read_table.Models
{
    public class SAPRFC_RFC_READ_TABLEModels
    {
        public string iQueryTable { get; set; }
        public string iQueryField { get; set; }
        public string oException { get; set; }

        public List<Dictionary<string, object>> Result { get; set; }
        List<Dictionary<string, object>> listResult = new List<Dictionary<string, object>>();

        public void Main()
        {
            SAPLogonCtrl.SAPLogonControlClass login = new SAPLogonCtrl.SAPLogonControlClass();

            login.ApplicationServer = ConfigurationManager.AppSettings["lg_ApplicationServer"];
            login.Client = ConfigurationManager.AppSettings["lg_Client"];
            login.Language = ConfigurationManager.AppSettings["lg_Language"];
            login.User = ConfigurationManager.AppSettings["lg_User"];
            login.Password = ConfigurationManager.AppSettings["lg_Password"];
            login.SystemNumber = Int32.Parse(ConfigurationManager.AppSettings["lg_SystemNumber"]);

            SAPLogonCtrl.Connection conn = (SAPLogonCtrl.Connection)login.NewConnection();

            try
            {

                if (conn.Logon(0, true))
                {
                    //lblLogonStatus.Text = "登入SAP成功";

                    SAPFunctionsOCX.SAPFunctionsClass func = new SAPFunctionsOCX.SAPFunctionsClass();
                    func.Connection = conn;

                    SAPFunctionsOCX.IFunction ifunc = (SAPFunctionsOCX.IFunction)func.Add("RFC_READ_TABLE");

                    SAPFunctionsOCX.IParameter iQUERY_TABLE = (SAPFunctionsOCX.IParameter)ifunc.get_Exports("QUERY_TABLE");
                    iQUERY_TABLE.Value = iQueryTable;
                    SAPFunctionsOCX.IParameter iDELIMITER = (SAPFunctionsOCX.IParameter)ifunc.get_Exports("DELIMITER");
                    iDELIMITER.Value = "|";

                    SAPTableFactoryCtrl.Tables tables = (SAPTableFactoryCtrl.Tables)ifunc.Tables;

                    SAPTableFactoryCtrl.Table tOPTIONS = (SAPTableFactoryCtrl.Table)tables.get_Item("OPTIONS");
                    tOPTIONS.AppendGridData(1, 1, 1, "");

                    SAPTableFactoryCtrl.Table tFIELDS = (SAPTableFactoryCtrl.Table)tables.get_Item("FIELDS");
                    if (iQueryField != null)
                    {
                        int StringStart = 0;
                        int iColumnIndex = 1;

                        int isDoExit = 0;
                        while (isDoExit == 0)
                        {
                            int StringEnd = iQueryField.ToString().IndexOf(",", StringStart);
                            if (StringEnd == -1)
                            {
                                StringEnd = iQueryField.ToString().Length;
                                isDoExit = 1;
                            }
                            int StringLength = StringEnd - StringStart;
                            tFIELDS.AppendGridData(1, iColumnIndex, 1, iQueryField.ToString().Substring(StringStart, StringLength));
                            StringStart = StringEnd + 1;
                            iColumnIndex = iColumnIndex + 1;

                        }

                    }

                    ifunc.Call();
                    oException = ifunc.Exception;

                    SAPTableFactoryCtrl.Table tDATA = (SAPTableFactoryCtrl.Table)tables.get_Item("DATA");

                    for (int m = 1; m <= tDATA.RowCount; m++)
                    {

                        int StringStart = 0;
                        Dictionary<string, object> listObject = new Dictionary<string, object>();
                        for (int n = 1; n <= tFIELDS.RowCount; n++)
                        {

                            int StringEnd = tDATA.get_Cell(m, 1).ToString().IndexOf("|", StringStart);
                            if (StringEnd == -1)
                            {
                                StringEnd = tDATA.get_Cell(m, 1).ToString().Length;
                            }
                            int StringLength = StringEnd - StringStart;

                            listObject.Add(tFIELDS.get_Cell(n, 1).ToString(), tDATA.get_Cell(m, 1).ToString().Substring(StringStart, StringLength));

                            StringStart = StringEnd + 1; 
                        }

                        listResult.Add(listObject);

                    }
                    conn.Logoff();
                }
                else
                {
                    throw new Exception("Logon Fail");
                }

                Result = listResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}