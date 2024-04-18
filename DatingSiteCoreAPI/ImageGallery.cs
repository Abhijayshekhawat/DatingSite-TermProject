using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class ImageGallery
    {
        private int imageId;
        private int privateId;
        private string image1;
        private string image2;
        private string image3;
        private string image4;
        private string image5;

        // Default constructor
        public ImageGallery()
        {
        }

        // Full constructor
        public ImageGallery(int imgID, int pID, string img1, string img2, string img3, string img4, string img5)
        {
            this.imageId = imgID;
            this.privateId = pID;
            this.image1 = img1;
            this.image2 = img2;
            this.image3 = img3;
            this.image4 = img4;
            this.image5 = img5;
        }

        // Public properties
        public int ImageId
        {
            get { return imageId; }
            set { imageId = value; }
        }

        public int PrivateId
        {
            get { return privateId; }
            set { privateId = value; }
        }

        public string Image1
        {
            get { return image1; }
            set { image1 = value; }
        }

        public string Image2
        {
            get { return image2; }
            set { image2 = value; }
        }

        public string Image3
        {
            get { return image3; }
            set { image3 = value; }
        }

        public string Image4
        {
            get { return image4; }
            set { image4 = value; }
        }

        public string Image5
        {
            get { return image5; }
            set { image5 = value; }
        }

        public int AddImages(int privateid, string image1, string image2, string image3, string image4, string image5)
        {
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpsertImageGallery";

            SqlParameter inputParameter1 = new SqlParameter("@PrivateId", privateid);
            objCommand.Parameters.Add(inputParameter1); 

            SqlParameter inputParameter2 = new SqlParameter("@Image1", image1);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@Image2", image2);
            objCommand.Parameters.Add(inputParameter3);

            SqlParameter inputParameter4 = new SqlParameter("@Image3", image3);
            objCommand.Parameters.Add(inputParameter4);

            SqlParameter inputParameter5 = new SqlParameter("@Image4", image4);
            objCommand.Parameters.Add(inputParameter5);

            SqlParameter inputParameter6 = new SqlParameter("@Image5", image5);
            objCommand.Parameters.Add(inputParameter6);

            int AddSecurityQuestion = objDB.DoUpdateUsingCmdObj(objCommand);

            return AddSecurityQuestion;
        }
    }
}
