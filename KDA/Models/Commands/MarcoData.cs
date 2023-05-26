using System.Collections.Generic;
using System.Windows.Documents;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class MarcoData
{
    public byte Number { get; set; }

    public string NumberStr => $"#{Number}";

    public byte Code { get; set; }

    public string CodeHex
    {
        get => Code.ToHex();
        set => Code = value.HexToByte();
    }

    public static List<KeyMarcoModes> MarcoModes { get; set; } = EnumHelper.ToList<KeyMarcoModes>();

    public KeyMarcoModes Mode
    { 
        get; 
        set; 
    } = KeyMarcoModes.KeyMakeThenRelease;

    public byte Times { get; set; }


    public MarcoData()
    {

    }

    public MarcoData(byte no)
    {
        Number = no;
    }

    public MarcoData(byte number, byte code, byte times) : this(number)
    {
        Number = number;
        Code = code;
        Times = times;
    }
}
