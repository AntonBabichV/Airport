using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View.Menu
{
    public class OperationContentEventArgs : EventArgs
    {
        public object ProcessedEntity { get; set; }
    }
    //Should be used  Action and Func with special types
    //public delegate void MenuOperation();
    //public delegate bool ComplicatedMenuOperation( OperationContentEventArgs currentContent);
}
