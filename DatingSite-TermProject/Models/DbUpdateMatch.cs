using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSite_TermProject.Models
{
    public class DbUpdateMatch
    {
        private DataSet GetMutualLikes()
        {
            DBConnect DB = new DBConnect();
            DataSet DS;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetMutualLikes";
            DS = DB.GetDataSet(Cmd);
            return DS;
        }
        public void UpdateMatch()
        {
            DataSet dsMutualLikes = GetMutualLikes();
            if (dsMutualLikes != null && dsMutualLikes.Tables.Count > 0)
            {
                foreach (DataRow row in dsMutualLikes.Tables[0].Rows)
                {
                    int user1ID = (int)row["User1ID"];
                    int user2ID = (int)row["User2ID"];
                    DateTime user1LikesUser2Time = (DateTime)row["User1LikesUser2Time"];
                    DateTime user2LikesUser1Time = (DateTime)row["User2LikesUser1Time"];
                    DateTime matchTime = user1LikesUser2Time > user2LikesUser1Time ? user1LikesUser2Time : user2LikesUser1Time;
                    if (!IsMatchExist(user1ID, user2ID))
                    {
                        InsertMatch(user1ID, user2ID, matchTime);
                    }
                }
            }

        }
        private bool IsMatchExist(int user1ID, int user2ID)
        {
            DBConnect DB = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CheckIfMatchExists";
            cmd.Parameters.AddWithValue("@MatcherID1", user1ID);
            cmd.Parameters.AddWithValue("@MatcherID2", user2ID);

            SqlParameter outputParam = new SqlParameter("@IsExist", SqlDbType.Bit);
            outputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);

            DB.GetDataSetUsingCmdObj(cmd);
            bool isExist = (bool)cmd.Parameters["@IsExist"].Value;
            return isExist;
        }
        private void InsertMatch(int matcherID1, int matcherID2, DateTime matchTime)
        {
            DBConnect DB = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_InsertMatch";
            cmd.Parameters.AddWithValue("@MatcherID1", matcherID1);
            cmd.Parameters.AddWithValue("@MatcherID2", matcherID2);
            cmd.Parameters.AddWithValue("@MatchTime", matchTime);

            DB.DoUpdateUsingCmdObj(cmd);
            //ViewBag.ErrorMessage = "You have a new match!";
        }
    }


}

