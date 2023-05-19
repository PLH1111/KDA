using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDA.Services
{
    public class GCH
    {
        private static byte[] Setup = { 0x21, 0x09, 0x00, 0x02, 0x00, 0x00, 0x40, 0x00 };

        public static HidDevice Device { get; set; }

        public static bool IsDeviceConnect => Device != null && Device.IsConnected;

        public static bool IsDeviceAvailable
        {
            get
            {
                if (Device == null || Device.IsConnected == false)
                {
                    TianWeiToolsPro.Service.MsgBoxService.ShowError("Device is not connect!");
                    return false;
                }
                return true;
            }
        }


        public static bool WriteReport(HidReport report)
        {
            if (!IsDeviceAvailable)
            {
                return false;
            }
            if (Device.Write(Setup))
            {
                return false;
            }
            Thread.Sleep(5);
            return Device.WriteReport(report);
        }

        public static HidReport ReadReport(int timeout)
        {
            if (!IsDeviceAvailable)
            {
                return null;
            }
            if (Device.Write(Setup))
            {
                return null;
            }
            Thread.Sleep(5);
            return Device.ReadReport(timeout);
        }

        public static bool WriteReportSync(HidReport report)
        {
            if (!IsDeviceAvailable)
            {
                return false;
            }
            if (Device.Write(Setup))
            {
                return false;
            }
            Thread.Sleep(5);
            return Device.WriteReportSync(report);
        }

        public static HidReport ReadReportSync(byte reportId)
        {
            if (!IsDeviceAvailable)
            {
                return null;
            }
            if (Device.Write(Setup))
            {
                return null;
            }
            Thread.Sleep(5);
            return Device.ReadReportSync(reportId);
        }

        public static async Task<bool> WriteReportAsync(HidReport report)
        {
            if (!IsDeviceAvailable)
            {
                return false;
            }
            if (Device.Write(Setup))
            {
                return false;
            }
            Thread.Sleep(5);
            return await Device.WriteReportAsync(report);
        }

        public static async Task<HidReport> ReadReportSync(int timeout)
        {
            if (!IsDeviceAvailable)
            {
                return null;
            }
            if (Device.Write(Setup))
            {
                return null;
            }
            Thread.Sleep(5);
            return await Device.ReadReportAsync(timeout);
        }

    }
}
