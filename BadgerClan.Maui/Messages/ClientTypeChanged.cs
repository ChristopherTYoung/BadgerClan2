using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.Messages
{
    public class ClientTypeChanged
    {
        public bool gRPC { get; }
        public ClientTypeChanged(bool _grpc)
        {
            gRPC = _grpc;
        }
    }
}
