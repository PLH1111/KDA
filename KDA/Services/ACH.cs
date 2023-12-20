using HidLibrary;
using KDA.Models;
using KDA.Models.Commands;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using TianWeiToolsPro.Extensions;

namespace KDA.Services;

/// <summary>Application Command Helper </summary>
public class ACH
{

    const byte SetSleepCommand = 0x01;
    const byte SetKeyMacroCommand = 0x02;
    const byte SetKeyColorCommand = 0x03;
    const byte SetAnimationCommand = 0x04;
    const byte SetProfileCommand = 0x05;
    const byte SetColorMapCommand = 0x06;
    const byte SetLanguageCommand = 0x07;
    const byte SetMarcoMapCommand = 0x10;
    const byte ResetDefaultCommand = 0x0F;
    const byte SetProfileMapCommand = 0x11;
    const byte SetBootUpMapCommand = 0x12;

    const byte GetModelCommand = 0x80;
    const byte GetSleepCommand = 0x81;
    const byte GetKeyMacroCommand = 0x82;
    const byte GetKeyColorCommand = 0x83;
    const byte GetAnimationCommand = 0x84;
    const byte GetProfileCommand = 0x85;
    const byte GetColorMapCommand = 0x86;
    const byte GetLanguageCommand = 0x87;
    const byte GetMarcoMapCommand = 0x90;
    const byte GetProfileMapCommand = 0x91;
    const byte GetBootUpMapCommand = 0x92;


    
     public static bool ResetDefaultSetting()
    {
        byte[] bytes = new byte[1];
        bytes[0] = 0;
        return GCH.WriteCommand(ResetDefaultCommand, bytes);
    }
    /// <summary> Get device model name </summary>
    public static string GetModel()
    {
        byte[] bytes = GCH.ReadCommand(GetModelCommand);
        if (bytes == null)
        {
            return null;
        }
        return Encoding.ASCII.GetString(bytes);
    }

    #region  Sleep


    /// <summary>Set sleep time and lighting mode</summary>
    public static bool SetSleep(SleepModel model)
    {
        byte[] bytes = new byte[2];
        bytes[0] = (byte)(model.SleepTime);
        bytes[1] = (byte)(model.SleepMode);
        return GCH.WriteCommand(SetSleepCommand, bytes);
    }


    /// <summary> Get sleep time and lighting mode </summary>
    public static SleepModel GetSleep()
    {
        byte[] bytes = GCH.ReadCommand(GetSleepCommand);
        if (bytes == null || bytes.Length < 2)
        {
            return null;
        }
        else
        {
            SleepModel model = new((SleepTimes)bytes[1], (LightingModes)bytes[2]);
            return model;
        }
    }

    #endregion

    #region  Key Macro

    /// <summary> Set single key macro </summary>
    public static bool SetKeyMacro(KeyMacroModel model)
    {
        byte[] bytes = new byte[3];
        bytes[0] = model.KeyIndex;
        bytes[1] = (byte)(model.KeyMode);
        bytes[2] = (byte)(model.KeyCode);
        return GCH.WriteCommand(SetKeyMacroCommand, bytes);
    }

    /// <summary> Get single key macro </summary>
    public static KeyMacroModel GetKeyMacro(byte[] KeyIndex)
    {
        byte[] bytes = GCH.ReadCommand(GetKeyMacroCommand, KeyIndex);
        if (bytes == null || bytes.Length < 3)
        {
            return null;
        }
        else
        {
            KeyMacroModel model = new(bytes[1], (KeyModes)bytes[2], bytes[3]);
            return model;
        }
    }

    #endregion

    #region  Key Color(RGB)

    /// <summary> Set single key RGB control </summary>

    public static bool SetKeyColor(KeyColorModel model)
    {
        byte[] bytes = new byte[5];
        bytes[0] = model.KeyIndex;
        bytes[1] = model.ColorR;
        bytes[2] = model.ColorG;
        bytes[3] = model.ColorB;
        bytes[4] = model.ColorA;
        return GCH.WriteCommand(SetKeyColorCommand, bytes);
    }

    /// <summary> Get single key RGB control </summary>
    public static KeyColorModel GetKeyColor(byte keyIndex)
    {
        byte[] paras = new byte[] { keyIndex };
        byte[] bytes = GCH.ReadCommand(GetKeyColorCommand, paras);
        if (bytes == null || bytes.Length < 5)
        {
            return null;
        }
        else
        {
            KeyColorModel model = new(bytes[1], bytes[2], bytes[3], bytes[4], bytes[5]);
            return model;
        }
    }

