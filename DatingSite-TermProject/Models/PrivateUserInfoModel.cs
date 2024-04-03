using DatingSiteCoreAPI;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSite_TermProject.Models
{
    public class PrivateUserInfoModel
    {
        private int privateid;
        private string firstname;
        private string lastname;
        private string email;
        private string privateusername;
        private string password;



        public PrivateUserInfoModel()
        {



        }

        public PrivateUserInfoModel(int privateid, string firstname, string lastname, string email, string privateusername, string password)
        {
            this.privateid = privateid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.privateusername = privateusername;
            this.password = password;

        }

        public DataSet GetUserInfo(string username)
        {
            PrivateUserInfo privateinfo = new PrivateUserInfo();
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUserInfo";

            SqlParameter inputParameter2 = new SqlParameter("@Username", username);
            objCommand.Parameters.Add(inputParameter2);

            

            DataSet ds = objDB.GetDataSet(objCommand);

            return ds;
        }


        public int PrivateId
        {
            get { return privateid; }
            set { privateid = value; }
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }

        }

        public string PrivateUsername
        {
            get { return privateusername; }
            set { privateusername = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }



    }
}
