using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models;

[Serializable]
public class CustomColors : ObservableCollection<CustomColor>
{
    public CustomColors()
    {
        Add(new CustomColor(Colors.Red));
        Add(new CustomColor(Colors.Lime));
        Add(new CustomColor(Colors.Blue));
    }



    public Color[] GetBrushes()
    {
        Color[] colors = new Color[Count + 1];
        Color[] temp = this.Select(x => x.Color).ToArray();
        for (int i = 1; i < temp.Length + 1; i++)
        {
            colors[i] = temp[i - 1];
        }
        return colors;
    }
}
