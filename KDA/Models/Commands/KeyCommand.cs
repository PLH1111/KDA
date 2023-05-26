using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;



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
