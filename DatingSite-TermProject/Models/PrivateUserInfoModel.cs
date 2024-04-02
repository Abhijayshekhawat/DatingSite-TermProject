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



    }
}
