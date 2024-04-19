using System.Data;
using Utilities;
using System.Data.SqlClient;

namespace DatingSite_TermProject.Models
{
    public class LikeRequestModel
    {
        private int likeeId;
        private string likerusername;

        public LikeRequestModel() { }



        public LikeRequestModel(int likeeId, string likerusername)
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

            result = objDB.DoUpdateUsingCmdObj(objCommand);

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
