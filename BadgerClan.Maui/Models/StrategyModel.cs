using BadgerClan.Maui;
using BadgerClan.Shared;
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
        [ObservableProperty]
        private StrategyType _stratType;

        private bool _stratUsed;

        public StrategyModel(StrategyType stratType)
        {
            StratType = stratType;
            if (StratType == StrategyType.MyStrategy)
                _stratUsed = true;
            else _stratUsed = false;

            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand(CanExecute = nameof(CanChangeStrategy))]
        public void ChangeStrategy()
        {
            _stratUsed = true;
            WeakReferenceMessenger.Default.Send(new StrategyChangedMessage() { StratType = StratType });
        }
        private bool CanChangeStrategy() => !_stratUsed;

        public void Receive(StrategyChangedMessage message)
        {
            if (message.StratType != StratType) 
                _stratUsed = false;
            ChangeStrategyCommand.NotifyCanExecuteChanged();
        }
    }
}
