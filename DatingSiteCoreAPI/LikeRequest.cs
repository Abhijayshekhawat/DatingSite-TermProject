using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class LikeRequest
    {

        private int likeeId;
        private string likerusername;

        public LikeRequest() { }



        public LikeRequest(int likeeId, string likerusername)
        {
            likeeId = likeeId;
            this.likerusername = likerusername;
        
        }
        
        public int AddLikeSuccessfully(string likerusername, int likeeid)
        {
            int result = 0;

            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_AddNewLike";

            SqlParameter inputParameter2 = new SqlParameter("@LikerUserName", likerusername);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@LikeeID", likeeid);
            objCommand.Parameters.Add(inputParameter3);

            SqlParameter returnParameter = new SqlParameter("@Result", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(returnParameter);

            result  =  objDB.DoUpdateUsingCmdObj(objCommand);
            return result;  
        }

        public int LIkeeId
        {
            get { return likeeId; }
            set { likeeId = value; }
        }

        public string LikerUsername
        {
            get { return likerusername; }
            set { likerusername = value; }
        }



    }
}

