using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TianWeiToolsPro.Service;

namespace KDA.Models
{
    public class AnimationKeyModel : List<KeyModel>
    {

        public AnimationKeyModel()
        {

        }


        public List<AnimationKeyGroups> GetSetAnimationGroupsList(int colums, Brush[] brushes)
        {
            if (colums <= 0 || brushes == null || brushes.Length == 0)
            {
                return null;
            }

            IEnumerable<KeyModel> keyModels = null;
            int rest = Count % (colums * brushes.Length * 2);
            if (rest == 0)
            {
                keyModels = this;
            }
            else
            {
                keyModels = this.Concat(this.Take(rest + colums * 2).ToList());
            }

            var list = new List<AnimationKeyGroups>();

            for (int i = 0; i < Count / 2; i++)
            {
                AnimationKeyGroups groups = new();
                for (int j = 0; j < brushes.Length; j++)
                {
                    AnimationKeyGroup group = new(brushes[j]);
                    int skipIndex = (2 * i) + (colums * 2 * j);
                    group.AddRange(keyModels.Skip(skipIndex).Take(colums * 2));
                    groups.Add(group);
                }
                list.Add(groups);
            }

            return list;
        }
    }
}
