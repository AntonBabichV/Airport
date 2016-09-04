using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public interface IMenuItemFactory
    {
        IMenuItem GetMenuItem();
        IMenuItem GetMenuItem(string name, string key, MenuType type, IList<IMenuItem> subMenus = null, MenuOperation operation = null, ComplicatedMenuOperation complicatedOperation = null);
    }
}
