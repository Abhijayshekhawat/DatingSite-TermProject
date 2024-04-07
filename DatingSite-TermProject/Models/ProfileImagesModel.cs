namespace DatingSite_TermProject.Models
{
    public class ProfileImagesModel
    {
        private int privateId;
        private string addImage1;
        private string addImage2;
        private string addImage3;
        private string addImage4;
        private string addImage5;

        public ProfileImagesModel()
        {
        }

        public ProfileImagesModel(int pId, string aI1, string aI2, string aI3,
                   string aI4, string aI5)
        {
            this.privateId = pId;
            this.addImage1 = aI1;
            this.addImage2 = aI2;
            this.addImage3 = aI3;
            this.addImage4 = aI4;
            this.addImage5 = aI5;
        }
        public int PrivateId
        {
            get { return privateId; }
            set { privateId = value; }
        }
        public string Image1
        {
            get { return addImage1; }
            set { addImage1 = value; }
        }
        public string Image2
        {
            get { return addImage2; }
            set { addImage2 = value; }
        }
        public string Image3
        {
            get { return addImage3; }
            set { addImage3 = value; }
        }
        public string Image4
        {
            get { return addImage4; }
            set { addImage4 = value; }
        }
        public string Image5
        {
            get { return addImage5; }
            set { addImage5 = value; }
        }
    }
}
