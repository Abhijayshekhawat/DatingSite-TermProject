using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class SecurityQuestion
    {
        private int privateid;
        private string question_one;
        private string question_two;
        private string question_three;
        private string answer_one;
        private string answer_two;
        private string answer_three;

        public SecurityQuestion() { }


        public SecurityQuestion(int privateid, string question_one, string question_two, string question_three, string answer_one, string answer_two, string answer_three)
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

        public int AddSecurityQuestions(int privateid, string question1, string question2 , string question3, string answer1, string answer2, string answer3)
        {
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpsertSecurityQuestion";

            SqlParameter inputParameter2 = new SqlParameter("@PrivateId", privateid);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@QuestionOne", question1);
            objCommand.Parameters.Add(inputParameter3);

            SqlParameter inputParameter4 = new SqlParameter("@QuestionTwo", question2);
            objCommand.Parameters.Add(inputParameter4);

            SqlParameter inputParameter5 = new SqlParameter("@QuestionThree", question3);
            objCommand.Parameters.Add(inputParameter5);

            SqlParameter inputParameter6 = new SqlParameter("@AnswerOne", answer1);
            objCommand.Parameters.Add(inputParameter6);

            SqlParameter inputParameter7 = new SqlParameter("@AnswerTwo", answer2);
            objCommand.Parameters.Add(inputParameter7);

            SqlParameter inputParameter8 = new SqlParameter("@AnswerThree", answer3);
            objCommand.Parameters.Add(inputParameter8);

            int AddSecurityQuestion = objDB.DoUpdateUsingCmdObj(objCommand);

            return AddSecurityQuestion;
        }



    }
}
