using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View.Menu
{
    public class MenuItem : IMenuItem
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public MenuType Type { get; private set; }
        public IEnumerable<IMenuItem> SubMenus { get; private set; }

        public Action SimpleOperation { get; private set; }

        public  Func<OperationContentEventArgs, bool> StartNewContext { get; private set; }
        public Action<OperationContentEventArgs> OperationWithContext{ get; private set; }
        public Action<OperationContentEventArgs> FinishContext { get; private set; }
        private MenuItem()
        {

        }
        /*        Exit,
        LevelUp,
        MenuLevel,
        SimpleOperation,// Like delete
        OperationWithSubMenus, // Like Add passagers which means management tickets
        */
        public class MenuItemBuilder
        {
            public MenuItem BuildExit(string name, string key)
            {
                return new MenuItem() { Name = name, Key = key, Type = MenuType.Exit };
            }
            public MenuItem BuildLevelUp(string name, string key)
            {
                return new MenuItem() { Name = name, Key = key, Type = MenuType.LevelUp };
            }
            public MenuItem BuildMenuLevel(string name, string key, IEnumerable<IMenuItem> subMenus)
            {
                var menus= new List<IMenuItem>();
                menus.AddRange(subMenus);
                return new MenuItem() { Name = name, Key = key, Type = MenuType.MenuLevel, SubMenus = menus };
            }
            public MenuItem BuildSimple(string name, string key, Action simpleOperation)
            {
                return new MenuItem() { Name = name, Key = key, Type = MenuType.SimpleOperation, SimpleOperation = simpleOperation };
            }
            public MenuItem BuildSimpleWithContext(string name, string key, Action<OperationContentEventArgs> operationWithContext)
            {
                return new MenuItem() { Name = name, Key = key, Type = MenuType.SimpleOperation, OperationWithContext = operationWithContext };
            }
            public MenuItem BuildContextWithSubMenus(string name, string key, IEnumerable<IMenuItem> subMenus, Func<OperationContentEventArgs, bool> startNewContext,  Action<OperationContentEventArgs> finishContext)
            {
                var menus = new List<IMenuItem>();
                menus.AddRange(subMenus);
                return new MenuItem() { Name = name, Key = key, Type = MenuType.OperationWithSubMenus, SubMenus = menus , StartNewContext  = startNewContext, FinishContext = finishContext };
            }
        }
    }
}
