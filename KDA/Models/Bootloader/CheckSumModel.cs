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
    public class CheckSumModel
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


        public uint CheckSum { get; set; }

        public string CheckSumHex
        {
            get => CheckSum.ToBytes().ToHex();
            set => CheckSum = value.HexToBytes().ToUint();
        }

        public CheckSumModel()
        {

        }

        public CheckSumModel(ResponseCodes responseCode, uint checkSum)
        {
            ResponseCode = responseCode;
            CheckSum = checkSum;
        }

        public CheckSumModel(ResponseCodes responseCode, uint adderss, uint size, uint checkSum)
        {
            ResponseCode = responseCode;
            Adderss = adderss;
            Size = size;
            CheckSum = checkSum;
        }

        public CheckSumModel(ResponseCodes responseCode, string responseCodeHex, string adderssHex, string sizeHex, string checkSumHex)
        {
            ResponseCode = responseCode;
            AdderssHex = adderssHex;
            SizeHex = sizeHex;
            CheckSumHex = checkSumHex;
        }
    }
}