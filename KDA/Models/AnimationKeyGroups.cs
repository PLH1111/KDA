using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;

public class AnimationKeyGroups : List<AnimationKeyGroup>
{
    public void SetAnimation()
    {
        foreach (var item in this)
        {
            item.SetAnimation();
        }
    }


    public List<KeyModel> GetKeyModels()
    {
        List<KeyModel> result = new();
        foreach (var item in this)
        {
            result.AddRange(item);
        }
        return result;
    }
}
