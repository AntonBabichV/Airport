using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole.Menu
{
    //should have link to ChildLevel
    public enum MenuType
    {
        Exit,
        Operation
    }

    public interface IMenuItem
    {
        string Name { get; set; }
        string Key { get; set; }
        MenuOperation Operation { get; set; }
        MenuType Type { get; set; }
    }

    public class MenuItem : IMenuItem
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public MenuOperation Operation { get; set; }
        public MenuType Type { get; set; }
    }
}
