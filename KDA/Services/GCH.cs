using HidLibrary;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KDA.Services;

public class GCH
{
    public const byte OutReportId = 0xE8;

    public const byte InReportId = 0xE9;

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


    public static bool SetUp()
    {
        if (!IsDeviceAvailable)
        {
            return false;
        }
        if (Device.Write(Setup))
        {
            return false;
        }
        return true;
    }

    public static bool WriteReport(HidReport report)
    {
        if (!SetUp())
        {
            return false;
        }
        Thread.Sleep(5);
        return Device.WriteReport(report);
    }


    public static HidReport ReadReport(int timeout)
    {
        if (!SetUp())
        {
            return null;
        }
        Thread.Sleep(5);
        return Device.ReadReport(timeout);
    }

    public static bool WriteCommand(byte cmd, byte[] data)
    {
        if (!SetUp())
        {
            return false;
        }

        if (data == null || data.Length == 0 || data.Length > 62)
        {
            return false;
        }

        Thread.Sleep(5);

        HidReport report = new(64)
        {
            ReportId = OutReportId,
            Data = data
        };
        report.Data[1] = cmd;
        Array.Copy(data, 0, report.Data, 2, data.Length);
        return Device.WriteReport(report);
    }


    public static byte[] ReadCommand(byte cmd, byte[] paras = null)
    {
        if (!SetUp())
        {
            return null;
        }
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = cmd;
        if (paras != null && paras.Length > 0)
        {
            Array.Copy(paras, 0, report.Data, 2, paras.Length);
        }
        Device.WriteReport(report);
        var inReport = Device.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 2 ||
            report.Data[1] != cmd)
        {
            return null;
        }
        var data = new byte[report.Data.Length - 2];
        Array.Copy(report.Data, 2, data, 0, data.Length);
        return data;
    }


    public static bool WriteReportSync(HidReport report)
    {
        if (!SetUp())
        {
            return false;
        }
        Thread.Sleep(5);
        return Device.WriteReportSync(report);
    }

    public static HidReport ReadReportSync(byte reportId)
    {
        if (!SetUp())
        {
            return null;
        }
        Thread.Sleep(5);
        return Device.ReadReportSync(reportId);
    }

    public static async Task<bool> WriteReportAsync(HidReport report)
    {
        if (!SetUp())
        {
            return false;
        }
        Thread.Sleep(5);
        return await Device.WriteReportAsync(report);
    }

    public static async Task<HidReport> ReadReportSync(int timeout)
    {
        if (!SetUp())
        {
            return null;
        }
        Thread.Sleep(5);
        return await Device.ReadReportAsync(timeout);
    }

}
