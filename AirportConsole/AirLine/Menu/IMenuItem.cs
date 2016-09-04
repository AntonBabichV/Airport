using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public interface IMenuItem
    {
        string Name { get; set; }
        string Key { get; set; }
        MenuOperation Operation { get; set; }
        ComplicatedMenuOperation ComplicatedOperation { get; set; }
        MenuType Type { get; set; }
        IList<IMenuItem> SubMenus { get; set; }

    }
}
