using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.Dialogs;
namespace AirLine.Menu
{

    public class ConsoleMenuManager : IMenuManager
    {
        public IDialogManager DialogManager { get; set; }
        public IMenuItem TopMenu { get; set; }
        public IMenuItemFactory MenuItemFactory { get; set; }

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
        private IDialogManager _dialogManager;
        private IMenuItem MenuSession(IMenuItem menu, OperationContentEventArgs currentContent = null)
        {

            IMenuItem subMenu;
            switch (menu.Type)
            {
                case MenuType.MenuLevel:
                    subMenu = null;
                    do
                    {

                        _dialogManager.ClearScreen();
                        subMenu = Selection(menu.SubMenus);
                        if ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit))
                            MenuSession(subMenu, currentContent);
                    } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));
                    return subMenu;
                case MenuType.SimpleOperation:
                    if (menu.Operation != null)
                        menu.Operation();
                    else
                        menu.ComplicatedOperation(currentContent);// current content won't be changed
                    return menu;
                    break;
                case MenuType.Exit:
                    return menu;
                    break;
                case MenuType.LevelUp:
                    return menu;
                    break;

                case MenuType.OperationWithSubMenus:
                    if (currentContent == null)
                        currentContent = new OperationContentEventArgs();

                    var currentProcessedEntity = currentContent.ProcessedEntity;
                    // current content will be changed
                    if (menu.ComplicatedOperation(currentContent))
                    {
                        subMenu = null;
                        do
                        {
                            _dialogManager.ClearScreen();
                            _dialogManager.ShowTextInfo("You are on stage management this entity:" + currentContent.ProcessedEntity.ToString());
                            subMenu = Selection(menu.SubMenus);
                            MenuSession(subMenu, currentContent);
                        } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));

                        if ((currentProcessedEntity != currentContent.ProcessedEntity) && (currentProcessedEntity != null))
                        {
                            currentContent.ProcessedEntity = currentProcessedEntity;
                        }
                        return menu;
                    }
                    else
                    {
                        return menu;
                    }

                    break;
            }
            return menu;

        }
      
        public void StartMenuSession(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
            IMenuItem resultMenu = null;
            do
            {
                resultMenu = MenuSession(TopMenu);
            } while (resultMenu.Type != MenuType.Exit);
        }


    }
}
