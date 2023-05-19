using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;

public enum KeyCommandNames
{
    Model,
    Sleep,
    Key_Macro,
    Key_RBG,
    Animation,
    Profile,
    RBG_Map,
    Language,
    Macro_Data,
    Profile_Data,
    BootUp,
    Flash_Data,
    Reset_Default,
}

public enum KeyCommandAcceess
{
    ReadWrite, WriteOnly, ReadOnly
}

public class KeyCommand
{
    public KeyCommandNames Name { get; }

    public byte SetCode { get; }

    public byte GetCode { get; }


    public KeyCommandAcceess Acceess { get; }


    public KeyCommand(KeyCommandNames name, byte setCode, byte getCode, KeyCommandAcceess acceess = KeyCommandAcceess.ReadWrite)
    {
        Name = name;
        SetCode = setCode;
        GetCode = getCode;
        Acceess = acceess;
    }

    public override string ToString()
    {
        return Name.ToString();
        //switch (Acceess)
        //{
        //    case KeyCommandAcceess.ReadWrite:
        //        return $"【{Name}】: Set - 0x{SetCode:X2}  Get - 0x{GetCode:X2}";
        //    case KeyCommandAcceess.WriteOnly:
        //        return $"【{Name}】: Set - 0x{SetCode:X2}";
        //    case KeyCommandAcceess.ReadOnly:
        //        return $"【{Name}】: Get - 0x{GetCode:X2}";
        //    default:
        //        return null;
        //}
    }
}
