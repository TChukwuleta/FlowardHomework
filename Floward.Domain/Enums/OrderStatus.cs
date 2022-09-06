using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Enums
{
    public enum OrderStatus
    {
        OrderCancelled = 1,
        OrderDelivered,
        OrderInTransit,
        OrderPaymentDue,
        OrderPickupAvailable,
        OrderProblem,
        OrderProcessing,
        OrderReturned
    }
}