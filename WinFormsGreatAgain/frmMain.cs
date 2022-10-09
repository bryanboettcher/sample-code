// set up an alias so the lines lower are a bit easier to read
using VM = WinFormsGreatAgain.frmMainViewModel;

namespace WinFormsGreatAgain
{
    public partial class frmMain : Form
    {
        private readonly IFormMainPresenter _formPresenter;
        private readonly VM _viewModel;

        /// <summary>
        /// Exists for the Designer.  Do not call manually.
        /// </summary>
        [Obsolete("Do not instantiate manually")]
        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(IFormMainPresenter formPresenter, VM viewModel)
        {
            _formPresenter = formPresenter;
            _viewModel = viewModel;

            InitializeComponent();

            // This constructor is the one called by the startup code.  After the components
            // are initialized, we can set up the data binding.  There is a UI way to do it,
            // but it gets sort of confused since WinForms databinding is primarily
            // designed to do database-related work.
            //
            // The code below sets up the data binding for the various controls.  For the
            // most part, the property of interest is "Text", which controls the caption of
            // labels, or the value of a textbox.  The OK button also gets the "Enabled"
            // property bound, too.  Databinding is a 2-way direction, so whatever is typed
            // into the textboxes is immediately put into the POCO once OnValidate() finishes
            // executing on the textboxes.  It is possible to subscribe to the _Validate event
            // on them to do custom handling, but without doing that they just no-op by default.
            //
            this.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionForm));
            gbParent.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionGroupBoxParent));
            btnOk.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionButtonOk));
            btnCancel.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionButtonCancel));
            lblFirstName.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionLabelFirstName));
            lblLastName.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionLabelLastName));
            lblPhoneNumber.DataBindings.Add("Text", _viewModel, nameof(VM.CaptionLabelPhoneNumber));

            txtFirstName.DataBindings.Add("Text", _viewModel, nameof(VM.InputFirstName), false, DataSourceUpdateMode.OnValidation);
            txtLastName.DataBindings.Add("Text", _viewModel, nameof(VM.InputLastName), false, DataSourceUpdateMode.OnValidation);
            txtPhoneNumber.DataBindings.Add("Text", _viewModel, nameof(VM.InputPhoneNumber), false, DataSourceUpdateMode.OnValidation);

            btnOk.DataBindings.Add("Enabled", _viewModel, nameof(VM.EnabledButtonOk));

            _viewModel.CloseForm += _viewModel_CloseForm;
            _viewModel.ShowStatusMessage += _viewModel_ShowStatusMessage;
        }

        private void _viewModel_ShowStatusMessage(object sender, string e) 
            => MessageBox.Show(e, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void _viewModel_CloseForm(object sender, EventArgs e) 
            => this.Close();

        // The majority of the code in the form is just forwarding the used events
        // into the form's Presenter.  As new events are needed, modify the interface
        // to add a new method call, and forward the parameters in.

        private void frmMain_Load(object sender, EventArgs e) 
            => _formPresenter.OnFormLoad(sender, e);

        private void btnOk_Click(object sender, EventArgs e) 
            => _formPresenter.OnButtonOk_Click(sender, e);

        private void txtFirstName_Validated(object sender, EventArgs e) 
            => _formPresenter.OnAnyInput_Validated(sender, e);

        private void txtLastName_Validated(object sender, EventArgs e) 
            => _formPresenter.OnAnyInput_Validated(sender, e);

        private void txtPhoneNumber_Validated(object sender, EventArgs e) 
            => _formPresenter.OnAnyInput_Validated(sender, e);

        private void btnCancel_Click(object sender, EventArgs e) 
            => _formPresenter.OnButtonCancel_Click(sender, e);
    }
}