using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPDWin2XTUManager
{
    [JsonObject(MemberSerialization.OptIn)]
    public class XTUProfile
    {
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public double MinimumWatt;
        [JsonProperty]
        public double MaximumWatt;
        [JsonProperty]
        public int CPUUndervolt;
        [JsonProperty]
        public int GPUUndervolt;
        [JsonProperty]
        public ProfileImage ProfileImage;

        public XTUProfile()
        {
            Name = "NEW_PROFILE";
            MinimumWatt = 7.00;
            MaximumWatt = 15.00;
            CPUUndervolt = 0;
            GPUUndervolt = 0;
            ProfileImage = ProfileImage.Gaming;
        }
        
        public XTUProfile(string name, double minimumWatt, double maximumWatt, int cpuUndervolt, int gpuUndervolt, ProfileImage profileImage)
        {
            Name = name;
            MinimumWatt = minimumWatt;
            MaximumWatt = maximumWatt;
            CPUUndervolt = cpuUndervolt;
            GPUUndervolt = gpuUndervolt;
            ProfileImage = profileImage;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
