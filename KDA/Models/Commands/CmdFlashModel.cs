using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class CmdFlashModel
{

    public uint Adderss { get; set; }

    public string AdderssHex
    {
        get => Adderss.ToBytes().ToHex();
        set => Adderss = value.HexToBytes().ToUint();
    }


    public ushort Count { get; set; } = 32;
    public string CountHex
    {
        get => Count.ToBytes().ToHex();
        set => Count = value.HexToBytes().ToUshort();
    }


    public byte[] Data { get; set; }=new byte[32];

    public string DataHex
    {
        get => Data.ToHex(true);
        set => Data = value.HexToBytes();
    }

    public CmdFlashModel()
    {

    }

    public CmdFlashModel(byte[] data)
    {
        Data = data;
    }

    public CmdFlashModel(uint adderss, byte[] data)
    {
        Adderss = adderss;
        Data = data;
    }

    public CmdFlashModel(string adderssHex, string dataHex)
    {
        AdderssHex = adderssHex;
        DataHex = dataHex;
    }

}
