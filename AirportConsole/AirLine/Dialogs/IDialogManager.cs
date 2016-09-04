using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.Menu;
namespace AirLine.Dialogs
{
    public struct EnumType
    {
        public string Name;
        public string KeyValue;
        public int Value;
    }
    public interface IDialogManager
    {
        IMenuItem ShowMenuDialog(IEnumerable<IMenuItem> menuList);
        void ShowTextInfo(string info,bool askContinue = false);
        bool ReceiveDoubleValue(string nameOfValue, ref double enteredValue, bool allowedToMiss = false);
        bool ReceiveIntValue(string nameOfValue, ref int enteredValue, bool allowedToMiss = false, int minValue = 0, int maxValue = 100);
        bool ReceiveStatus(string name, EnumType[] statuses, ref int selectedValue, bool allowedToMiss = false);
        bool ReceiveText(string name, ref string enteredText, bool allowedToMiss = false);
        bool ReceiveDateTime(string name, ref DateTime enteredDate, bool allowedToMiss = false);
        bool ReceiveDate(string name, ref DateTime enteredDate, bool allowedToMiss = false);
        bool ReceiveTodayTime(string name, ref DateTime enteredTime, bool allowedToMiss = false);
    }
}
