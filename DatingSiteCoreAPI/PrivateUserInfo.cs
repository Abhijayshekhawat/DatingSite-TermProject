
using Utilities;
using System.Data;
using System.Data.SqlClient; 



namespace DatingSiteCoreAPI
{
    public class PrivateUserInfo
    {
        private int privateid;
        private string firstname;
        private string lastname;
        private string email;
        private string privateusername;
        private string password;


        public PrivateUserInfo()
        {

        }
        
        public int CreateAccount(string FirstName, string LastName, string Email, string PrivateUsername, string Password)
        {
            // enter parameters! for this one 
            PrivateUserInfo privateinfo = new PrivateUserInfo();
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CreateAccount";

            SqlParameter inputParameter2 = new SqlParameter("@FirstName", FirstName);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@LastName", LastName);
            objCommand.Parameters.Add(inputParameter3);


            SqlParameter inputParameter4 = new SqlParameter("@Email", Email);
            objCommand.Parameters.Add(inputParameter4);

            SqlParameter inputParameter5 = new SqlParameter("@PrivateUsername", PrivateUsername);
            objCommand.Parameters.Add(inputParameter5);
            SqlParameter inputParameter6 = new SqlParameter("@Password", Password);
            objCommand.Parameters.Add(inputParameter6);

            int AddUser = objDB.DoUpdateUsingCmdObj(objCommand);
            
            return AddUser;


        }

        public int Login(string username, string password)
        {
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.CommandText = "UserLogin"; 

            SqlParameter inputParameter = new SqlParameter("@Username", username);
            objCommand.Parameters.Add(inputParameter);

            SqlParameter inputParameter2 = new SqlParameter("@Password", password);
            objCommand.Parameters.Add(inputParameter2);

           

            DataSet ds = objDB.GetDataSet(objCommand);

            int SuccessfulLogin = ds.Tables[0].Rows.Count;

            return SuccessfulLogin;
        }


        public PrivateUserInfo(int privateid, string firstname, string lastname, string email, string privateusername, string password)
        {
            this.privateid = privateid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.privateusername = privateusername;
            this.password = password;

        }


        public int PrivateId
        {
            get {return privateid;}
            set {privateid = value;}
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value;}
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
