using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models;


public enum KeyModes
{
    NormalKey = 0x01,
    UserKey,
    MacroKey,
}


[AddINotifyPropertyChangedInterface]
public class KeyMacroModel
{
    public byte KeyIndex { get; set; }

    public string KeyName
    {
        get => KeyIndex.ToHex();
        set => KeyIndex = value.HexToByte();
    }

    public KeyModes KeyMode { get; set; } = KeyModes.NormalKey;

    public byte KeyCode { get; set; }

    public string KeyCodeHex
    {
        get => KeyCode.ToHex();
        set => KeyCode = value.HexToByte();
    }

    public KeyMacroModel()
    {

    }

    public KeyMacroModel(byte keyIndex, KeyModes keyMode, byte keyCode)
    {
        KeyIndex = keyIndex;
        KeyMode = keyMode;
        KeyCode = keyCode;
    }
}
