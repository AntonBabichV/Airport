using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public delegate void MenuOperation();
    public delegate bool ComplicatedMenuOperation(IEnumerable<IMenuItem> subItems, OperationContentEventArgs currentContent);
}
