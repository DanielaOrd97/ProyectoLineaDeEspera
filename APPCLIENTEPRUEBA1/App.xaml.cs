namespace APPCLIENTEPRUEBA1
{
    public partial class App : Application
    {
        internal static object Service;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
