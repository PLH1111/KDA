using CyUSB;
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

    private static readonly byte[] setupFrame = { 0x00, 0x09, 0x00, 0x02, 0x00, 0x00, 0x40, 0x00 };

    public static CyHidDevice Device { get; set; }

    public static CyHidReport Input => Device?.Inputs;

    public static CyHidReport Output => Device?.Outputs;



    public static bool IsDeviceConnect => Device != null && Device.RwAccessible;

    public static bool IsDeviceAvailable
    {
        get
        {
            if (Device == null || Device.RwAccessible == false)
            {
                MsgBoxService.ShowError("Device is not connect!");
                return false;
            }
            return true;
        }
    }

    public static bool SetUp()
    {
        Output.DataBuf = TianWeiToolsPro.Extensions.SerializerExtension.DeepCopyByBin(setupFrame);
        return Device.SetOutput(0x21);
    }


    public static bool WriteData(byte[] data)
    {
        if(Device==null)
        {
            return false;
        }
        Output.DataBuf = data;
        return Device.WriteOutput();
    }


    public static byte[] ReadData()
    {
        if (Device == null)
        {
            return null;
        }
        if (Device.ReadInput() == false)
        {
            return null;
        }
        return Input.DataBuf;
    }

    public static bool WriteCommand(byte cmd, byte[] data)
    {
        if (Device == null)
        {
            return false;
        }
        if (data == null || data.Length == 0 || data.Length > 62)
        {
            return false;
        }

        byte[] bytes = new byte[64];
        bytes[1] = cmd;
        Array.Copy(data, 0, bytes, 2, data.Length);
        return WriteData(bytes);
    }


    public static byte[] ReadCommand(byte cmd, byte[] paras = null)
    {
        if (Device == null)
        {
            return null;
        }
        //SetUp();
        byte[] bytesOut;
        if (paras == null)
        {
            bytesOut = new byte[2];
        }
        else
        {
            bytesOut = new byte[paras.Length + 2];
            Array.Copy(paras, 0, bytesOut, 2, paras.Length);
        }

        bytesOut[1] = cmd;

        WriteData(bytesOut);
        Thread.Sleep(5);
        byte[] bytesIn = ReadData();
        if (bytesIn == null || bytesIn.Length < 2 || bytesIn[1] != cmd)
        {
            return null;
        }
        var data = new byte[bytesIn.Length - 2];
        Array.Copy(bytesIn, 2, data, 0, data.Length);
        return data;
    }


}
