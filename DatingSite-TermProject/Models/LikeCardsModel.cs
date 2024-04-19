namespace DatingSite_TermProject.Models
{
    public class LikesCardModel
    {
        private List<CardsModel> peopleWhoLikedYou;
        private List<CardsModel> peopleYouLiked;

        public List<CardsModel> PeopleWhoLikedYou
        {
            get { return peopleWhoLikedYou; }
            set { peopleWhoLikedYou = value; }
        }

        public List<CardsModel> PeopleYouLiked
        {
            get { return peopleYouLiked; }
            set { peopleYouLiked = value; }
        }
    }
}