    #endregion

    #region Animation

    /// <summary> Set active animation </summary>
    public static bool SetAnimation(AnimationModel model)
    {
        byte[] bytes = new byte[8];
        bytes[0] = (byte)model.AnimationId;
        bytes[1] = model.ColorR;
        bytes[2] = model.ColorG;
        bytes[3] = model.ColorB;
        bytes[4] = model.ColorA;
        bytes[5] = model.Speed;
        bytes[6] = (byte)model.Display;
        bytes[7] = (byte)model.Direction;
        return GCH.WriteCommand(SetAnimationCommand, bytes);
    }

    /// <summary> Get active animation</summary>
    public static AnimationModel GetAnimation()
    {
        byte[] bytes = GCH.ReadCommand(GetAnimationCommand);
        if (bytes == null || bytes.Length < 8)
        {
            return null;
        }
        else
        {
            AnimationModel model = new((AnimationIds)bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6],
                                       (AnimationDisplays)bytes[7], (AnimationDirections)bytes[8]);

            return model;
        }
    }

    #endregion

    #region Profile

    /// <summary>Set active profile </summary>
    public static bool SetProfile(ProfileModel model)
    {
        byte[] bytes = new byte[1];
        bytes[0] = model.Number;
        return GCH.WriteCommand(SetProfileCommand, bytes);
    }

    /// <summary> Get active profile </summary>
    public static ProfileModel GetProfile()
    {
        byte[] bytes = GCH.ReadCommand(GetProfileCommand);
        if (bytes == null || bytes.Length < 1)
        {
            return null;
        }
        else
        {
            ProfileModel model = new(bytes[0]);
            return model;
        }
    }

    #endregion

    #region Color Map

    /// <summary>Set active profile </summary>
    public static bool SetColorMap(KeyColorMap map)
    {
        byte[] bytes = new byte[46];
        bytes[0] = map.Index;
        bytes[1] = 0x00;
        for (byte i = 0; i < 11; i++)
        {
            bytes[(4 * i) + 2] = map.MapDatas[i].ColorR;
            bytes[(4 * i) + 3] = map.MapDatas[i].ColorG;
            bytes[(4 * i) + 4] = map.MapDatas[i].ColorB;
            bytes[(4 * i) + 5] = map.MapDatas[i].ColorA;
        }
        return GCH.WriteCommand(SetColorMapCommand, bytes);
    }

    /// <summary> Get active profile </summary>
    public static KeyColorMap GetColorMap()
    {
        byte[] bytes = GCH.ReadCommand(GetColorMapCommand);
        if (bytes == null || bytes.Length < 48)
        {
            return null;
        }
        else
        {
            KeyColorMap map = new(bytes[0]);
            for (byte i = 0; i < 11; i++)
            {
                map.MapDatas[i].ColorR = bytes[(4 * i) + 2];
                map.MapDatas[i].ColorG = bytes[(4 * i) + 3];
                map.MapDatas[i].ColorB = bytes[(4 * i) + 4];
                map.MapDatas[i].ColorA = bytes[(4 * i) + 5];
            }
            return map;
        }
    }


    #endregion

    #region Language

    /// <summary> Set language mode </summary>
    public static bool SetLanguage(KeyBoardLanguages language)
    {
        byte[] bytes = new byte[1];
        bytes[0] = (byte)language;
        return GCH.WriteCommand(SetLanguageCommand, bytes);
    }

    /// <summary> Get language mode </summary>
    public static KeyBoardLanguages GetLanguage()
    {
        byte[] bytes = GCH.ReadCommand(GetLanguageCommand);
        if (bytes == null || bytes.Length < 1)
        {
            return KeyBoardLanguages.None;
        }
        else
        {
            return (KeyBoardLanguages)bytes[0];
        }
    }

    #endregion

    #region Marco Map



    /// <summary>Set active profile </summary>
    public static bool SetMarcoMap(MarcoMap map)
    {
        byte[] bytes = new byte[34];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = 0xFF;
        }
        var str = map.MapDatas[0].Code.Replace(" ", "");
        bytes[0] = map.Number;
        bytes[1] = (byte)(bytes.Length - 2);
        
        for (int i = 0, j = 0; j < str.Length; i++, j += 2)                               //掐头去尾
        {
            bytes[2 + i] = (byte)Convert.ToInt32(str.Substring(j, 2), 16);                 //将截取的字符串转为16进制存储在数组中        
        }

        return GCH.WriteCommand(SetMarcoMapCommand, bytes);
    }

    /// <summary>Set active profile </summary>
    public static bool SetMarcoMap(Macro macro)
    {
        byte[] bytes = new byte[34];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = 0xFF;
        }
        
        bytes[0] = macro.Index;
        bytes[1] = (byte)(bytes.Length - 2);

        var vaildBytes = macro.GetBytes();

        Array.Copy(vaildBytes, 0, bytes, 2, vaildBytes.Length);
       
        return GCH.WriteCommand(SetMarcoMapCommand, bytes);
    }

    /// <summary> Get active profile </summary>
    public static Macro GetMarcoMap(int index)
    {
        byte[] paras = new byte[] { (byte)index };
        byte[] bytes = GCH.ReadCommand(GetMarcoMapCommand, paras);
        if (bytes == null || bytes.Length < 48)
        {
            return null;
        }
        else
        {
            Macro macro = new Macro(bytes);

            return macro;
        }
    }

    /// <summary> Get active profile </summary>
    public static MarcoMap GetMarcoMap(byte mapNo)
    {
        byte[] paras = new byte[] { mapNo };
        byte[] bytes = GCH.ReadCommand(GetMarcoMapCommand, paras);
        if (bytes == null || bytes.Length < 48)
        {
            return null;
        }
        else
        {
            MarcoMap map = new(bytes[0])
            {
                Index = bytes[1]
            };


            for (byte i = 0; i < bytes[2]; i++)
            {
                map.MapDatas[0].Code += bytes[3 + i].ToString("X2");
            }
            return map;
        }
    }


    #endregion

    #region Profile Map

    /// <summary> Set whole key profile configuration, based on keyboard model to define key numbers </summary>
    public static bool SetProfileMap(ProfileMap map)
    {
        byte[] bytes = new byte[10];
        bytes[0] = map.Number;
        bytes[1] = 0x01;
        bytes[2] = (byte)map.AnimationId;
        bytes[3] = map.ColorR;
        bytes[4] = map.ColorG;
        bytes[5] = map.ColorB;
        bytes[6] = map.ColorA;
        bytes[7] = map.Speed;
        bytes[8] = (byte)map.Display;
        bytes[9] = (byte)map.Direction;
        bool isWrite = GCH.WriteCommand(SetProfileMapCommand, bytes);
        if (!isWrite)
        {
            return false;
        }

        bytes = new byte[46];
        bytes[0] = map.Number;
        bytes[1] = map.Index;
        for (byte i = 0; i < map.MapDatas.Count; i++)
        {
            bytes[(4 * i) + 2] = map.MapDatas[i].KeyIndex;
            bytes[(4 * i) + 3] = (byte)map.MapDatas[i].Mode;
            bytes[(4 * i) + 4] = map.MapDatas[i].Code;
        }
        return GCH.WriteCommand(SetProfileMapCommand, bytes);
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
        byte[] bytes = new byte[44];
        bytes[0] = map.Number;
        bytes[1] = 0x00;

        for (byte i = 0; i < 7; i++)
        {
            bytes[(6 * i) + 2] = map.MapDatas[i].KeyIndex;
            bytes[(6 * i) + 3] = map.MapDatas[i].ColorR;
            bytes[(6 * i) + 4] = map.MapDatas[i].ColorG;
            bytes[(6 * i) + 5] = map.MapDatas[i].ColorB;
            bytes[(6 * i) + 6] = map.MapDatas[i].ColorA;
            bytes[(6 * i) + 7] = map.MapDatas[i].Times;
        }
        return GCH.WriteCommand(SetBootUpMapCommand, bytes);
    }

    /// <summary> Get whole key profile configuration, based on keyboard model to define key numbers</summary>
    public static BootUpMap GetBootUpMap(byte mapNo)
    {
        byte[] paras = new byte[] { mapNo };
        byte[] bytes = GCH.ReadCommand(GetBootUpMapCommand, paras);
        if (bytes == null || bytes.Length < 44)
        {
            return null;
        }
        else
        {
            BootUpMap map = new(bytes[0]);
            for (byte i = 0; i < 7; i++)
            {
                map.MapDatas[i].KeyIndex = bytes[(6 * i) + 2];
                map.MapDatas[i].ColorR = bytes[(6 * i) + 3];
                map.MapDatas[i].ColorG = bytes[(6 * i) + 4];
                map.MapDatas[i].ColorB = bytes[(6 * i) + 5];
                map.MapDatas[i].ColorA = bytes[(6 * i) + 6];
                map.MapDatas[i].Times = bytes[(6 * i) + 7];
            }
            return map;
        }
    }

    #endregion

}
