using System.Web.Configuration;

namespace AnimalStore.Web.ViewModels
{
    public class ContactInformation
    {
        public string MainPhone
        {
            get { return WebConfigurationManager.AppSettings[AppSettingKeys.MainPhone]; }
        }

        public string OutOfHoursPhone
        {
            get { return WebConfigurationManager.AppSettings[AppSettingKeys.OutOfHoursPhone]; }
        }

        public string Email
        {
            get { return WebConfigurationManager.AppSettings[AppSettingKeys.Email]; }
        }

        public string MarketingEmail
        {
            get { return WebConfigurationManager.AppSettings[AppSettingKeys.MarketingEmail]; }
        }
    }
}