using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Bootloader
{
    [AddINotifyPropertyChangedInterface]
    public class RunModel
    {
        public ResponseCodes ResponseCode { get; set; } = ResponseCodes.TBA;


        public uint Adderss { get; set; }

        public string AdderssHex
        {
            get => Adderss.ToBytes().ToHex();
            set => Adderss = value.HexToBytes().ToUint();
        }



        public RunModel()
        {

        }

        public RunModel(ResponseCodes responseCode, string adderssHex, string versionHex)
        {
            ResponseCode = responseCode;
            AdderssHex = adderssHex;
        }

        public RunModel(ResponseCodes responseCode, uint adderss, uint version)
        {
            ResponseCode = responseCode;
            Adderss = adderss;
        }
    }
}