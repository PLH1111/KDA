using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class KeyColorMap
{
    public byte Number { get; set; }

    public ColorModelList ColorModelList { get; set; }=new ColorModelList(10);

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
