using BadgerClan.Maui;
using BadgerClan.Maui.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.Models
{
    public partial class StrategyModel : ObservableObject, IRecipient<StrategyChangedMessage>
    {
        HttpClient _client; 
        [ObservableProperty]
        public StrategyType _stratType;

        private bool _stratUsed;

        public StrategyModel(StrategyType stratType, HttpClient client)
        {
            _client = client;
            StratType = stratType;
            if (StratType == StrategyType.MyStrategy)
                _stratUsed = true;
            else _stratUsed = false;
        }

        [RelayCommand(CanExecute = nameof(CanChangeStrategy))]
        public async Task ChangeStrategy()
        {
            await _client.PostAsJsonAsync("/changestrategy", _stratType);
            _stratUsed = true;
            WeakReferenceMessenger.Default.Send(new StrategyChangedMessage());
        }
        private bool CanChangeStrategy() => !_stratUsed;

        public void Receive(StrategyChangedMessage message)
        {
            ChangeStrategyCommand.NotifyCanExecuteChanged();
        }
    }
}
