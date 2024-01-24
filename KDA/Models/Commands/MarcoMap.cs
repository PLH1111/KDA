using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace KDA.Models.Commands
{
    [AddINotifyPropertyChangedInterface]
    public class MarcoMap
    {
        public byte Number { get; set; }

        public byte Index { get; set; }

        public MarcoDataList MapDatas { get; set; } = new MarcoDataList(64);

        public MarcoMap()
        {

        }

        public MarcoMap(byte no)
        {
            Number = no;
        }

        public override string ToString()
        {
            return $"#{Number:D2}";
        }
    }
}