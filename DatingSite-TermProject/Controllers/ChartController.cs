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

    }
}
