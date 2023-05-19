using HidLibrary;
using KDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace KDA.Services;

/// <summary>KeyBoard Command Helper </summary>
public class KBCH
{
    public const byte OutReportId = 0xE8;

    public const byte InReportId = 0xE9;

    /// <summary> Get device model name </summary>
    public static string GetModel()
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x80;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 16 ||
            report.Data[1] != 0x80)
        {
            return null;
        }
        var data = report.Data.Take(new Range(2, 15)).ToArray();
        return Encoding.ASCII.GetString(data);
    }

    #region  Sleep


    /// <summary>Set sleep time and lighting mode</summary>
    public static bool SetSleep(SleepModel model)
    {

        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x01;
        report.Data[2] = (byte)model.SleepTime;
        report.Data[3] = (byte)model.SleepMode;
        return GCH.WriteReport(report);
    }


    /// <summary> Get sleep time and lighting mode </summary>
    public static SleepModel GetSleep()
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x81;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 4 ||
            report.Data[1] != 0x81)
        {
            return null;
        }

        SleepModel model = new((SleepTimes)report.Data[2], (LightingModes)report.Data[3]);
        return model;
    }

    #endregion

    #region  Key Macro

    /// <summary> Set single key macro </summary>
    public static bool SetKeyMacro(KeyMacroModel macro)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x02;
        report.Data[2] = macro.KeyIndex;
        report.Data[3] = (byte)macro.KeyMode;
        report.Data[4] = macro.KeyCode;
        return GCH.WriteReport(report);
    }

    /// <summary> Get single key macro </summary>
    public static KeyMacroModel GetKeyMacro()
    {

        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x82;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 5 ||
            report.Data[1] != 0x82)
        {
            return null;
        }

        KeyMacroModel model = new(report.Data[2], (KeyModes)report.Data[3], report.Data[4]);
        return model;
    }

    #endregion

    #region  Key Color(RGB)

    /// <summary> Set single key RGB control </summary>

    public static bool SetKeyColor(KeyColorModel model)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x03;
        report.Data[2] = model.KeyIndex;
        report.Data[3] = model.ColorR;
        report.Data[4] = model.ColorG;
        report.Data[5] = model.ColorB;
        report.Data[6] = model.ColorA;
        return GCH.WriteReport(report);
    }

    /// <summary> Get single key RGB control </summary>
    public static KeyColorModel GetKeyColor(byte keyIndex)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x83;
        report.Data[2] = keyIndex;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 7 ||
            report.Data[1] != 0x83)
        {
            return null;
        }

        KeyColorModel model = new(report.Data[2], report.Data[3], report.Data[4], report.Data[5], report.Data[6]);
        return model;
    }

    #endregion

    #region Animation

    /// <summary> Set active animation </summary>
    public static bool SetAnimation(AnimationModel model)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x04;
        report.Data[2] = (byte)model.AnimationId;
        report.Data[3] = model.ColorR;
        report.Data[4] = model.ColorG;
        report.Data[5] = model.ColorB;
        report.Data[6] = model.ColorA;
        report.Data[7] = model.Speed;
        report.Data[8] = (byte)model.Display;
        report.Data[9] = (byte)model.Direction;
        return GCH.WriteReport(report);
    }

    /// <summary> Get active animation</summary>
    public static AnimationModel GetAnimation()
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x84;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 10 ||
            report.Data[1] != 0x84)
        {
            return null;
        }

        AnimationModel model = new((AnimationIds)report.Data[2], report.Data[3], report.Data[4], report.Data[5], report.Data[6],
            report.Data[7], (AnimationDisplays)report.Data[8], (AnimationDirections)report.Data[9]);
        return model;
    }

    #endregion

    #region Profile

    /// <summary>Set active profile </summary>
    public static bool SetProfile(ProfileModel model)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x05;
        report.Data[2] = model.Number;
        return GCH.WriteReport(report);
    }

    /// <summary> Get active profile </summary>
    public static ProfileModel GetProfile()
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x85;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 2||
            report.Data[1] != 0x85)
        {
            return null;
        }
        ProfileModel model = new(report.Data[2]);
        return model;
    }

    #endregion

    #region Language

    /// <summary> Set language mode </summary>
    public static bool SetLanguage(KeyBoardLanguages language)
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x07;
        report.Data[2] = (byte)language;
        return GCH.WriteReport(report);
    }

    /// <summary> Get language mode </summary>
    public static KeyBoardLanguages GetLanguage()
    {
        HidReport report = new(64)
        {
            ReportId = OutReportId,
        };
        report.Data[1] = 0x87;
        GCH.WriteReport(report);
        var inReport = GCH.ReadReport(50);
        if (inReport == null || report.ReportId != InReportId ||
            report.Data == null || report.Data.Length < 3 ||
            report.Data[1] != 0x87)
        {
            return KeyBoardLanguages.None;
        }

        return (KeyBoardLanguages)report.Data[2];
    }

    #endregion
}
