using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;
using PropertyChanged;

namespace KDA.Models.Commands
{
    [AddINotifyPropertyChangedInterface]
    public class ProfileData
    {
        public byte Number { get; set; }

        public string NumberStr => $"#{Number}";


        public byte KeyIndex { get; set; }
        public string KeyIndexRHex
        {
            get => KeyIndex.ToHex();
            set => KeyIndex = value.HexToByte();
        }


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


        public ProfileData()
        {
        }

        public ProfileData(byte no)
        {
            Number = no;
        }

        public ProfileData(byte number, byte index, byte code, KeyMarcoModes mode) : this(number)
        {
            Number = number;
            KeyIndex = index;
            Code = code;
            Mode = mode;
        }

    }
}