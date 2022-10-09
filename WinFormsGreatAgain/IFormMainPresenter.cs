namespace WinFormsGreatAgain;

public interface IFormMainPresenter
{
    frmMainViewModel ViewModel { get; }

    void OnFormLoad(object sender, EventArgs eventArgs);

    void OnButtonOk_Click(object sender, EventArgs eventArgs);
    void OnAnyInput_Validated(object sender, EventArgs eventArgs);
    void OnButtonCancel_Click(object sender, EventArgs eventArgs);
}