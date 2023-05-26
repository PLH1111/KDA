using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Bootloader;

[AddINotifyPropertyChangedInterface]
public class FlashModel
{
    public ResponseCodes ResponseCode { get; set; } = ResponseCodes.TBA;


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


    public byte[] Data { get; set; } = new byte[32];

    public string DataHex
    {
        get => Data.ToHex(true);
        set => Data = value.HexToBytes();
    }

    public FlashModel()
    {

    }

    public FlashModel(ResponseCodes responseCode, byte[] data)
    {
        ResponseCode = responseCode;
        Data = data;
    }

    public FlashModel(ResponseCodes responseCode, uint adderss, byte[] data)
    {
        ResponseCode = responseCode;
        Adderss = adderss;
        Data = data;
    }

    public FlashModel(ResponseCodes responseCode, string adderssHex, string dataHex)
    {
        ResponseCode = responseCode;
        AdderssHex = adderssHex;
        DataHex = dataHex;
    }

}
