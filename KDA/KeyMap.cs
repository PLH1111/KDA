using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDA.Models
{
    public class KeyUnit
    {
        /// <summary>
        /// Microsoft key id
        /// </summary>
        public Key Key { get;}
        /// <summary>
        /// key index of a keyboard
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// key code
        /// </summary>
        public byte Value { get;}

        public string KeyStr => Key.ToString();

        public KeyUnit(Key key, int index, byte value)
        {
            Key = key;
            Index = index;
            Value = value;
        }


    }

    public static class KeyMap
    {
        public static List<KeyUnit> keyUnits { get; }

        private static Dictionary<string, KeyUnit> _dict;

        private static Dictionary<byte, KeyUnit> _codeDict;

        static KeyMap()
        {
            keyUnits = new List<KeyUnit>();
            keyUnits.Add(new KeyUnit(Key.Escape         , 0x00, 0x29));
            keyUnits.Add(new KeyUnit(Key.F1             , 0x02, 0x3A));
            keyUnits.Add(new KeyUnit(Key.F2             , 0x03, 0x3B));
            keyUnits.Add(new KeyUnit(Key.F3             , 0x04, 0x3C));
            keyUnits.Add(new KeyUnit(Key.F4             , 0x05, 0x3D));
            keyUnits.Add(new KeyUnit(Key.F5             , 0x06, 0x3E));
            keyUnits.Add(new KeyUnit(Key.F6             , 0x07, 0x3F));
            keyUnits.Add(new KeyUnit(Key.F7             , 0x08, 0x40));
            keyUnits.Add(new KeyUnit(Key.F8             , 0x09, 0x41));
            keyUnits.Add(new KeyUnit(Key.F9             , 0x0A, 0x42));
            keyUnits.Add(new KeyUnit(Key.F10            , 0x0B, 0x43));
            keyUnits.Add(new KeyUnit(Key.F11            , 0x0C, 0x44));
            keyUnits.Add(new KeyUnit(Key.F12            , 0x0D, 0x45));
            keyUnits.Add(new KeyUnit(Key.PrintScreen    , 0x0E, 0x46));
            keyUnits.Add(new KeyUnit(Key.Scroll         , 0x0F, 0x47));
            keyUnits.Add(new KeyUnit(Key.Pause          , 0x10, 0x48));
            keyUnits.Add(new KeyUnit(Key.VolumeMute     , 0x11, 0x7F));
            keyUnits.Add(new KeyUnit(Key.VolumeDown     , 0x12, 0x81));
            keyUnits.Add(new KeyUnit(Key.VolumeUp       , 0x13, 0x80));
            keyUnits.Add(new KeyUnit(Key.Oem3           , 0x15, 0x35));
            keyUnits.Add(new KeyUnit(Key.D1             , 0x16, 0x1E));
            keyUnits.Add(new KeyUnit(Key.D2             , 0x17, 0x1F));
            keyUnits.Add(new KeyUnit(Key.D3             , 0x18, 0x20));
            keyUnits.Add(new KeyUnit(Key.D4             , 0x19, 0x21));
            keyUnits.Add(new KeyUnit(Key.D5             , 0x1A, 0x22));
            keyUnits.Add(new KeyUnit(Key.D6             , 0x1B, 0x23));
            keyUnits.Add(new KeyUnit(Key.D7             , 0x1C, 0x24));
            keyUnits.Add(new KeyUnit(Key.D8             , 0x1D, 0x25));
            keyUnits.Add(new KeyUnit(Key.D9             , 0x1E, 0x26));
            keyUnits.Add(new KeyUnit(Key.D0             , 0x1F, 0x27));
            keyUnits.Add(new KeyUnit(Key.OemMinus       , 0x20, 0x2D));
            keyUnits.Add(new KeyUnit(Key.OemPlus        , 0x21, 0x2E));
            keyUnits.Add(new KeyUnit(Key.Back           , 0x22, 0x2A));
            keyUnits.Add(new KeyUnit(Key.Insert         , 0x23, 0x49));
            keyUnits.Add(new KeyUnit(Key.Home           , 0x24, 0x4A));
            keyUnits.Add(new KeyUnit(Key.PageUp         , 0x25, 0x4B));
            keyUnits.Add(new KeyUnit(Key.NumLock        , 0x26, 0x53));
            keyUnits.Add(new KeyUnit(Key.Divide         , 0x27, 0x54));
            keyUnits.Add(new KeyUnit(Key.Multiply       , 0x28, 0x55));
            keyUnits.Add(new KeyUnit(Key.Subtract       , 0x29, 0x56));
            keyUnits.Add(new KeyUnit(Key.Tab            , 0x2A, 0x2B));
            keyUnits.Add(new KeyUnit(Key.Q              , 0x2B, 0x14));
            keyUnits.Add(new KeyUnit(Key.W              , 0x2C, 0x1A));
            keyUnits.Add(new KeyUnit(Key.E              , 0x2D, 0x08));
            keyUnits.Add(new KeyUnit(Key.R              , 0x2E, 0x15));
            keyUnits.Add(new KeyUnit(Key.T              , 0x2F, 0x17));
            keyUnits.Add(new KeyUnit(Key.Y              , 0x30, 0x1C));
            keyUnits.Add(new KeyUnit(Key.U              , 0x31, 0x18));
            keyUnits.Add(new KeyUnit(Key.I              , 0x32, 0x0C));
            keyUnits.Add(new KeyUnit(Key.O              , 0x33, 0x12));
            keyUnits.Add(new KeyUnit(Key.P              , 0x34, 0x13));
            keyUnits.Add(new KeyUnit(Key.OemOpenBrackets, 0x35, 0x2F));
            keyUnits.Add(new KeyUnit(Key.Oem6           , 0x36, 0x30));
            keyUnits.Add(new KeyUnit(Key.Oem5           , 0x37, 0x31));
            keyUnits.Add(new KeyUnit(Key.Delete         , 0x38, 0x4C));
            keyUnits.Add(new KeyUnit(Key.End            , 0x39, 0x4D));
            keyUnits.Add(new KeyUnit(Key.Next           , 0x3A, 0x4E));
            keyUnits.Add(new KeyUnit(Key.NumPad7        , 0x3B, 0x5F));
            keyUnits.Add(new KeyUnit(Key.NumPad8        , 0x3C, 0x60));
            keyUnits.Add(new KeyUnit(Key.NumPad9        , 0x3D, 0x61));
            keyUnits.Add(new KeyUnit(Key.Add            , 0x3E, 0x57));
            keyUnits.Add(new KeyUnit(Key.Capital        , 0x3F, 0x39));
            keyUnits.Add(new KeyUnit(Key.A              , 0x40, 0x04));
            keyUnits.Add(new KeyUnit(Key.S              , 0x41, 0x16));
            keyUnits.Add(new KeyUnit(Key.D              , 0x42, 0x07));
            keyUnits.Add(new KeyUnit(Key.F              , 0x43, 0x09));
            keyUnits.Add(new KeyUnit(Key.G              , 0x44, 0x0A));
            keyUnits.Add(new KeyUnit(Key.H              , 0x45, 0x0B));
            keyUnits.Add(new KeyUnit(Key.J              , 0x46, 0x0D));
            keyUnits.Add(new KeyUnit(Key.K              , 0x47, 0x0E));
            keyUnits.Add(new KeyUnit(Key.L              , 0x48, 0x0F));
            keyUnits.Add(new KeyUnit(Key.Oem1           , 0x49, 0x33));
            keyUnits.Add(new KeyUnit(Key.OemQuotes      , 0x4A, 0x34));
            keyUnits.Add(new KeyUnit(Key.Enter          , 0x4C, 0x28));
            keyUnits.Add(new KeyUnit(Key.NumPad4        , 0x50, 0x5C));
            keyUnits.Add(new KeyUnit(Key.NumPad5        , 0x51, 0x5D));
            keyUnits.Add(new KeyUnit(Key.NumPad6        , 0x52, 0x5E));
            keyUnits.Add(new KeyUnit(Key.LeftShift      , 0x54, 0xE1));
            keyUnits.Add(new KeyUnit(Key.Z              , 0x55, 0x1D));
            keyUnits.Add(new KeyUnit(Key.X              , 0x56, 0x1B));
            keyUnits.Add(new KeyUnit(Key.C              , 0x57, 0x06));
            keyUnits.Add(new KeyUnit(Key.V              , 0x58, 0x19));
            keyUnits.Add(new KeyUnit(Key.B              , 0x59, 0x05));
            keyUnits.Add(new KeyUnit(Key.N              , 0x5A, 0x11));
            keyUnits.Add(new KeyUnit(Key.M              , 0x5B, 0x10));
            keyUnits.Add(new KeyUnit(Key.OemComma       , 0x5C, 0x36));
            keyUnits.Add(new KeyUnit(Key.OemPeriod      , 0x5D, 0x37));
            keyUnits.Add(new KeyUnit(Key.OemQuestion    , 0x5E, 0x38));
            keyUnits.Add(new KeyUnit(Key.RightShift     , 0x61, 0xE5));
            keyUnits.Add(new KeyUnit(Key.Up             , 0x63, 0x52));
            keyUnits.Add(new KeyUnit(Key.NumPad1        , 0x65, 0x59));
            keyUnits.Add(new KeyUnit(Key.NumPad2        , 0x66, 0x5A));
            keyUnits.Add(new KeyUnit(Key.NumPad3        , 0x67, 0x5B));
            //keyUnits.Add(new KeyUnit(Key.Return         , 0x68, 0x58));
            keyUnits.Add(new KeyUnit(Key.LeftCtrl       , 0x69, 0xE0));
            keyUnits.Add(new KeyUnit(Key.LWin           , 0x6A, 0xE3));
            keyUnits.Add(new KeyUnit(Key.LeftAlt        , 0x6B, 0xE2));
            keyUnits.Add(new KeyUnit(Key.Space          , 0x6E, 0x2C));
            keyUnits.Add(new KeyUnit(Key.RightAlt       , 0x72, 0xE6));
            keyUnits.Add(new KeyUnit(Key.RWin           , 0x73, 0xE7));
            keyUnits.Add(new KeyUnit(Key.FinalMode      , 0x74, 0xDE));// Fn
            keyUnits.Add(new KeyUnit(Key.RightCtrl      , 0x76, 0xE4));
            keyUnits.Add(new KeyUnit(Key.Left           , 0x77, 0x50));
            keyUnits.Add(new KeyUnit(Key.Down           , 0x78, 0x51));
            keyUnits.Add(new KeyUnit(Key.Right          , 0x79, 0x4F));
            keyUnits.Add(new KeyUnit(Key.NumPad0        , 0x7A, 0x62));
            keyUnits.Add(new KeyUnit(Key.Decimal        , 0x7C, 0x63));

            try
            {
                _dict = keyUnits.ToDictionary(x => x.KeyStr);

                _codeDict = keyUnits.ToDictionary(x => x.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static int GetKeyIndex(Key key)
        {
            if (_dict == null) return 0;
            if (!_dict.ContainsKey(key.ToString())) return 0;

            return _dict[key.ToString()].Index;
        }

        public static byte GetKeyIndex(string key)
        {
            if (_dict == null) return 0;
            if (!_dict.ContainsKey(key)) return 0;

            return (byte)_dict[key].Index;
        }

        public static byte GetKeyValue(Key key)
        {
            if (_dict == null) return 0xFF;
            if (!_dict.ContainsKey(key.ToString())) return 0XFF;

            return _dict[key.ToString()].Value;
        }

        public static byte GetKeyValue(string key)
        {
            if (_dict == null) return 0xFF;
            if (!_dict.ContainsKey(key)) return 0XFF;

            return _dict[key.ToString()].Value;
        }

        public static Key GetKeyFormValue(byte value)
        {
            if (_codeDict == null) return Key.None;
            if (!_codeDict.ContainsKey(value)) return Key.None;

            return _codeDict[value].Key;
        }

    }
}
