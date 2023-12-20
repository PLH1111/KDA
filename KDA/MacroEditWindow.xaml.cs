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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KDA
{
    /// <summary>
    /// KeyEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MacroEditWindow : Window
    {
        public MacroEditWindow()
        {
            InitializeComponent();
        }

        private byte keyIndex;

        public MacroEditWindow(KeyModel keyModel, MacroList macroList) : this()
        {
            Label1.Content = keyModel.KeyStr;

            Combox1.ItemsSource = macroList.macros.Select(p => p.Name).ToList();

            Combox1.SelectedIndex = 0;

            keyIndex = (byte)KeyMap.GetKeyIndex(keyModel.Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.MacroKey, (byte)(Combox1.SelectedIndex + 1));

            ACH.SetKeyMacro(model);
        }
    }
}
