using System.Data;
using Utilities;

namespace DatingSite_TermProject.Models
{
    public class MatchesModel
    {
        private int matcherID;
        private string matcherUsername;

        public MatchesModel() { }
        public MatchesModel(int mID, string mUN)
        {
            this.matcherID = matcherID;
            this.matcherUsername = matcherUsername;

        }

        public int MatcherID
        {
            get { return matcherID; }
            set { matcherID = value; }
        }
        public string MatcherUsername
        {
            get { return matcherUsername; }
            set { matcherUsername = value; }
        }
    }

}

