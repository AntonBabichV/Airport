using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.Dialogs;
namespace AirLine.Menu
{
    public interface IMenuManager
    {
        IMenuItem TopMenu { get; set; }
        IMenuItemFactory MenuItemFcatory { get; set; }
        void StartMenuSession(IDialogManager dialogManager);
    }
}
