using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands
{

public class ProfileMapList : List<ProfileMap>
{
    public ProfileMapList()
    {

    }

    public ProfileMapList(byte count)
    {
        for (byte i = 0; i < count; i++)
        {
            Add(new ProfileMap());
        }
    }
}

}
