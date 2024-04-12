using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Utilities;

namespace DatingSiteWebService
{
    /// <summary>
    /// Summary description for Dashboard
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Dashboard : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]

        public List<Cards> ShowAllUsers(int ExcludedPrivateid)
        {
            List<Cards> list = new List<Cards>();
            Cards cards = new Cards();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUsersCards";

            SqlParameter inputParameter1 = new SqlParameter("@ExcludedUserId", ExcludedPrivateid);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                 cards = new Cards(dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["ProfilePhotoURL"].ToString(), dr["Description"].ToString(), dr["City"].ToString(), dr["State"].ToString()); 
            }

            list.Add(cards);


            return list;

        }
    }
}
