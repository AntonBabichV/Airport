using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View.Menu
{
    public class MenuItemFactory :IMenuItemFactory
    {
        public IMenuItem GetMenuItem()
        {
            return new MenuItem();
        }
        public IMenuItem GetMenuItem(string name, string key, MenuType type, IList<IMenuItem> subMenus = null, Action operation = null, Func<OperationContentEventArgs, bool> complicatedOperation = null)
        {
            return new MenuItem() { Name = name,Key = key,Type = type, SubMenus= subMenus,ComplicatedOperation = complicatedOperation,Operation = operation};
        }
    }
}
