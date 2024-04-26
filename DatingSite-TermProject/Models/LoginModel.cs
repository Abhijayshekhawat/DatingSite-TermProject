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

        [Required(ErrorMessage = "*Username is required*")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters long")]
        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        [Required(ErrorMessage = "*Password is required*")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

}
