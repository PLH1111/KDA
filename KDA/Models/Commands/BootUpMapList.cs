using System.Collections.Generic;

namespace KDA.Models.Commands
{
public class BootUpMapList : List<BootUpMap>
{
    public BootUpMapList()
    {

    }


    public BootUpMapList(byte count)
    {
        for (byte i = 1; i <= count; i++)
        {
            Add(new BootUpMap(i));
        }
    }
}
}