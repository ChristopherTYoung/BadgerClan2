using BadgerClan.Maui.ViewModels;

namespace BadgerClan.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel bc)
        {
            InitializeComponent();
            BindingContext = bc;
        }

    }

}
