using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models.Commands;

[AddINotifyPropertyChangedInterface]
public class KeyColorMap
{
    public byte Number { get; set; }

    public KeyColorDataList MapDatas { get; set; } = new(11);

    public KeyColorMap()
    {

    }

    public KeyColorMap(byte no)
    {
        Number = no;
    }

    public override string ToString()
    {
        return $"#{Number:X2}";
    }
}
