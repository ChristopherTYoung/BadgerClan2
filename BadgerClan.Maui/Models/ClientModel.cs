using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.Models
{
    public partial class ClientModel : ObservableObject
    {
        [ObservableProperty]
        public string _name;

        [ObservableProperty]
        public string _url;

        public HttpClient _client { get; }

        public ClientModel(HttpClient client, string name, string url) 
        {
            Name = name;
            _client = client;
            _url = url; 
        }


        [RelayCommand]
        public async Task Controls()
        {
            await Shell.Current.GoToAsync("client", new Dictionary<string, object>() { { "client", new ClientModel(_client, Name, Url) } });
        }

    }
}
