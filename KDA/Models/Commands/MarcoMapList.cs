using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;

public class MarcoMapList : List<MarcoMap>
{
    public MarcoMapList()
    {
    }

    public MarcoMapList(byte count)
    {
        for (byte i = 1; i < count + 1; i++)
        {
            Add(new MarcoMap(i));
        }
    }
}
