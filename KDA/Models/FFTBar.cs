using System.Windows.Media;

namespace KDA.Models
{
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
                if (heigth < 90)
                {
                    Fill = Brushes.Green;
                }
                else if (heigth > 90 && heigth < 150)
                {
                    Fill = Brushes.Lime;
                }
                else if (heigth >150 && heigth < 240)
                {
                    Fill = Brushes.Yellow;
                }
                else
                {
                    Fill = Brushes.Orange;
                }
            }
        }


        public Brush Fill { get; set; } = Brushes.Lime;

    }
}
