using BadgerClan.Maui.Messages;
using BadgerClan.Maui.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace BadgerClan.Maui.Views;

public partial class ClientPage : ContentPage
{
	public ClientPage(ClientViewModel cvm)
	{
		InitializeComponent();
        BindingContext = cvm;
	}

    private void Name_TextChanged(object sender, TextChangedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FieldChangedMessage());
    }

    private void Url_TextChanged(object sender, TextChangedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FieldChangedMessage());
    }

    private void OnCheckChange(object sender, CheckedChangedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new ClientTypeChanged(e.Value));
    }
}