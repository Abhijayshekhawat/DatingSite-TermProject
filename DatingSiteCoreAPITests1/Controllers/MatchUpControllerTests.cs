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
            int ActualValue2 = dt.Rows.Count;
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

            Assert.AreEqual(ExpectedValue8, ActualValue8);



        }

        [TestMethod]
        // first test if the count is 1 then test if the data is inside.
        // then delete it. then the count should be 0 which mean theres no row that has those ids.
        // since my store procedure is getting likerid from like table using the username privateid
        public void DeleteLikesTest()
        {
            DislikeRequest dislike = new DislikeRequest();
            DislikeRequest dislike2 = new DislikeRequest();
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
            bool ActualValue2 = controller.Hater(dislike);

            Assert.AreEqual(ExpectedValue2, ActualValue2);

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

            Assert.AreEqual(ExpectedValue3, ActualValue3);
            // second example  
            dislike2.LikerUsername = "happy_hogan";
            dislike2.LikeeId = 72;
            int privateid2 = p.getPrivateId(dislike2.LikerUsername);

            DBConnect objDB3 = new DBConnect();
            SqlCommand objCommand3 = new SqlCommand();



            objCommand3.CommandType = CommandType.StoredProcedure;
            objCommand3.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter3 = new SqlParameter("@privateid", privateid2);
            objCommand3.Parameters.Add(inputParameter3);

            DataSet ds3 = objDB3.GetDataSet(objCommand3);

            DataTable dt3 = ds3.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue4 = 1;
            int ActualValue4 = dt3.Rows.Count;
            Assert.AreEqual(ExpectedValue4, ActualValue4);



            MatchUpController controller2 = new MatchUpController();

            bool ExpectedValue5 = true;
            bool ActualValue5 = controller.Hater(dislike2);

            Assert.AreEqual(ExpectedValue5, ActualValue5);


            DBConnect objDB4 = new DBConnect();
            SqlCommand objCommand4 = new SqlCommand();

            objCommand4.CommandType = CommandType.StoredProcedure;
            objCommand4.CommandText = "TP_GetLikeInfoTest";

            SqlParameter inputParameter4 = new SqlParameter("@privateid", privateid2);
            objCommand4.Parameters.Add(inputParameter4);

            DataSet ds4 = objDB4.GetDataSet(objCommand4);

            DataTable dt4 = ds4.Tables[0];


            // after doing the store procedure the table count should print out 1
            // if added successfully 
            int ExpectedValue6 = 0;
            int ActualValue6 = dt4.Rows.Count;

            Assert.AreEqual(ExpectedValue6, ActualValue6);


        }

        [TestMethod]

        public void UnMatchTest()
        {

            // checking to see if the match exist in our match table.
            int MatcherId = 69;
            int MatcheeId = 82;
            UnmatchRequest unmatch = new UnmatchRequest();


            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetMatchInfoTest";

            SqlParameter inputParameter1 = new SqlParameter("@privateid", MatcherId);
            objCommand.Parameters.Add(inputParameter1);
            SqlParameter inputParameter2 = new SqlParameter("@matcheeid", MatcheeId);
            objCommand.Parameters.Add(inputParameter2);
            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];

            int ExpectedCount = 1;
            int ActualCount = dt.Rows.Count;
            Assert.AreEqual(ExpectedCount, ActualCount);
            string name = "john_doe";
            MatchUpController controller = new MatchUpController();
            unmatch.MatcherUsername = name;
            unmatch.MatcherID = MatcheeId;
            controller.UnMatch(unmatch);
            // now after unmatching we going to test if the match is still there inside 
            int MatcherId2 = 69;
            int MatcheeId2 = 82;
            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();
            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetMatchInfoTest";

            SqlParameter inputParameter3 = new SqlParameter("@privateid", MatcherId2);
            objCommand2.Parameters.Add(inputParameter3);
            SqlParameter inputParameter4 = new SqlParameter("@matcheeid", MatcheeId2);
            objCommand2.Parameters.Add(inputParameter4);
            DataSet ds2 = objDB2.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];

            int ExpectedCount2 = 0;
            int ActualCount2 = dt2.Rows.Count;

            Assert.AreEqual(ExpectedCount2, ActualCount2);








        }

        [TestMethod]
        public void AddDateRequestTest()
        {
            string username = "sara_connor";
            int requesterid = 72;
            int requesteeid = 98;

            DateRequest date = new DateRequest();
            date.RequesterUsername = username;
            date.RequesteeId = requesteeid;

            MatchUpController controller = new MatchUpController();
            // if the send date succeed it will return true
            bool ExpectedValue = true;
            bool ActualValue = controller.SendDateRequest(date);
            Assert.AreEqual(ExpectedValue, ActualValue);
            // now ima check to see if the data is in there 

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDateInfoTest";

            SqlParameter inputParameter1 = new SqlParameter("@privateid", requesterid);
            objCommand.Parameters.Add(inputParameter1);
            SqlParameter inputParameter2 = new SqlParameter("@requesteeid", requesteeid);
            objCommand.Parameters.Add(inputParameter2);
            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];

            int ExpectedCount2 = 1;
            int ActualCount2 = dt.Rows.Count;
            Assert.AreEqual(ExpectedCount2, ActualCount2);
            // lets check to see if the ids are correct in the requestee/requester ids
            int ExpectedRequesterId = 0;
            int ActualRequesterid = requesterid;

            int ExpectedRequesteeId = 0;
            int ActualRequesteeid = requesteeid;
            foreach (DataRow dr in dt.Rows)
            {
                ExpectedRequesterId = Int32.Parse(dr["RequesterID"].ToString());
                ExpectedRequesteeId = Int32.Parse(dr["RequesteeID"].ToString());




            }

            Assert.AreEqual(ExpectedRequesterId, ActualRequesterid);
            Assert.AreEqual(ExpectedRequesteeId, ActualRequesteeid);

            string username2 = "tony_stark";
            int requesterid2 = 75;
            int requesteeid2 = 69;
            //
            DateRequest date2 = new DateRequest();
            date2.RequesterUsername = username2;
            date2.RequesteeId = requesteeid2;

            bool ExpectedValue2 = true;
            bool ActualValue2 = controller.SendDateRequest(date2);
            Assert.AreEqual(ExpectedValue2, ActualValue2);

            // now ima check to see if the data is in there 
            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();
            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetDateInfoTest";
            SqlParameter inputParameter3 = new SqlParameter("@privateid", requesterid2);
            objCommand2.Parameters.Add(inputParameter3);
            SqlParameter inputParameter4 = new SqlParameter("@requesteeid", requesteeid2);
            objCommand2.Parameters.Add(inputParameter4);
            DataSet ds2 = objDB2.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];

            int ExpectedCount3 = 1;
            int ActualCount3 = dt2.Rows.Count;
            Assert.AreEqual(ExpectedCount3, ActualCount3);
            // lets check to see if the ids are correct in the requestee/requester ids
            int ExpectedRequesterId2 = 0;
            int ActualRequesterid2 = requesterid2;

            int ExpectedRequesteeId2 = 0;
            int ActualRequesteeid2 = requesteeid2;
            foreach (DataRow dr in dt2.Rows)
            {
                ExpectedRequesterId2 = Int32.Parse(dr["RequesterID"].ToString());
                ExpectedRequesteeId2 = Int32.Parse(dr["RequesteeID"].ToString());




            }

            Assert.AreEqual(ExpectedRequesterId2, ActualRequesterid2);
            Assert.AreEqual(ExpectedRequesteeId2, ActualRequesteeid2);




        }
        [TestMethod]

        public void GetMatchesProfileTest()
        {
            // we used john doe a lot so he has a lot of matches in the match table
            // we will be testing our get methods --> this will get all his matches that he got while on the app
            MatchUpController controller = new MatchUpController();
            Match match = new Match();
            match.MatchUsername = "john_doe";
            List<Card> matchcards = controller.GetMatchesProfile(match);
            // check his data he has 6 matches 

            Assert.IsNotNull(matchcards);
            int ExpectedCardsCount = 6;
            int ActualCardsCount = matchcards.Count();


            Assert.AreEqual(ExpectedCardsCount, ActualCardsCount);
            // now we check to see if the data actually populated correctly

            Card card = matchcards.ElementAt(1);
            string ExpectedFirstName = "Will";
            string ActualFirstName = card.FirstName;
            Assert.AreEqual(ExpectedFirstName, ActualFirstName);

            string ExpectedCity = "Philadelphia";
            string ActualCity = card.City;

            Assert.AreEqual(ExpectedCity, ActualCity);
            // testing second user should be different from previous 
            Match match2 = new Match();
            match2.MatchUsername = "jane_doe";
            List<Card> matchcards2 = controller.GetMatchesProfile(match2);
            // check the data jane only have two matches 
            Assert.IsNotNull(matchcards2);
            int ExpectedCardsCount2 = 2;
            int ActualCardCount2 = matchcards2.Count(); 


            Assert.AreEqual(ExpectedCardsCount2,ActualCardCount2); 
            
            Card card2 = matchcards2.ElementAt(0);
            string ExpectedFirstName2 = "John";
            string ActualFirstName2 = card2.FirstName;

            Assert.AreEqual(ExpectedFirstName2, ActualFirstName2);
            int ExpectedAge = 29;
            int ActualAge = card2.Age;  

            Assert.AreEqual(ExpectedAge, ActualAge);    
        }

        [TestMethod]
        public void PopulateProfileTest()
        
        
        {
            MatchUpController controller = new MatchUpController();
            Profile p = new Profile();
            p.ProfilePrivateId= 1;
            List<Card> cards = controller.GetProfiles(p);
            // in the database we populated 31 accounts so the amount of profiles in the list should be 30

            Assert.IsNotNull(cards);
            int ExpectedCount = 33;
            int ActualCount = cards.Count();        
            Assert.AreEqual(ExpectedCount,ActualCount);

            Card card = cards.ElementAt(5);

            string ExpectedFirstName = "Tony";
            string ActualFirstName = card.FirstName;
            Assert.AreEqual(ExpectedFirstName, ActualFirstName);
            string ExpectedHeight = "5ft 5in";
            string ActualHeight = card.Height.TrimEnd();
            Assert.AreEqual(ExpectedHeight, ActualHeight);

           Card card2 = cards.ElementAt(6);

            string ExpectedFirstName2 = "Bruce";
            string ActualFirstName2 = card2.FirstName;
            Assert.AreEqual(ExpectedFirstName2, ActualFirstName2);
            int ExpectedAge = 27;
            int ActualAge = card2.Age;
            Assert.AreEqual(ExpectedAge,ActualAge);
            // test last person

            Card card3 = cards.ElementAt(30);
            string ExpectedFirstName3 = "Abhijay";
            string ActualFirstName3 = card3.FirstName;  
            Assert.AreEqual(ExpectedFirstName3, ActualFirstName3);  

        }

    }
}