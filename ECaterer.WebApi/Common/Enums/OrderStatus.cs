using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Enums
{
    public enum OrderStatus
    {
        Created,
        WaitingForPayment,
        ToRealized,
        Paid,
        Prepared,
        Delivered,
        Finished,
        Canceled
    }
}
