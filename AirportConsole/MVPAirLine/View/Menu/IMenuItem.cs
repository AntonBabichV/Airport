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
        Action SimpleOperation { get;  }

        Func<OperationContentEventArgs, bool> StartNewContext { get;}
        Action<OperationContentEventArgs> OperationWithContext{ get;  }
        Action<OperationContentEventArgs> FinishContext { get;  }

        MenuType Type { get;  }
        IEnumerable<IMenuItem> SubMenus { get;  }

    }
}
