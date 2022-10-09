namespace WinFormsGreatAgain;

public class HardcodedEnglishTranslations : ITranslations
{
    public string GetTranslation(string key) =>
        key.ToLowerInvariant() switch
        {
            "captionbuttoncancel" => "Cancel",
            "captionbuttonok" => "OK",
            "captionform" => "Dumb Lil' App",
            "captiongroupboxparent" => "User Information",
            "captionlabelfirstname" => "First Name:",
            "captionlabellastname" => "Last Name:",
            "captionlabelphonenumber" => "Phone Number:",
            "savesuccessful" => "Successfully saved data!",
            _ => $"!! {key} !!" // show that we don't know how to translate this
        };
}