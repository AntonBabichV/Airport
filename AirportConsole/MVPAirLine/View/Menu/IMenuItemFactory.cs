using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View.Menu
{
    public interface IMenuItemFactory
    {
        IMenuItem GetMenuItem();
        IMenuItem GetMenuItem(string name, string key, MenuType type, IList<IMenuItem> subMenus = null, Action operation = null, Func<OperationContentEventArgs, bool> complicatedOperation = null);
    }
}
