using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class KeyColorModel
{
    public byte KeyIndex { get; set; }

    public string KeyName
    {
        get => KeyIndex.ToHex();
        set => KeyIndex = value.HexToByte();
    }

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

    public KeyColorModel()
    {

    }

    public KeyColorModel(byte index, byte colorR, byte colorG, byte colorB, byte colorA)
    {
        KeyIndex = index;
        ColorR = colorR;
        ColorG = colorG;
        ColorB = colorB;
        ColorA = colorA;
    }
}
