using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class ProfileMap
{

    public byte Number { get; set; }

    public byte Index { get; set; }
    public string IndexRHex
    {
        get => Index.ToHex();
        set => Index = value.HexToByte();
    }

    public AnimationIds AnimationId { get; set; }

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

    public byte Speed { get; set; }

    public string SpeedHex
    {
        get => Speed.ToHex();
        set => Speed = value.HexToByte();
    }

    public AnimationDisplays Display { get; set; }


    public AnimationDirections Direction { get; set; } = AnimationDirections.LeftToRight;



    public override string ToString()
    {
        return $"#{Number:X2}";
    }



    public ProfileDataList MapDatas { get; set; }=new ProfileDataList(11);


}
