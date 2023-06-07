using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KDA.Models;

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
                key.AnimationBrush = Brushes.Lime;
                key.IsAnimation = true;
            }
        }
        else
        {
            foreach (var key in Keys01)
            {
                key.AnimationBrush = null;
                key.IsAnimation = false;
            }
        }

        if (value > 60)
        {
            foreach (var key in Keys02)
            {
                key.AnimationBrush = Brushes.Cyan;
                key.IsAnimation = true;
            }
        }
        else
        {
            foreach (var key in Keys02)
            {
                key.AnimationBrush = null;
                key.IsAnimation = false;
            }
        }

        if (value > 120)
        {
            foreach (var key in Keys03)
            {
                key.AnimationBrush = Brushes.Yellow;
                key.IsAnimation = true;
            }
        }
        else
        {
            foreach (var key in Keys03)
            {
                key.AnimationBrush = null;
                key.IsAnimation = false;
            }
        }

        if (value > 180)
        {
            foreach (var key in Keys04)
            {
                key.AnimationBrush = Brushes.Orange;
                key.IsAnimation = true;
            }
        }
        else
        {
            foreach (var key in Keys04)
            {
                key.AnimationBrush = null;
                key.IsAnimation = false;
            }
        }

        if (value > 240)
        {
            foreach (var key in Keys05)
            {
                key.AnimationBrush = Brushes.OrangeRed;
                key.IsAnimation = true;
            }
        }
        else
        {
            foreach (var key in Keys05)
            {
                key.AnimationBrush = null;
                key.IsAnimation = false;
            }
        }
    }
}
