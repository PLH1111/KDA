using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class ProfileModel
{
    public byte Number { get; set; }

    public string NumberHex
    {
        get => Number.ToHex();
        set => Number = value.HexToByte();
    }


    public ProfileModel()
    {

    }


    public ProfileModel(byte number)
    {
        Number = number;
    }

    public ProfileModel(string number)
    {
        NumberHex = number;
    }
}
