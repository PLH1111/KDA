using HidLibrary;
using KDA.Models;
using KDA.Models.Commands;
using System;
using System.Linq;
using System.Text;

namespace KDA.Services;

/// <summary>KeyBoard Command Helper </summary>
public class KBCH
{
    public const byte OutReportId = 0xE8;

    public const byte InReportId = 0xE9;

    /// <summary> Get device model name </summary>
    public static string GetModel()
    {
        byte[] bytes = GCH.ReadCommand(0x80);
        return Encoding.ASCII.GetString(bytes);
    }

    #region  Sleep


    /// <summary>Set sleep time and lighting mode</summary>
    public static bool SetSleep(SleepModel model)
    {

        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x01;
        //report.Data[2] = (byte)model.SleepTime;
        //report.Data[3] = (byte)model.SleepMode;
        //return GCH.WriteReport(report);
        return true;
    }


    /// <summary> Get sleep time and lighting mode </summary>
    public static SleepModel GetSleep()
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x81;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 4 ||
        //    report.Data[1] != 0x81)
        //{
        //    return null;
        //}

        //SleepModel model = new((SleepTimes)report.Data[2], (LightingModes)report.Data[3]);
        //return model;
        return null;
    }

    #endregion

    #region  Key Macro

    /// <summary> Set single key macro </summary>
    public static bool SetKeyMacro(KeyMacroModel macro)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x02;
        //report.Data[2] = macro.KeyIndex;
        //report.Data[3] = (byte)macro.KeyMode;
        //report.Data[4] = macro.KeyCode;
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get single key macro </summary>
    public static KeyMacroModel GetKeyMacro()
    {

        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x82;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 5 ||
        //    report.Data[1] != 0x82)
        //{
        //    return null;
        //}

        //KeyMacroModel model = new(report.Data[2], (KeyModes)report.Data[3], report.Data[4]);
        //return model;
        return null;
    }

    #endregion

    #region  Key Color(RGB)

    /// <summary> Set single key RGB control </summary>

    public static bool SetKeyColor(KeyColorModel model)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x03;
        //report.Data[2] = model.KeyIndex;
        //report.Data[3] = model.ColorR;
        //report.Data[4] = model.ColorG;
        //report.Data[5] = model.ColorB;
        //report.Data[6] = model.ColorA;
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get single key RGB control </summary>
    public static KeyColorModel GetKeyColor(byte keyIndex)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x83;
        //report.Data[2] = keyIndex;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 7 ||
        //    report.Data[1] != 0x83)
        //{
        //    return null;
        //}

        //KeyColorModel model = new(report.Data[2], report.Data[3], report.Data[4], report.Data[5], report.Data[6]);
        //return model;
        return null;
    }

    #endregion

    #region Animation

    /// <summary> Set active animation </summary>
    public static bool SetAnimation(AnimationModel model)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x04;
        //report.Data[2] = (byte)model.AnimationId;
        //report.Data[3] = model.ColorR;
        //report.Data[4] = model.ColorG;
        //report.Data[5] = model.ColorB;
        //report.Data[6] = model.ColorA;
        //report.Data[7] = model.Speed;
        //report.Data[8] = (byte)model.Display;
        //report.Data[9] = (byte)model.Direction;
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get active animation</summary>
    public static AnimationModel GetAnimation()
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x84;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 10 ||
        //    report.Data[1] != 0x84)
        //{
        //    return null;
        //}

        //AnimationModel model = new((AnimationIds)report.Data[2], report.Data[3], report.Data[4], report.Data[5], report.Data[6],
        //    report.Data[7], (AnimationDisplays)report.Data[8], (AnimationDirections)report.Data[9]);
        //return model;
        return null;
    }

    #endregion

    #region Profile

    /// <summary>Set active profile </summary>
    public static bool SetProfile(ProfileModel model)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x05;
        //report.Data[2] = model.Number;
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get active profile </summary>
    public static ProfileModel GetProfile()
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x85;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 2 ||
        //    report.Data[1] != 0x85)
        //{
        //    return null;
        //}
        //ProfileModel model = new(report.Data[2]);
        //return model;
        return null;
    }

    #endregion

    #region Color Map

    /// <summary>Set active profile </summary>
    public static bool SetColorMap(KeyColorMap map)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x06;
        //report.Data[2] = map.Index;
        //report.Data[3] = 0x00;
        //for (byte i = 0; i < 11; i++)
        //{
        //    report.Data[(4 * i) + 4] = map.MapDatas[i].ColorR;
        //    report.Data[(4 * i) + 5] = map.MapDatas[i].ColorG;
        //    report.Data[(4 * i) + 6] = map.MapDatas[i].ColorB;
        //    report.Data[(4 * i) + 7] = map.MapDatas[i].ColorA;
        //}
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get active profile </summary>
    public static KeyColorMap GetColorMap()
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x86;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 48 ||
        //    report.Data[1] != 0x86)
        //{
        //    return null;
        //}
        //KeyColorMap map = new(report.Data[2]);
        //for (byte i = 0; i < 11; i++)
        //{
        //    map.MapDatas[i].ColorR = report.Data[(4 * i) + 4];
        //    map.MapDatas[i].ColorG = report.Data[(4 * i) + 5];
        //    map.MapDatas[i].ColorB = report.Data[(4 * i) + 6];
        //    map.MapDatas[i].ColorA = report.Data[(4 * i) + 7];
        //}
        //return map;
        return null;
    }


    #endregion

    #region Language

    /// <summary> Set language mode </summary>
    public static bool SetLanguage(KeyBoardLanguages language)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x07;
        //report.Data[2] = (byte)language;
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get language mode </summary>
    public static KeyBoardLanguages GetLanguage()
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x87;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 3 ||
        //    report.Data[1] != 0x87)
        //{
        //    return KeyBoardLanguages.None;
        //}

        //return (KeyBoardLanguages)report.Data[2];
        return KeyBoardLanguages.US;
    }

    #endregion

    #region Marco Map



    /// <summary>Set active profile </summary>
    public static bool SetMarcoMap(MarcoMap map)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x10;
        //report.Data[2] = map.Number;
        //report.Data[3] = map.Index;
        //for (byte i = 0; i < 11; i++)
        //{
        //    report.Data[(4 * i) + 4] = map.MapDatas[i].Code;
        //    report.Data[(4 * i) + 5] = (byte)map.MapDatas[i].Mode;
        //    report.Data[(4 * i) + 6] = map.MapDatas[i].Times;
        //}
        //return GCH.WriteReport(report);
        return true;
    }

    /// <summary> Get active profile </summary>
    public static MarcoMap GetMarcoMap(byte mapNo)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x90;
        //report.Data[2] = mapNo;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);
        //if (inReport == null || report.ReportId != InReportId ||
        //    report.Data == null || report.Data.Length < 48 ||
        //    report.Data[1] != 0x90)
        //{
        //    return null;
        //}
        //MarcoMap map = new(report.Data[2])
        //{
        //    Index = report.Data[3]
        //};
        //for (byte i = 0; i < 11; i++)
        //{
        //    map.MapDatas[i].Code = report.Data[(4 * i) + 4];
        //    map.MapDatas[i].Mode = (KeyMarcoModes)report.Data[(4 * i) + 5];
        //    map.MapDatas[i].Times = report.Data[(4 * i) + 6];
        //}
        //return map;
        return null;
    }


    #endregion

    #region Profile Map

    /// <summary> Set whole key profile configuration, based on keyboard model to define key numbers </summary>
    public static bool SetProfileMap(ProfileMap map)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x11;
        //report.Data[2] = map.Number;
        //report.Data[3] = 0x01;
        //report.Data[4] = (byte)map.AnimationId;
        //report.Data[5] = map.ColorR;
        //report.Data[6] = map.ColorG;
        //report.Data[7] = map.ColorB;
        //report.Data[8] = map.ColorA;
        //report.Data[9] = map.Speed;
        //report.Data[10] = (byte)map.Display;
        //report.Data[11] = (byte)map.Direction;

        //bool isWrite = GCH.WriteReport(report);

        //if (!isWrite)
        //{
        //    return false;
        //}

        //report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x11;
        //report.Data[2] = map.Number;
        //report.Data[3] = map.Index;
        //for (byte i = 0; i < map.MapDatas.Count; i++)
        //{
        //    report.Data[(4 * i) + 4] = map.MapDatas[i].KeyIndex;
        //    report.Data[(4 * i) + 5] = (byte)map.MapDatas[i].Mode;
        //    report.Data[(4 * i) + 6] = map.MapDatas[i].Code;
        //}
        //isWrite = GCH.WriteReport(report);
        //return isWrite;
        return true;
    }

    /// <summary> Get whole key profile configuration, based on keyboard model to define key numbers</summary>
    public static ProfileMap GetProfileMap(byte mapNo)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x91;
        //report.Data[1] = mapNo;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);

        //var inReport02 = GCH.ReadReport(50);
        //if (inReport == null
        //    || report.ReportId != InReportId
        //    || report.Data == null
        //    || report.Data.Length < 12
        //    || report.Data[1] != 0x91
        //    || inReport02 == null
        //    || inReport02.ReportId != InReportId
        //    || inReport02.Data == null
        //    || inReport02.Data.Length < 48
        //    || inReport02.Data[1] != 0x91)
        //{
        //    return null;
        //}

        //ProfileMap map = new()
        //{
        //    AnimationId = (AnimationIds)report.Data[4],
        //    ColorR = report.Data[5],
        //    ColorG = report.Data[6],
        //    ColorB = report.Data[7],
        //    ColorA = report.Data[8],
        //    Speed = report.Data[9],
        //    Display = (AnimationDisplays)report.Data[10],
        //    Direction = (AnimationDirections)report.Data[11],
        //};

        //for (byte i = 0; i < map.MapDatas.Count; i++)
        //{
        //    map.MapDatas[i].KeyIndex = report.Data[(4 * i) + 4];
        //    map.MapDatas[i].Mode = (KeyMarcoModes)report.Data[(4 * i) + 5];
        //    map.MapDatas[i].Code = report.Data[(4 * i) + 6]; ;
        //}

        //return map;
        return null;
    }

    #endregion

    #region BootUp Map

    /// <summary> Set whole key profile configuration, based on keyboard model to define key numbers </summary>
    public static bool SetBootUpMap(BootUpMap map)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x11;
        //report.Data[2] = map.Number;
        //report.Data[3] = 0x00;

        //for (byte i = 0; i < map.MapDatas.Count; i++)
        //{
        //    report.Data[(6 * i) + 4] = map.MapDatas[i].KeyIndex;
        //    report.Data[(6 * i) + 5] = map.MapDatas[i].ColorR;
        //    report.Data[(6 * i) + 6] = map.MapDatas[i].ColorG;
        //    report.Data[(6 * i) + 7] = map.MapDatas[i].ColorB;
        //    report.Data[(6 * i) + 8] = map.MapDatas[i].ColorA;
        //    report.Data[(6 * i) + 8] = map.MapDatas[i].Times;
        //}

        //bool isWrite = GCH.WriteReport(report);
        //return isWrite;
        return true;
    }

    /// <summary> Get whole key profile configuration, based on keyboard model to define key numbers</summary>
    public static BootUpMap GetBootUpMap(byte mapNo)
    {
        //HidReport report = new(64)
        //{
        //    ReportId = OutReportId,
        //};
        //report.Data[1] = 0x92;
        //report.Data[1] = mapNo;
        //GCH.WriteReport(report);
        //var inReport = GCH.ReadReport(50);

        //if (inReport == null
        //    || report.ReportId != InReportId
        //    || report.Data == null
        //    || report.Data.Length < 46
        //    || report.Data[1] != 0x91)
        //{
        //    return null;
        //}

        //BootUpMap map = new()
        //{
        //    Number = report.Data[2],
        //};

        //for (byte i = 0; i < map.MapDatas.Count; i++)
        //{
        //    map.MapDatas[i].KeyIndex = report.Data[(6 * i) + 4];
        //    map.MapDatas[i].ColorR = report.Data[(6 * i) + 5];
        //    map.MapDatas[i].ColorG = report.Data[(6 * i) + 6];
        //    map.MapDatas[i].ColorB = report.Data[(6 * i) + 7];
        //    map.MapDatas[i].ColorA = report.Data[(6 * i) + 8];
        //    map.MapDatas[i].Times = report.Data[(6 * i) + 9];
        //}

        //return map;
        return null;
    }

    #endregion

}
