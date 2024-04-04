namespace DatingSite_TermProject.Models
{
    public class SecurityQuestionModel
    {
        private int privateid;
        private string question_one;
        private string question_two;
        private string question_three;
        private string answer_one;
        private string answer_two;
        private string answer_three;

        public SecurityQuestionModel() { }


        public SecurityQuestionModel(int privateid, string question_one, string question_two, string question_three, string answer_one, string answer_two, string answer_three)
        {
            this.privateid = privateid;
            this.question_one = question_one;
            this.question_two = question_two;
            this.question_three = question_three;
            this.answer_one = answer_one;
            this.answer_two = answer_two;
            this.answer_three = answer_three;
        }



        public int PrivateId

        {
            get { return privateid; }
            set { privateid = value; }

        }
        public string Question_One
        {
            get { return question_one; }
            set { question_one = value; }
        }

        public string Question_Two
        {
            get { return question_two; }
            set { question_two = value; }
        }

        public string Question_Three
        {
            get { return question_three; }
            set { question_three = value; }

        }

        public string Answer_One
        {
            get { return answer_one; }
            set { answer_one = value; }

        }

        public string Answer_Two
        {
            get { return answer_two; }
            set { answer_two = value; }

        }

        public string Answer_Three
        {
            get { return answer_three; }
            set { answer_three = value; }

        }





    }
}
