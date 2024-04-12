using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DatingSiteWebService
{
    public class Cards
    {
        private string firstname;
        private string lastname;
        private string profilephotoUrl;
        private string description;
        private string city;
        private string state;


        public Cards() { }
        

        public Cards(string firstname, string lastname, string profilephotoUrl, string description, string city, string state)
        {
            this.firstname = firstname; 
            this.lastname = lastname;
            this.profilephotoUrl = profilephotoUrl;
            this.description = description;
            this.city = city;
            this.state = state;

        }
        

        public string FirstName
        {
            get { return this.firstname; }
            set { this.firstname = value; }

        }
        public string LastName
        {
            get { return this.lastname; }
            set { this.lastname = value; }

        }


        public string ProfilePhotoURL
        {
            get { return this.profilephotoUrl; }
            set { this.profilephotoUrl = value;}
        }

        public string Description
        {
            get { return this.description; }    
            set { this.description = value; }

        }


        public string City
        {
            get { return this.city; }
            set { this.city = value; }

        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }

        }

    }
}