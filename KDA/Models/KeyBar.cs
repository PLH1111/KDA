using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models
{
    public class KeyBar
    {
        public List<KeyModel> Keys01 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys02 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys03 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys04 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys05 { get; set; } = new List<KeyModel>();


        Random random = new Random();
        byte[] bytes = new byte[3];

        private Color RandomColor()
        {
            random.NextBytes(bytes);
            return Color.FromRgb(bytes[0], bytes[1], bytes[2]);
        }

        public void SetValue(double value)
        {
            if (value > 15)
            {
                foreach (var key in Keys01)
                {
                    key.AnimationColor = RandomColor();
                }
            }
            else
            {
                foreach (var key in Keys01)
                {
                    key.AnimationColor = default;
                }
            }

            if (value > 60)
            {
                foreach (var key in Keys02)
                {
                    key.AnimationColor = RandomColor();
                }
            }
            else
            {
                foreach (var key in Keys02)
                {
                    key.AnimationColor = default;
                }
            }

            if (value > 120)
            {
                foreach (var key in Keys03)
                {
                    key.AnimationColor = RandomColor();
                }
            }
            else
            {
                foreach (var key in Keys03)
                {
                    key.AnimationColor = default;
                }
            }

            if (value > 180)
            {
                foreach (var key in Keys04)
                {
                    key.AnimationColor = RandomColor();
                }
            }
            else
            {
                foreach (var key in Keys04)
                {
                    key.AnimationColor = default;
                }
            }

            if (value > 240)
            {
                foreach (var key in Keys05)
                {
                    key.AnimationColor = RandomColor();
                }
            }
            else
            {
                foreach (var key in Keys05)
                {
                    key.AnimationColor = default;
                }
            }
        }
    }
}