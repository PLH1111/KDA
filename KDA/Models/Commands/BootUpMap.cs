using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;

public class BootUpMap
{
    public byte Number { get; set; }

    public BootUpDataList MapDatas { get; set; } = new(7);

    public BootUpMap()
    {

    }

    public BootUpMap(byte no)
    {
        Number = no;
    }

    public override string ToString()
    {
        return $"#{Number:X2}";
    }
}
