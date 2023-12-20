using KDA.Models;
using KDA.Models.Commands;
using KDA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KDA
{
    /// <summary>
    /// KeyEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KeyEditWindow : Window
    {
        public KeyEditWindow()
        {
            InitializeComponent();
        }

        private byte keyIndex;

        private byte keyCode;

        public KeyEditWindow(KeyModel keyModel) : this()
        {
            Label1.Content = keyModel.KeyStr;

            keyIndex = (byte)KeyMap.GetKeyIndex(keyModel.Key);
        }

        public void Hook_OnKeyDown(object sender, KeyEventArgs e)
        {
            KeyInput.SetCurrentValue(TextBox.TextProperty, e.Key.ToString());

            keyCode = KeyMap.GetKeyValue(e.Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.UserKey, keyCode);

            ACH.SetKeyMacro(model);
        }
    }
}
