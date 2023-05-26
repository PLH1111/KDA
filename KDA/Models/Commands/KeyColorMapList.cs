using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;

public class KeyColorMapList : List<KeyColorMap>
{
    public KeyColorMapList()
    {
    }

    public KeyColorMapList(byte count)
    {
        for (byte i = 1; i <= count; i++)
        {
            this.Add(new KeyColorMap(i));
        }
    }
}
