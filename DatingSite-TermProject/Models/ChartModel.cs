using System.Data;
using Utilities;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DatingSite_TermProject.Models;

namespace DatingSite_TermProject.Models
{
    public class ChartModel
    {
        // Fields for the TP_Chart_Age stored procedure
        private int age;
        private int userCountByAge;

        // Fields for the TP_Chart_Commitment stored procedure
        private string commitmentType;
        private int userCountByCommitmentType;

        // Fields for the TP_Chart_LikesTurnedMatches stored procedure
        private int totalLikes;
        private int likesTurnedMatches;

        // Fields for the TP_Chart_PlannedDates stored procedure
        private int totalDatesCount;
        private int plannedDatesCount;

        // Fields for the TP_Chart_State stored procedure
        private string state;
        private int userCountByState;

        // Fields for the TP_Chart_yourLikesRatio stored procedure
        private int totalLikesReceived;
        private int totalLikesOverall;

        // Fields for the TP_Charts_GetMatchAndDateStats stored procedure
        private int matchesTurnedDates;
        private int totalMatches;
        
        public ChartModel() { }
        
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public int UserCountByAge
        {
            get { return userCountByAge; }
            set { userCountByAge = value; }
        }
        public string CommitmentType
        {
            get { return commitmentType; }
            set { commitmentType = value; }
        }
        public int UserCountByCommitmentType
        {
            get { return userCountByCommitmentType; }
            set { userCountByCommitmentType = value; }
        }
        public int TotalLikes
        {
            get { return totalLikes; }
            set { totalLikes = value; }
        }
        public int LikesTurnedMatches
        {
            get { return likesTurnedMatches; }
            set { likesTurnedMatches = value; }
        }
        public int TotalDatesCount
        {
            get { return totalDatesCount; }
            set { totalDatesCount = value; }
        }
        public int PlannedDatesCount
        {
            get { return plannedDatesCount; }
            set { plannedDatesCount = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }   
        public int UserCountByState
        {
            get { return userCountByState; }
            set { userCountByState = value; }
        }
        public int TotalLikesReceived
        {
            get { return totalLikesReceived; }
            set { totalLikesReceived = value; }
        }
        public int TotalLikesOverall
        {
            get { return totalLikesOverall; }
            set { totalLikesOverall = value; }
        }
        public int MatchesTurnedDates
        {
            get { return matchesTurnedDates; }
            set { matchesTurnedDates = value; }
        }
        public int TotalMatches
        {
            get { return totalMatches; }
            set { totalMatches = value; }
        }

        public DataSet GetAgeChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_Age";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetCommitmentChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_Commitment";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetLikesTurnedMatchesChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_LikesTurnedMatches";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetPlannedDatesChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_PlannedDates";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetStateChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_State";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetYourLikesRatioChartData(int privateId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Chart_yourLikesRatio";
            objCommand.Parameters.AddWithValue("@UserId", privateId);
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public DataSet GetMatchAndDateStatsChartData()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_Charts_GetMatchAndDateStats";
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
    }
}
