using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.Menu
{
    public class MenuItem : IMenuItem
    {
        public string Name { get; set; }
        public string Key { get; set; }
        private MenuOperation _operation;
        public MenuOperation Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                if ((value!=null) &&(_complicatedOperation != null))
                    throw new Exception("Second operation can't be assigned");
                _operation = value;
            }
        }
        public ComplicatedMenuOperation _complicatedOperation;
        public ComplicatedMenuOperation ComplicatedOperation {
            get {
                return _complicatedOperation;
            } set {
                if ((value != null) && (_operation != null))
                    throw new Exception("Second operation can't be assigned");
                _complicatedOperation = value;
            }
        }
        public MenuType Type { get; set; }

        public IList<IMenuItem> SubMenus { get; set; }
    }
}
