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
    public byte Index { get; set; }

    public KeyColorDataList MapDatas { get; set; }

    public KeyColorMap()
    {

    }

    public KeyColorMap(byte no)
    {
        Index = no;
        MapDatas = new(no);
    }

    public override string ToString()
    {
        return $"#{Index:X2}";
    }
}
