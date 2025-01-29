using BadgerClan.Maui.Messages;
using BadgerClan.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.ViewModels
{
    public class MainPageViewModel : ObservableObject, IRecipient<StrategyChangedMessage>
    {
        HttpClient _client;
        public ObservableCollection<StrategyModel> Strategies { get; set; }
        public MainPageViewModel(HttpClient client)
        {
            _client = client;
            Strategies = new ObservableCollection<StrategyModel>()
            {
                new StrategyModel(StrategyType.MyStrategy, _client),
                new StrategyModel(StrategyType.OtherStrategy, _client)
            };
            WeakReferenceMessenger.Default.Register(this);
        }

        public void Receive(StrategyChangedMessage message)
        {
            OnPropertyChanged();
        }
    }
}
