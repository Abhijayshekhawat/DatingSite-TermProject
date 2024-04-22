using System.Data;
using Utilities;
using System.Data.SqlClient;

namespace DatingSite_TermProject.Models
{
    public class ViewManagement
    {

        public ViewManagement() { }


        public void PopulateFilters()
        {
            ////Populate States
            //{
            //    List<string> uniqueStates = new List<string>();
            //    DBConnect DB = new DBConnect();
            //    DataSet DS;
            //    SqlCommand Cmd = new SqlCommand();
            //    Cmd.CommandType = CommandType.StoredProcedure;
            //    Cmd.CommandText = "TP_GetUniqueStates";
            //    DS = DB.GetDataSet(Cmd);
            //    foreach (DataRow row in DS.Tables[0].Rows)
            //    {
            //        string state = row["State"].ToString();
            //        uniqueStates.Add(state);
            //    }
            //    ViewBag.States = uniqueStates;
            //}
            ////Populate Interests
            //{
            //    DBConnect DB = new DBConnect();
            //    DataSet DS;
            //    SqlCommand Cmd = new SqlCommand();
            //    Cmd.CommandType = CommandType.StoredProcedure;
            //    Cmd.CommandText = "TP_GetUniqueInterests";
            //    DS = DB.GetDataSet(Cmd);

            //    List<string> uniqueInterests = new List<string>();
            //    foreach (DataRow row in DS.Tables[0].Rows)
            //    {
            //        string interestList = row["Interests"].ToString();
            //        string[] interests = interestList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (string interest in interests)
            //        {
            //            string trimmedInterest = interest.Trim();
            //            if (!uniqueInterests.Contains(trimmedInterest))
            //            {
            //                uniqueInterests.Add(trimmedInterest);
            //            }
            //        }
            //    }
            //    ViewBag.Interests = uniqueInterests;
            //}
            ////Populate Commitment Types
            //{
            //    List<string> uniqueCommitments = new List<string>();
            //    DBConnect DB = new DBConnect();
            //    DataSet DS;
            //    SqlCommand Cmd = new SqlCommand();
            //    Cmd.CommandType = CommandType.StoredProcedure;
            //    Cmd.CommandText = "TP_GetUniqueCommitmentTypes";
            //    DS = DB.GetDataSet(Cmd);
            //    foreach (DataRow row in DS.Tables[0].Rows)
            //    {
            //        string commitmentType = row["CommitmentType"].ToString();
            //        uniqueCommitments.Add(commitmentType);
            //    }
            //    ViewBag.Commitments = uniqueCommitments;
            //}



        }


    }
}
