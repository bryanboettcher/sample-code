using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinFormsGreatAgain.Annotations;

namespace WinFormsGreatAgain
{
    public class frmMainViewModel 
        : INotifyPropertyChanged  // needed for databinding, otherwise this is a POCO
    {
        private string _captionForm;
        private string _captionGroupBoxParent;
        private string _captionButtonOk;
        private string _captionButtonCancel;
        private string _captionLabelFirstName;
        private string _captionLabelLastName;
        private string _captionLabelPhoneNumber;
        private bool _enabledButtonOk;

        public string CaptionForm
        {
            get => _captionForm;
            set
            {
                _captionForm = value;
                OnPropertyChanged();
            }
        }

        public string CaptionGroupBoxParent
        {
            get => _captionGroupBoxParent;
            set
            {
                _captionGroupBoxParent = value;
                OnPropertyChanged();
            }
        }

        public string CaptionButtonOk
        {
            get => _captionButtonOk;
            set
            {
                _captionButtonOk = value;
                OnPropertyChanged();
            }
        }

        public string CaptionButtonCancel
        {
            get => _captionButtonCancel;
            set
            {
                _captionButtonCancel = value;
                OnPropertyChanged();
            }
        }

        public string CaptionLabelFirstName
        {
            get => _captionLabelFirstName;
            set
            {
                _captionLabelFirstName = value;
                OnPropertyChanged();
            }
        }

        public string CaptionLabelLastName
        {
            get => _captionLabelLastName;
            set
            {
                _captionLabelLastName = value;
                OnPropertyChanged();
            }
        }

        public string CaptionLabelPhoneNumber
        {
            get => _captionLabelPhoneNumber;
            set
            {
                _captionLabelPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public bool EnabledButtonOk
        {
            get => _enabledButtonOk;
            set
            {
                _enabledButtonOk = value;
                OnPropertyChanged();
            }
        }

        // The following 3 properties do not call OnPropertyChanged
        // because we are expecting data to be pushed *into* them from
        // the UI, so we do not need to notify the UI that the data
        // has changed -- it knows, it's who told us.
        // 
        // There is no rule that these can't use OnPropertyChanged,
        // just our code is what would listen for it, not the Form
        public string InputFirstName { get; set; }
        public string InputLastName { get; set; }
        public string InputPhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CloseForm;

        public event EventHandler<string> ShowStatusMessage;

        public void OnCloseForm() => CloseForm?.Invoke(this, EventArgs.Empty);

        public void OnShowStatusMessage(string message) => ShowStatusMessage?.Invoke(this, message);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
