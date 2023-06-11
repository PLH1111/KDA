using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;
public class KeyColorDataList : List<KeyColorData>
{
    public KeyColorDataList()
    {

    }

    public KeyColorDataList(byte mapIndex)
    {
        byte startIndex = (byte)(11 * mapIndex);
        for (byte i = startIndex; i < startIndex + 11; i++)
        {
            Add(new KeyColorData(i));
        }
    }

    public void ClearColorDatas()
    {
        foreach (var data in this)
        {
            data?.ResetColor();
        }
    }
}
