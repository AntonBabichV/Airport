using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.View.Dialogs;
namespace AirLineMVP.View.Menu
{
    public interface IMenuManager
    {
        IMenuItem TopMenu { get;  }
        IMenuItemFactory MenuItemFactory { get; }
        void StartMenuSession(IDialogManager dialogManager);
    }
}
