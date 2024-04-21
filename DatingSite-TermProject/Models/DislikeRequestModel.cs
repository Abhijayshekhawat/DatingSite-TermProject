using System.Data;
using Utilities;
using System.Data.SqlClient;

namespace DatingSite_TermProject.Models
{
    public class DislikeRequestModel
    {

        private int likeeId;
        private string likerusername;

        public DislikeRequestModel()
        {

        }

        public DislikeRequestModel(int likeeId, string likerusername)
        {
            this.likeeId = likeeId;
            this.likerusername = likerusername;
        }


        public int LikeeId
        {
            get { return likeeId; }
            set { likeeId = value; }



        }

        public string LikerUsername { get { return likerusername; } set { likerusername = value; } }

        public int DeleteLikeSuccessfully(string likerusername, int likeeid)
        {
            int result = 0;

            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_DeleteLike";

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
