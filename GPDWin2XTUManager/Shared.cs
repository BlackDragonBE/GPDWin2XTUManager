using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPDWin2XTUManager
{
    public static class Shared
    {
        public static readonly string SETTINGS_PATH = "Settings.json";
        public static readonly string XTU_PATH = @"C:\Program Files (x86)\Intel\Intel(R) Extreme Tuning Utility\Client\xtucli.exe";

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
    }


}
