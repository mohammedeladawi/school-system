using System.Globalization;

namespace SchoolProject.Data.Helpers;

public static class GeneralLocalizableEntity
{
    public static string LocalizeText(string textEn, string textAr)
    {
        var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        if (currentCulture == "ar")
            return textAr;

        return textEn;
    }
}