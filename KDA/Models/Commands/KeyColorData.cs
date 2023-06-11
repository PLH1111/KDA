using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class KeyColorData
{
    public byte KeyIndex { get; set; }

    public string KeyIndexStr => $"#{KeyIndex}";

    public Key Key { get; set; }

    public string KeyStr => $"{Key}";

    public byte ColorR { get; set; }

    public string ColorRHex
    {
        get => ColorR.ToHex();
        set => ColorR = value.HexToByte();
    }

    public byte ColorG { get; set; }

    public string ColorGHex
    {
        get => ColorG.ToHex();
        set => ColorG = value.HexToByte();
    }


    public byte ColorB { get; set; }

    public string ColorBHex
    {
        get => ColorB.ToHex();
        set => ColorB = value.HexToByte();
    }

    public byte ColorA { get; set; }

    public string ColorAHex
    {
        get => ColorA.ToHex();
        set => ColorA = value.HexToByte();
    }

    public KeyColorData()
    {

    }

    public KeyColorData(byte no)
    {
        KeyIndex = no;
        Key = KeyIndexMap.Keys[no];
    }

    public KeyColorData(byte no, Key key)
    {
        KeyIndex = no;
        Key = key;
    }

    public KeyColorData(byte r, byte b, byte g, byte a)
    {
        ColorR = r;
        ColorB = b;
        ColorG = g;
        ColorA = a;
    }

    public void Clear()
    {
        ColorR = 0;
        ColorB = 0;
        ColorG = 0;
        ColorA = 0;
    }
}
