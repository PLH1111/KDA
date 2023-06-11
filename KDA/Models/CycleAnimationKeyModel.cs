using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TianWeiToolsPro.Service;

namespace KDA.Models;

public class CycleAnimationKeyModel : List<KeyModel>
{

    public CycleAnimationKeyModel()
    {

    }


    public List<AnimationKeyGroups> GetSetAnimationGroupsList(int colums, Color[] brushes)
    {
        if (colums <= 0 || brushes == null || brushes.Length == 0)
        {
            return null;
        }

        int keyCountPerCycle = colums * brushes.Length * 2;
        List<KeyModel> keyModels = this.Concat(this.Take(keyCountPerCycle).ToList()).ToList();

        var list = new List<AnimationKeyGroups>();

        for (int i = 0; i < Count / 2; i += 1)
        {
            AnimationKeyGroups groups = new();
            for (int j = 0; j < brushes.Length; j++)
            {
                AnimationKeyGroup group = new(brushes[j]);
                int index = (2 * i) + (colums * 2 * j);
                group.AddRange(keyModels.GetRange(index, colums * 2));
                groups.Add(group);
            }
            list.Add(groups);
        }
        return list;
    }
}
