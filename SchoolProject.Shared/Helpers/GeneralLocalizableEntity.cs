using System.Globalization;

namespace SchoolProject.Shared.Helpers;

public static class GeneralLocalizableEntity
{
    public static string LocalizeText(string textEn, string? textAr)
    {
        var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        if (currentCulture == "ar" && !string.IsNullOrWhiteSpace(textAr))
            return textAr;

        return textEn;
    }
}