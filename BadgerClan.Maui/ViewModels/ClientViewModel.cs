using BadgerClan.Maui.Messages;
using BadgerClan.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.ViewModels
{
    public partial class ClientViewModel : ObservableObject, IRecipient<FieldChangedMessage>
    {
        [ObservableProperty]
        public string _nametmp;

        [ObservableProperty]
        public string _urltmp;

        [ObservableProperty]
        public ObservableCollection<ClientModel> _clients;

        IHttpClientFactory _factory;

        public ClientViewModel(IHttpClientFactory factory)
        {
            Clients = new ObservableCollection<ClientModel>();
            _factory = factory;
            Nametmp = "";
            Urltmp = "";
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand(CanExecute = nameof(CanAddClient))]
        public void AddClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(uriString: Urltmp) };
            Clients.Add(new ClientModel(client, Nametmp, Urltmp));
        }

        public void Receive(FieldChangedMessage message)
        {
            AddClientCommand.NotifyCanExecuteChanged();
        }

        private bool CanAddClient() => Nametmp != string.Empty && Urltmp != string.Empty;
    }
}
