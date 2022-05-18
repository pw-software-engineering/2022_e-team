using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Models.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        WaitingForPayment = 1,
        ToRealized = 2,
        Paid = 3,
        Prepared = 4,
        Delivered = 5,
        Finished = 6,
        Canceled = 7
    }
}
