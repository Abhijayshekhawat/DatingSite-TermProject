namespace DatingSite_TermProject.Models
{
    public class PlanDateCardModel
    {
        private List<CardsModel> datesNotYetPlanned;
        private List<CardsModel> plannedDates;
        public List<CardsModel> DatesNotYetPlanned
        {
            get { return datesNotYetPlanned; }
            set { datesNotYetPlanned = value; }
        }

        public List<CardsModel> PlannedDates
        {
            get { return plannedDates; }
            set { plannedDates = value; }
        }
    }
}
