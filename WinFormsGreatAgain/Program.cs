using System.Runtime.CompilerServices;

namespace WinFormsGreatAgain
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // In a "real application", this would be done with a DI container and
            // registrations or auto-scanning.
            //
            // Instead of newing up the form on line 29, you would instead
            // see something like:
            //
            // var form = container.GetInstance<frmMain>();
            //
            // and that would be passed into Application.Run() instead.

            var databaseEngine = new FakeDatabase();
            var translations = new HardcodedEnglishTranslations();
            var formPresenter = new DefaultFormMainPresenter(translations, databaseEngine);
            var viewModel = formPresenter.ViewModel;
            var form = new frmMain(formPresenter, viewModel);

            Application.Run(form);
        }
    }
}