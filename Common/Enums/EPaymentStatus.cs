using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum EPaymentStatus
    {
        Created = 1,
        Canceled = 2,
        InProcess = 3,
        Errored = 4,
        Processed = 5,
    }
}
