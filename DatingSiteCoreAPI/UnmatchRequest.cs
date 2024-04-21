using System.Data.SqlClient;
using System.Data;
using Utilities;
using Microsoft.Extensions.FileSystemGlobbing;

namespace DatingSiteCoreAPI
{
    public class UnmatchRequest
    {
        private int matcherID;
        private string matcherUsername;
        public UnmatchRequest(){}
        public UnmatchRequest(int mID, string mUN)
        {
            this.matcherID = mID;
            this.matcherUsername = mUN;
        }
        public int MatcherID
        {
            get { return matcherID; }
            set { matcherID = value; }
        }
        public string MatcherUsername
        {
            get { return matcherUsername; }
            set { matcherUsername = value; }
        }
        public int UnMatch(string likerusername, int likeeid)
        {
            int result = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            //objCommand.CommandText = "TP_DeleteLike";

            SqlParameter inputParameter2 = new SqlParameter("@LikerUserName", likerusername);
            objCommand.Parameters.Add(inputParameter2);
            SqlParameter inputParameter3 = new SqlParameter("@LikeeID", likeeid);
            objCommand.Parameters.Add(inputParameter3);
            SqlParameter returnParameter = new SqlParameter("@Result", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(returnParameter);

            result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result;
        }
    }
}
