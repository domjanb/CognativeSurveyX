using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace CognativeSurveyX.UWP
{
    public class UWPPlatformInfo : IPlatformInfo
    {
        EasClientDeviceInformation devInfo = new EasClientDeviceInformation();
        public string GetModell()
        {
            return String.Format("{0} {1}", devInfo.SystemManufacturer,devInfo.SystemProductName);

        }

        public string GetVersion()
        {
            return devInfo.OperatingSystem;
        }
    }
}
