using System.Windows.Input;

namespace KDA.Models;

[AddINotifyPropertyChangedInterface]
public class KeyModel
{

    public Key Key { get; set; }

    public string KeyStr { get; set; }

    public bool IsPressed { get; set; }

    public KeyModel()
    {

    }

    public KeyModel(Key key, string keyStr = null)
    {
        Key = key;
        KeyStr = string.IsNullOrEmpty(keyStr) ? key.ToString() : keyStr;
    }
}
