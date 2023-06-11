using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models;

public class AnimationKeyGroup : List<KeyModel>
{

    public Color AnimationColor { get; set; }

    public AnimationKeyGroup(Color color)
    {
        AnimationColor = color;
    }

    public void SetAnimation()
    {
        foreach (var item in this)
        {
            item.AnimationColor = AnimationColor;
        }
    }
}
