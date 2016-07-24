using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole.Menu
{

 


    public interface IMenuManager
    {
        IMenuItem Selection( IList<IMenuItem> menuLevel);
        IList<IMenuItem> MainMenu { get;  }
        //bool DoMenu(IMenuItem);

    }
    public class ConsoleMenuManager : IMenuManager
    {

        public IDialogManager DialogManager { get; set; }

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
      


    }
}
