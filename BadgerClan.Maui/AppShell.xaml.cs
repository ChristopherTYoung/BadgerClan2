namespace BadgerClan.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("client", typeof(MainPage));
        }
    }
}
