using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Bootloader;

[AddINotifyPropertyChangedInterface]
public class ConfigModel
{
    public ResponseCodes ResponseCode { get; set; } = ResponseCodes.TBA;


    public uint Adderss { get; set; }

    public string AdderssHex
    {
        get => Adderss.ToBytes().ToHex();
        set => Adderss = value.HexToBytes().ToUint();
    }

    public uint Version { get; set; }

    public string VersionHex
    {
        get => Version.ToBytes().ToHex();
        set => Version = value.HexToBytes().ToUint();
    }

    public ConfigModel()
    {

    }

    public ConfigModel(ResponseCodes responseCode, string adderssHex, string versionHex)
    {
        ResponseCode = responseCode;
        AdderssHex = adderssHex;
        VersionHex = versionHex;
    }

    public ConfigModel(ResponseCodes responseCode, uint adderss, uint version)
    {
        ResponseCode = responseCode;
        Adderss = adderss;
        Version = version;
    }
}
