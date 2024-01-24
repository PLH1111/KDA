using KDA.Models;
using KDA.Models.Commands;
using KDA.Services;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace KDA
{
    [AddINotifyPropertyChangedInterface]
    public class Macro
    {
        public string Name  { get; set; }
        public byte   Index { get; set; }

        public ObservableCollection<MacroContent> MacroContents { get; set; } = new ObservableCollection<MacroContent>();

        public Macro() { }

        public Macro(byte[] bytes)
        {
            if (bytes == null) return;

            if (bytes.Length < 4) return;

            Index = bytes[0];

            var length = bytes[1];

            for (byte i = 0; i < length; i++)
            {
                MacroContent content = new MacroContent(bytes[i + 2]);
                MacroContents.Add(content);
            }
           
        }

        public byte[] GetBytes()
        {
            return MacroContents.Select(p => p.KeyValue).ToArray();
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class MacroContent
    {
        public MacroContent(Key key)
        {
            Key = key;
        }

        public MacroContent(byte keyCode)
        {
            Key = KeyMap.GetKeyFormValue(keyCode);
        }

        public string KeyName => Key.ToString();

        public Key Key { get; }

        public byte KeyValue => KeyMap.GetKeyValue(Key);
    }

    [AddINotifyPropertyChangedInterface]
    public class MacroList
    {
        public ObservableCollection<Macro> macros { get; set; } = new ObservableCollection<Macro>();

        public MacroList() 
        {
            for (int i = 0; i < 64; i++)
            {
                macros.Add(new Macro());

                macros[i].Index = (byte)(i + 1);

                macros[i].Name = $"宏{i + 1}";
            }
        }

        public void Refersh()
        {
            for (int i = 0; i < 64; i++)
            {
                var macro = ACH.GetMarcoMap(i + 1);

                if (macro == null) return;

                macros[i] = macro;

                macros[i].Name = $"宏{i + 1}";
            }
        }
    }
}
