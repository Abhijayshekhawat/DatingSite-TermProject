using System.Net;
using System.Net.Mail;
using System.Text;

using System.Threading.Tasks;

namespace DatingSite_TermProject.Models
{
    public class EmailModel

    {
        private MailMessage objMail = new MailMessage();

        private MailAddress toAddress;

        private MailAddress fromAddress;

        private MailAddress ccAddress;

        private MailAddress bccAddress;

        private String subject;

        private String messageBody;

        private Boolean isHTMLBody = true;

        private MailPriority priority = MailPriority.Normal;

        private String mailHost = "smtp.temple.edu";



        public void SendMail(String recipient, String sender, String subject, String body, String cc = "", String bcc = "")

        {

            try
            {
                // Initialize the MailMessage object
                MailMessage objMail = new MailMessage()
                {
                    From = new MailAddress(sender),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = this.isHTMLBody,
                    Priority = this.priority
                };

                objMail.To.Add(new MailAddress(recipient));

                // Add CC and BCC if provided
                if (!string.IsNullOrEmpty(cc))
                {
                    objMail.CC.Add(new MailAddress(cc));
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    objMail.Bcc.Add(new MailAddress(bcc));
                }

                // Configure the SmtpClient like below to use Papercut
                //using (SmtpClient smtpMailClient = new SmtpClient("localhost", 25))
                //{
                //    // No need for credentials or SSL with Papercut
                //    smtpMailClient.EnableSsl = false;
                //    smtpMailClient.UseDefaultCredentials = true;
                //}

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw; // Or handle this exception more gracefully
            }



        }





        public Boolean SendMail()

        {

            try

            {

                objMail.To.Add(this.toAddress);

                objMail.From = this.fromAddress;

                objMail.Subject = this.subject;

                objMail.Body = this.messageBody;

                objMail.IsBodyHtml = this.isHTMLBody;

                objMail.Priority = this.priority;



                if (!ccAddress.Equals(String.Empty))

                    objMail.CC.Add(this.ccAddress);



                if (!bccAddress.Equals(String.Empty))

                    objMail.Bcc.Add(this.bccAddress);



                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);

                smtpMailClient.Send(objMail);



                return true;

            }

            catch (Exception ex)

            {

                return false;

            }

        }



        public String Recipient

        {

            get { return this.toAddress.ToString(); }

            set { this.toAddress = new MailAddress(value); }

        }



        public String Sender

        {

            get { return this.fromAddress.ToString(); }

            set { this.fromAddress = new MailAddress(value); }

        }



        public String CCAddress

        {

            get { return this.ccAddress.ToString(); }

            set { this.ccAddress = new MailAddress(value); }

        }



        public String BCCAddress

        {

            get { return this.bccAddress.ToString(); }

            set { this.bccAddress = new MailAddress(value); }

        }



        public String Subject

        {

            get { return this.subject; }

            set { this.subject = value; }

        }



        public String Message

        {

            get { return this.messageBody; }

            set { this.messageBody = value; }

        }



        public Boolean HTMLBody

        {

            get { return this.isHTMLBody; }

            set { this.isHTMLBody = value; }

        }



        public MailPriority Priority

        {

            get { return this.priority; }

            set { this.priority = value; }

        }



        public String MailHost

        {

            get { return this.mailHost; }

            set { this.mailHost = value; }

        }

    }




}

