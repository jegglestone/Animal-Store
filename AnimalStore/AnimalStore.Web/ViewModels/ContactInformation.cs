namespace AnimalStore.Web.ViewModels
{
    public class ContactInformation
    {
        public string MainPhone 
        { 
            get { return _mainPhone; } 
        }

        public string OutOfHoursPhone
        {
            get { return _outOfHoursPhone; }
        }

        public string Email 
        { 
            get { return _email; } 
        }

        private const string _mainPhone = "0113";
        private const string _outOfHoursPhone = "0113";
        private const string _email = "joe@animalstore.com";
    }
}