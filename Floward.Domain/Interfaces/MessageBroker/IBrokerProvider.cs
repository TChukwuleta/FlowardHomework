using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Interfaces.MessageBroker
{
    public interface IBrokerProvider
    {
        IModel RabbitMQChannel { get; }
    }
}
