using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
[Serializable]
public class CustomColor
{
    public Color Color { get; set; }

    public CustomColor()
    {
    }

    public CustomColor(Color color)
    {
        Color = color;
    }

  
}
