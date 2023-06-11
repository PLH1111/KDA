using HidLibrary;
using KDA.Models;
using KDA.Models.Bootloader;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Services;


/// <summary>
/// Bootloader Command Helper
/// </summary>
public class BCH
{

    private const byte getConfigCommand = 0xE0;

    private const byte setRangeCommand = 0x61;

    private const byte getRangeCommand = 0xE1;

    private const byte writeFlashCommand = 0x62;

    private const byte readFlashCommand = 0xE2;

    private const byte readCheckSumCommand = 0xE3;

    private const byte runAppCommand = 0x68;

    public static ConfigModel GetConfig()
    {
        byte[] bytes = GCH.ReadCommand(getConfigCommand);
        if (bytes == null || bytes.Length < 9)
        {
            return null;
        }
        var config = new ConfigModel((ResponseCodes)bytes[0], bytes.ToUint(1), bytes.ToUint(5));
        return config;
    }

    public static bool SetRange(RangeModel model)
    {
        if (model == null)
        {
            return false;
        }
        if (GCH.WriteCommand(setRangeCommand, model.RangeData) == false)
        {
            return false;
        }
        byte[] rsp = GCH.ReadCommand(setRangeCommand);
        return rsp != null && rsp.Length > 0 && rsp[0] == 0x00;
    }

    public static RangeModel GetRange()
    {
        byte[] bytes = GCH.ReadCommand(getRangeCommand);
        if (bytes == null || bytes.Length < 9)
        {
            return null;
        }
        var range = new RangeModel((ResponseCodes)bytes[0], bytes.ToUint(1), bytes.ToUint(5));
        return range;
    }


    public static bool WriteFlash(FlashModel model)
    {
        if (model == null)
        {
            return false;
        }
        var data = model.Adderss.ToBytes().Concat(model.Count.ToBytes()).Concat(model.Data).ToArray();
        if (GCH.WriteCommand(writeFlashCommand, data) == false)
        {
            return false;
        }
        byte[] rsp = GCH.ReadCommand(writeFlashCommand);
        return rsp != null && rsp.Length > 0 && rsp[0] == 0x00;
    }

    public static FlashModel ReadFlash(FlashModel model)
    {
        var paras = model.Adderss.ToBytes().Concat(model.Count.ToBytes()).ToArray();
        byte[] bytes = GCH.ReadCommand(readFlashCommand, paras);
        if (bytes == null || bytes.Length < model.Count + 8)
        {
            return null;
        }
        var data = bytes.Take(new Range(8, 8 + model.Count)).ToArray();
        var range = new FlashModel((ResponseCodes)bytes[0], data);
        return range;
    }

    public static CheckSumModel GetCheckSum(CheckSumModel model)
    {
        var paras=model.Adderss.ToBytes().Concat(model.Size.ToBytes()).ToArray();   
        byte[] bytes = GCH.ReadCommand(readCheckSumCommand,paras);
        if (bytes == null || bytes.Length < 7)
        {
            return null;
        }
        var data = bytes.Take(new Range(3, 7)).ToArray().ToUint();
        var checkSum = new CheckSumModel((ResponseCodes)bytes[0], data);
        return checkSum;
    }


    public static void RunApp(uint addr)
    {
        var paras = addr.ToBytes().Reverse().ToArray();
        GCH.WriteCommand(runAppCommand, paras);
    }

}
