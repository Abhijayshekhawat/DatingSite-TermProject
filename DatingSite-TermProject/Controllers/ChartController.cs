using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace DatingSite_TermProject.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult AgeData()
        {
            ChartModel chartModel = new ChartModel();

            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetAgeChartData();
            DataTable dt = ds.Tables[0];

            var ageDataList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                // Extract the age and user count from each row and add it to the list
                int age = (int)row["Age"];
                int userCount = (int)row["UserCount"];
                ageDataList.Add(new { Age = age, UserCount = userCount });
            }

            return Json(ageDataList);
        }
        public IActionResult CommitmentData()
        {
            ChartModel chartModel = new ChartModel();

            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetCommitmentChartData();
            DataTable dt = ds.Tables[0];

            var commitmentDataList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                // Extract the age and user count from each row and add it to the list
                string commitment = row["CommitmentType"].ToString();
                int userCount = (int)row["UserCount"];
                commitmentDataList.Add(new { Commitment = commitment, UserCount = userCount });
            }

            return Json(commitmentDataList);
        }
        public IActionResult StateData()
        {
            ChartModel chartModel = new ChartModel();

            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetStateChartData();
            DataTable dt = ds.Tables[0];

            var stateDateList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                // Extract the age and user count from each row and add it to the list
                string state = row["State"].ToString();
                int userCount = (int)row["UserCount"];
                stateDateList.Add(new { State = state, UserCount = userCount });
            }

            return Json(stateDateList);
        }
        public IActionResult YourLikeRatio()
        {
            ChartModel chartModel = new ChartModel();
            UserProfileModel userProfile = new UserProfileModel();
            int privateId = userProfile.getPrivateId(Request.Cookies["Username"].ToString());
            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetYourLikesRatioChartData(privateId);
            DataTable dt = ds.Tables[0];

            var yourLikeRatioList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                // Extract the age and user count from each row and add it to the list
                int totalLikesReceived = (int)row["TotalLikesReceived"];
                int totalLikesOverall = (int)row["TotalLikes"];
                yourLikeRatioList.Add(new { TotalLikesReceived = totalLikesReceived, TotalLikesOverall = totalLikesOverall });
            }

            return Json(yourLikeRatioList);
        }
        public IActionResult LikesToMatches()
        {
            try
            {
                ChartModel chartModel = new ChartModel();
                UserProfileModel userProfile = new UserProfileModel();
                string username = Request.Cookies["Username"].ToString();
                int privateId = userProfile.getPrivateId(username);
                DataSet ds = chartModel.GetLikesTurnedMatchesChartData();

                if (ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];

                    var totalLikes = dt.Rows.Count > 0 ? (int)dt.Rows[0]["TotalLikes"] : 0;
                    var likesTurnedMatches = dt1.Rows.Count > 0 ? (int)dt1.Rows[0]["LikesTurnedMatches"] : 0;

                    var likesToMatchesList = new
                    {
                        TotalLikes = totalLikes,
                        LikesTurnedMatches = likesTurnedMatches
                    };

                    return Json(likesToMatchesList);
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here, e.g., using ILogger
                return Json(new { error = ex.Message });
            }
            return Json(new { error = "No data available" });
        }
        public IActionResult MatchesToDates()
        {
            ChartModel chartModel = new ChartModel();

            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetMatchAndDateStatsChartData();
            DataTable dt = ds.Tables[0];

            var matchDateList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                int matchesTurnedDates = (int)row["MatchesTurnedDates"];
                int totalMatches = (int)row["TotalMatches"];
                matchDateList.Add(new { MatchesTurnedDates = matchesTurnedDates, TotalMatches = totalMatches });
            }

            return Json(matchDateList);
        }
        public IActionResult DatesToDatesPlanned()
        {
            ChartModel chartModel = new ChartModel();

            // Fetch data using the TP_Chart_Age stored procedure
            DataSet ds = chartModel.GetPlannedDatesChartData();
            DataTable dt = ds.Tables[0];

            var plannedDatesList = new List<object>(); // Create a list to hold your data

            foreach (DataRow row in dt.Rows)
            {
                int totalDatesCount = (int)row["TotalDatesCount"];
                int plannedDatesCount = (int)row["PlannedDatesCount"];
                plannedDatesList.Add(new { TotalDatesCount = totalDatesCount, PlannedDatesCount = plannedDatesCount });
            }

            return Json(plannedDatesList);
        }


    }
}
