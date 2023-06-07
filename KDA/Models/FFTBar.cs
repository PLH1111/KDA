using System.Windows.Media;
using TianWeiToolsPro.Extensions;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class FFTBar
{

    private double heigth = 100;
    public double Height
    {
        get => heigth;
        set
        {
            heigth = value;
            if (heigth.InRange(0, 50))
            {
                Fill = Brushes.Green;
            }
            else if (heigth.InRange(50, 150))
            {
                Fill = Brushes.Lime;
            }
            else if (heigth.InRange(150, 250))
            {
                Fill = Brushes.Yellow;
            }
            else if (heigth.InRange(250, 400))
            {
                Fill = Brushes.Gold;
            }
            else
            {
                Fill = Brushes.Orange;
            }
        }
    }


    public Brush Fill { get; set; } = Brushes.Lime;

}
