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
        Add(new CustomColor(Brushes.Lime));
        Add(new CustomColor(Brushes.Yellow));
        Add(new CustomColor(Brushes.Cyan));
    }

    

    public Brush[] GetBrushes()
    {
        return this.Select(x=>x.Color).ToArray();
    }
}
