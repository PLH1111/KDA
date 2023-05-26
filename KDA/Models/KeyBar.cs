using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models
{
    public class KeyBar
    {
        public List<KeyModel> Keys01 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys02 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys03 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys04 { get; set; } = new List<KeyModel>();

        public List<KeyModel> Keys05 { get; set; } = new List<KeyModel>();

        public void SetValue(double value)
        {
            if (value > 15)
            {
                foreach (var key in Keys01)
                {
                    key.IsPressed = true;
                }
            }
            else
            {
                foreach (var key in Keys01)
                {
                    key.IsPressed = false;
                }
            }

            if (value > 60)
            {
                foreach (var key in Keys02)
                {
                    key.IsPressed = true;
                }
            }
            else
            {
                foreach (var key in Keys02)
                {
                    key.IsPressed = false;
                }
            }

            if (value > 120)
            {
                foreach (var key in Keys03)
                {
                    key.IsPressed = true;
                }
            }
            else
            {
                foreach (var key in Keys03)
                {
                    key.IsPressed = false;
                }
            }

            if (value > 180)
            {
                foreach (var key in Keys04)
                {
                    key.IsPressed = true;
                }
            }
            else
            {
                foreach (var key in Keys04)
                {
                    key.IsPressed = false;
                }
            }

            if (value > 240)
            {
                foreach (var key in Keys05)
                {
                    key.IsPressed = true;
                }
            }
            else
            {
                foreach (var key in Keys05)
                {
                    key.IsPressed = false;
                }
            }
        }
    }
}
