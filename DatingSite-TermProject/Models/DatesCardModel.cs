namespace DatingSite_TermProject.Models
{
    public class DatesCardModel
    {
        private List<CardsModel> datesYouSent;
        private List<CardsModel> datesYouReceived;

        public List<CardsModel> DatesYouSent
        {
            get { return datesYouSent; }
            set { datesYouSent = value; }
        }

        public List<CardsModel> DatesYouReceived
        {
            get { return datesYouReceived; }
            set { datesYouReceived = value; }
        }
    }
}
