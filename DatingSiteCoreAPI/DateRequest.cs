using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class DateRequest
    {
        private string requesterusername;
        private int requesteeid;


        public DateRequest() { }




        public int SendRequestSuccessfully(string requesterusername, int requesteeid)
        {
            int result = 0;

            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_AddNewDateRequest";

            SqlParameter inputParameter2 = new SqlParameter("@LikerUserName", requesterusername);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@LikeeID", requesteeid);
            objCommand.Parameters.Add(inputParameter3);

            SqlParameter returnParameter = new SqlParameter("@Result", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(returnParameter);

            SqlParameter returnParameter2 = new SqlParameter("@Status", "pending");
            objCommand.Parameters.Add(returnParameter2);


            result = objDB.DoUpdateUsingCmdObj(objCommand);

            return result;
        }
        public DateRequest(string requestusername, int requesteeid)
        {

            this.requesterusername = requestusername;
            this.requesteeid = requesteeid; 

        }

        public string RequesterUsername { 
            
            get {  return requesterusername; }
            set { requesterusername = value; }  
        }

        public int RequesteeId { get { return requesteeid; } set { requesteeid = value; } }

    }
}
