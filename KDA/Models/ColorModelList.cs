using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;
public class ColorModelList : List<ColorModel>
{
    public ColorModelList()
    {

    }

    public ColorModelList(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Add(new ColorModel((byte)(i + 1)));
        }
    }
}
