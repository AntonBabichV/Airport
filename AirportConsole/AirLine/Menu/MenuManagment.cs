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
        IMenuItem Selection( IList<IMenuItem> menuLevel);
        IMenuItem Selection(IMenuItem menu);
        IList<IMenuItem> MainMenu { get;  }

        IMenuItem TopMenu { get; set; }
        IMenuItemFactory MenuItemFcatory { get; set; }
        //bool DoMenu(IMenuItem);

    }
    public class ConsoleMenuManager : IMenuManager
    {

        public IDialogManager DialogManager { get; set; }
        public IMenuItem TopMenu { get; set; }
        public IMenuItemFactory MenuItemFcatory { get; set; }
        public ConsoleMenuManager()
        {

            _mainMenu = new List<IMenuItem>();
        }
        /// <summary>
        /// Contains first level of menu()without multi levels
        /// </summary>
        private List<IMenuItem> _mainMenu;
        public IList<IMenuItem> MainMenu
        {
            get
            {
                return _mainMenu;
            }

        }
        /// <summary>
        /// Show console menues list and back the selected
        /// </summary>
        /// <param name="menuLevel"></param>
        /// <returns></returns>
        public IMenuItem Selection(IList<IMenuItem> menuLevel)
        {
           return DialogManager.ShowMenuDialog(menuLevel);
        }

        public IMenuItem Selection(IMenuItem menu)
        {
            if (menu.Type == MenuType.MenuLevel)
                return DialogManager.ShowMenuDialog(menu.SubMenus);
            else
                throw new Exception("Wrong meu type!");
        }



    }
}
