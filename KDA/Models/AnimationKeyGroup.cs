using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models;

public class AnimationKeyGroup : List<KeyModel>
{

    public Brush AnimationBrush { get; set; }

    public AnimationKeyGroup(Brush brush)
    {
        AnimationBrush = brush;
    }

    public void SetAnimation()
    {
        foreach (var item in this)
        {
            item.AnimationBrush = AnimationBrush;
            item.IsAnimation = true;
        }
    }

    public void ClearAnimation()
    {
        foreach (var item in this)
        {
            item.IsAnimation = false;
            item.AnimationBrush = null;
        }
    }
}
