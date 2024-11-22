using System.Globalization;

namespace School.infrastructure.Commons
{
    public class LocalizableEntity
    {
        // this class will be inherited to all entites you have 

        public string GetLocalized(string textAr, string textEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return textAr;
            }
            else
            {
                return textEn;
            }
        }
    }
}
