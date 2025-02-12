using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Shared
{
    [DataContract]
    public class ChangeRequest
    {
        [DataMember(Order = 1)]
        public StrategyType StratType { get; set; }
    }

    [ServiceContract]
    public interface IChangeService
    {
        [OperationContract]
        public Task ChangeStrategy(ChangeRequest request);
    }
}
