using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GPDWin2XTUManager
{
    public static class RegistryManager
    {
        public static bool LogonRegistryKeyExists()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Shared.RUN_AT_LOGON_PATH, true);
            return key.GetValue(Shared.APP_REG_KEY_VALUE) != null;
        }

        public static string GetLogonProfileKeyValue()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Shared.RUN_AT_LOGON_PATH, true);
            string[] valueArray = key.GetValue(Shared.APP_REG_KEY_VALUE).ToString().Split(' ');
            //MessageBox.Show(valueArray[valueArray.Length - 1]);//todo: debug, remove
            return valueArray[valueArray.Length-1]; // Get profile name by extracting name after last space
        }

        public static void AddLogonProfileKey(XTUProfile profile)
        {
            ClearLogonProfileKey();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Shared.RUN_AT_LOGON_PATH, true);
            key.SetValue(Shared.APP_REG_KEY_VALUE, '"' + Application.ExecutablePath + '"' + " " + profile.Name);
        }

        public static void ClearLogonProfileKey()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Shared.RUN_AT_LOGON_PATH, true);
            key.DeleteValue(Shared.APP_REG_KEY_VALUE, false);
        }
    }
}
