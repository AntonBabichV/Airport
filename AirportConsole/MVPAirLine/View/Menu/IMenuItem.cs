using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View.Menu
{
    public interface IMenuItem
    {
        string Name { get; set; }
        string Key { get; set; }
        Action Operation { get; set; }
        Func<OperationContentEventArgs, bool> ComplicatedOperation { get; set; }
        MenuType Type { get; set; }
        IList<IMenuItem> SubMenus { get; set; }

    }
}
