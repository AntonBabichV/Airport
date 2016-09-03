using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public enum MenuType
    {
        Exit,
        LevelUp,
        MenuLevel,
        SimpleOperation,// Like delete
        OperationWithSubMenus, // Like Add passagers which means management tickets

    }

    public interface IMenuItem
    {
        string Name { get; set; }
        string Key { get; set; }
        MenuOperation Operation { get; set; }
        ComplicatedMenuOperation ComplicatedOperation { get; set; }
        MenuType Type { get; set; }
        IList<IMenuItem> SubMenus { get; set; }

    }

    public class MenuItem : IMenuItem
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public MenuOperation Operation { get; set; }
        public ComplicatedMenuOperation ComplicatedOperation { get; set; }
        public MenuType Type { get; set; }

        public IList<IMenuItem> SubMenus { get; set; }
    }
}
