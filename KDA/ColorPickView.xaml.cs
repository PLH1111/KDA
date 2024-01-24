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
using TianWeiToolsPro.Controls;

namespace KDA
{
    /// <summary>
    /// ColorPickView.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickView : FilletWindow
    {
        public Color SelectedColor { get; set; }

        public ColorPickView()
        {
            InitializeComponent();
        }

        public Color ShowView(Color color)
        {
            SelectedColor = color;
            ShowDialog();
            return SelectedColor;
        }
    }
}
