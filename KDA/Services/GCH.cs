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

    private static readonly byte[] setupFrame = { 0x21, 0x09, 0x00, 0x02, 0x00, 0x00, 0x40, 0x00 };

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



    public static bool WriteData(byte[] data)
    {
        Output.DataBuf = data;
        return Device.WriteOutput();
    }


    public static byte[] ReadData()
    {
        if (Device.ReadInput() == false)
        {
            return null;
        }
        return Input.DataBuf;
    }

    public static bool WriteCommand(byte cmd, byte[] data)
    {

        if (data == null || data.Length == 0 || data.Length > 62)
        {
            return false;
        }

        byte[] bytes = new byte[data.Length + 2];
        bytes[1] = cmd;
        Array.Copy(data, 0, bytes, 2, data.Length);
        return WriteData(bytes);
    }


    public static byte[] ReadCommand(byte cmd, byte[] paras = null)
    {
        byte[] bytesOut = null;
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
        byte[] bytesIn = ReadData();
        if (bytesIn == null || bytesIn.Length < 2 || bytesIn[1] != cmd)
        {
            return null;
        }
        var data = new byte[bytesIn.Length - 1];
        Array.Copy(bytesIn, 1, data, 0, data.Length);
        return data;
    }


}
