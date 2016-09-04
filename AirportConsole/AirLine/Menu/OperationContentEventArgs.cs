using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public class OperationContentEventArgs : EventArgs
    {
        public object ProcessedEntity { get; set; }
    }
}
