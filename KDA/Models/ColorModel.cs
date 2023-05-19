using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class ColorModel
{
    public byte Number { get; set; }

    public string NumberStr => $"#{Number}";

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

    public ColorModel() 
    { 

    }

    public ColorModel(byte no)
    {
        Number = no;
    }

    public ColorModel(byte r, byte b, byte g, byte a)
    {
        ColorR = r;
        ColorB = b;
        ColorG = g;
        ColorA = a;
    }
}
