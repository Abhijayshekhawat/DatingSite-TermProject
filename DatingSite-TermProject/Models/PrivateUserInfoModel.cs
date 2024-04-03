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
