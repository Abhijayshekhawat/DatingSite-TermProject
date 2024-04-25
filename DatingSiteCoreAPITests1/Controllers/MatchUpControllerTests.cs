using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSiteCoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI.Controllers.Tests
{
    [TestClass()]
    public class MatchUpControllerTests
    {
        LikeRequest like = new LikeRequest();
        LikeRequest like2 = new LikeRequest();
        [TestMethod()]
        public void LikeUserTest()
        {
            UserProfile p = new UserProfile();      
            like.LikerUsername = "sara_connor";
            like.LIkeeId = 80;
            int privateid = p.getPrivateId(like.LikerUsername);

            int Likerid = 0;
            int Likeeid = 0;

            int Likerid2 = 0;
            int Likeeid2 = 0;

             MatchUpController controller = new MatchUpController();
            // if the database method runs correctly it will --> return true.
            bool ExpectedValue = true;
            bool ActualValue = controller.LikeUser(like);

            Assert.AreEqual(ExpectedValue, ActualValue);
             //in this api method we take in the liker username and find their private id in backend and get the likee id by using post method that is attach to the like button





            //After the api successfully added the like. i used a storeprocedure
            //that will go into the likes table and check for the new liker & likee
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();



            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter1 = new SqlParameter("@privateid", privateid);
            objCommand.Parameters.Add(inputParameter1);

            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue2 = 1; 
            int ActualValue2= dt.Rows.Count;
            Assert.AreEqual(ExpectedValue2, ActualValue2);



            foreach (DataRow dr in dt.Rows)
            {

                Likerid = Int32.Parse(dr["LikerID"].ToString());
                Likeeid = Int32.Parse(dr["LikeeID"].ToString());


            }
            // both ids of liker and likee are in the like table as plan.
            int ExpectedValue3 = 72;
            int ActualValue3 = Likerid;
            Assert.AreEqual(ExpectedValue3, ActualValue3);

            int ExpectedValue4 = 80;
            int ActualValue4 = Likeeid;

            Assert.AreEqual(ExpectedValue4, ActualValue4);

            like2.LikerUsername = "happy_hogan";
            like2.LIkeeId = 72;

            MatchUpController controller2 = new MatchUpController();

            bool ExpectedValue5 = true;
            bool ActualValue5 = controller2.LikeUser(like2);

            Assert.AreEqual(ExpectedValue5, ActualValue5);


            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();


            int privateid2 = p.getPrivateId(like2.LikerUsername);
            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter2 = new SqlParameter("@privateid", privateid2);
            objCommand2.Parameters.Add(inputParameter2);

            DataSet ds2 = objDB2.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue6 = 1;
            int ActualValue6 = dt2.Rows.Count;
            Assert.AreEqual(ExpectedValue6, ActualValue6);



            foreach (DataRow dr in dt2.Rows)
            {

                Likerid2 = Int32.Parse(dr["LikerID"].ToString());
                Likeeid2 = Int32.Parse(dr["LikeeID"].ToString());


            }

            int ExpectedValue7 = 98;
            int ActualValue7 = Likerid2;

            Assert.AreEqual(ExpectedValue7, ActualValue7);
            int ExpectedValue8 = 72;
            int ActualValue8 = Likeeid2;

            Assert.AreEqual(ExpectedValue8,ActualValue8);   

         

        }

        [TestMethod]
        // first test if the count is 1 then test if the data is inside.
        // then delete it. then the count should be 0 which mean theres no row that has those ids.
        // since my store procedure is getting likerid from like table using the username privateid
        public void DeleteLikesTest()
        {
            DislikeRequest dislike = new DislikeRequest();  
            UserProfile p = new UserProfile();
            dislike.LikerUsername = "sara_connor";
            dislike.LikeeId = 80;
            int privateid = p.getPrivateId(dislike.LikerUsername);

            // after liking if i use this store procedure where i select the user private id in the like table to see if it was added successfully
            // if it does select then the datatable row should have 1
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();



            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter1 = new SqlParameter("@privateid", privateid);
            objCommand.Parameters.Add(inputParameter1);

            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue = 1;
            int ActualValue = dt.Rows.Count;
            Assert.AreEqual(ExpectedValue, ActualValue);

            // now I will delete the like 
            MatchUpController controller = new MatchUpController();

            bool ExpectedValue2 = true; 
           bool ActualValue2 =  controller.Hater(dislike);

            Assert.AreEqual(ExpectedValue2,ActualValue2);

            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();



            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter2 = new SqlParameter("@privateid", privateid);
            objCommand2.Parameters.Add(inputParameter2);

            DataSet ds2 = objDB2.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue3 = 0;
            int ActualValue3 = dt2.Rows.Count;

            Assert.AreEqual(ExpectedValue3,ActualValue3);   






        }
    }
}