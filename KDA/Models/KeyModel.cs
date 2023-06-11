using System.Windows.Input;
using System.Windows.Media;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class KeyModel
{

    public Key Key { get; set; }

    public string KeyStr { get; set; }

    public bool IsKeyPressed
    {
        get;
        set;
    }


    private Color animationColor;
    public Color AnimationColor
    {
        get => animationColor;
        set
        {
            animationColor = value;
        }
    }


    public KeyModel()
    {

    }

    public KeyModel(Key key, string keyStr = null)
    {
        Key = key;
        KeyStr = string.IsNullOrEmpty(keyStr) ? key.ToString() : keyStr;
    }
}
