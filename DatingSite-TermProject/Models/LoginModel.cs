using System.ComponentModel.DataAnnotations;

namespace DatingSite_TermProject.Models
{
    public class LoginModel
    {
        private string userName;
        private string password;

        public LoginModel()
        {
        }

        public LoginModel(string un, string pwd)
        {
            Username = un;
            Password = pwd;
        }
        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

}
