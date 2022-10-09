using System.Text.RegularExpressions;

namespace WinFormsGreatAgain;


// Presenters (similar to web Controllers) are fairly specific for the form
// they represent.  They can be interchanged if different behavior is desired
// but usually there's a single implementation.  These are behind an interface
// because they can be mocked if needed.

// the OnXXX methods are how the UI communicates back to the business
// logic code.  These calls can all be asserted-as-called if a mock instance
// is used, or called directly as the subject of a unit test.
public class DefaultFormMainPresenter : IFormMainPresenter
{
    private readonly ITranslations _translations;
    private readonly IDatabase _database;
        
    private static readonly frmMainViewModel _formModel;

    static DefaultFormMainPresenter()
    {
        _formModel = new frmMainViewModel();
    }

    public DefaultFormMainPresenter(ITranslations translations, IDatabase database)
    {
        _translations = translations;
        _database = database;
    }

    // ViewModels are *extremely* tightly-coupled to the UI they represent
    // No sense for interfaces here, the ViewModel has no dependencies by design,
    // and it is not interchangeable with another ViewModel.
    public frmMainViewModel ViewModel => _formModel;

    public void OnFormLoad(object sender, EventArgs eventArgs)
    {
        // this is the time to initialize the display, so our
        // primary step is to initialize the captions from the
        // fake translation provider.

        // Realistically, there are better ways to do this kind of thing.
        // They involve enumerating properties with reflections and magic-names,
        // but this way seemed to be the most straightforward for a small demo.
        _formModel.CaptionButtonCancel = _translations.GetTranslation(nameof(_formModel.CaptionButtonCancel));
        _formModel.CaptionButtonOk = _translations.GetTranslation(nameof(_formModel.CaptionButtonOk));

        _formModel.CaptionForm = _translations.GetTranslation(nameof(_formModel.CaptionForm));
        _formModel.CaptionGroupBoxParent = _translations.GetTranslation(nameof(_formModel.CaptionGroupBoxParent));

        _formModel.CaptionLabelFirstName = _translations.GetTranslation(nameof(_formModel.CaptionLabelFirstName));
        _formModel.CaptionLabelLastName = _translations.GetTranslation(nameof(_formModel.CaptionLabelLastName));
        _formModel.CaptionLabelPhoneNumber = _translations.GetTranslation(nameof(_formModel.CaptionLabelPhoneNumber));

        _formModel.InputFirstName = string.Empty;
        _formModel.InputLastName = string.Empty;
        _formModel.InputPhoneNumber = string.Empty;
    }

    public void OnButtonOk_Click(object sender, EventArgs eventArgs)
    {
        _database.SaveData();
        
        _formModel.OnShowStatusMessage(_translations.GetTranslation("SaveSuccessful"));
        _formModel.OnCloseForm();
    }

    public void OnAnyInput_Validated(object sender, EventArgs eventArgs)
    {
        // we are using this one method to handle validation for all 3 textboxes
        // but there is no reason it couldn't be done with 3 individual methods,
        // depending on what makes more sense for the application

        _formModel.EnabledButtonOk = IsDataValid();
    }

    public void OnButtonCancel_Click(object sender, EventArgs eventArgs)
    {
        _formModel.OnCloseForm();
    }

    private bool IsDataValid()
    {
        // larger applications would probably inject an IDataValidator<T> for the correct
        // view model type to handle this, but I feel like that would over-complicate
        // this small demo.
        
        var firstNameValid = !string.IsNullOrWhiteSpace(_formModel.InputFirstName);
        var lastNameValid = !string.IsNullOrWhiteSpace(_formModel.InputLastName);
        var phoneNumberValid = Regex.IsMatch(_formModel.InputPhoneNumber, @"(\d{3}\-?)?\d{3}\-?\d{4}");

        return firstNameValid && lastNameValid && phoneNumberValid;
    }
}