using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPDWin2XTUManager.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace GPDWin2XTUManager
{
    public static class Shared
    {
        public static readonly decimal VERSION = 1.04m;
        public static readonly string SETTINGS_PATH = "Settings.json";
        public static readonly string RUN_AT_LOGON_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static readonly string APP_REG_KEY_VALUE = "GPDWin2XTUManager";
        public static readonly string XTU_PATH = @"C:\Program Files (x86)\Intel\Intel(R) Extreme Tuning Utility\Client\xtucli.exe";
        public static Dictionary<ProfileImage, Bitmap> IMAGE_RESOURCES_DICTIONARY = new Dictionary<ProfileImage, Bitmap>();


        public static void SaveProfilesToDisk(List<XTUProfile> profiles)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(Shared.SETTINGS_PATH))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, profiles);
            }
        }

        public static void PrepareImages()
        {
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.Battery, Resources.Battery);
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.GPD, Resources.GPD);
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.Gaming, Resources.Controller);
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.Performance, Resources.Speedometer);
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.Gamepad, Resources.Gamepad);
            IMAGE_RESOURCES_DICTIONARY.Add(ProfileImage.Dpad, Resources.Dpad);
        }
    }


}
