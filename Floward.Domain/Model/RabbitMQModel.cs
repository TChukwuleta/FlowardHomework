using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Model
{
    public class RabbitMQModel
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string AutomaticRecoveryEnabled { get; set; }
        public string RequestedHeartbeat { get; set; }
        public string Queue { get; set; }
        public string Exchange { get; set; }
    }
}
