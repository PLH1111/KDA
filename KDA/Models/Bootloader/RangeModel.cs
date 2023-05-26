using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Bootloader;

[AddINotifyPropertyChangedInterface]
public class RangeModel
{
    public ResponseCodes ResponseCode { get; set; } = ResponseCodes.TBA;


    public uint Adderss { get; set; }

    public string AdderssHex
    {
        get => Adderss.ToBytes().ToHex();
        set => Adderss = value.HexToBytes().ToUint();
    }

    public uint Size { get; set; }

    public string SizeHex
    {
        get => Size.ToBytes().ToHex();
        set => Size = value.HexToBytes().ToUint();
    }

    public byte[] RangeData => Adderss.ToBytes().Concat(Size.ToBytes()).ToArray();


    public RangeModel()
    {

    }

    public RangeModel(ResponseCodes responseCode, string adderssHex, string versionHex)
    {
        ResponseCode = responseCode;
        AdderssHex = adderssHex;
        SizeHex = versionHex;
    }

    public RangeModel(ResponseCodes responseCode, uint adderss, uint version)
    {
        ResponseCode = responseCode;
        Adderss = adderss;
        Size = version;
    }
}
