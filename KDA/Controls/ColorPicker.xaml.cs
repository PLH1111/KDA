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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDA.Controls;

/// <summary>
/// ColorPicker.xaml 的交互逻辑
/// </summary>
public partial class ColorPicker : UserControl
{

    public SolidColorBrush SelectedColor
    {
        get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
        set { SetValue(SelectedColorProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="SelectedColor"/> dependency property.</summary>
    public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register(nameof(SelectedColor), typeof(SolidColorBrush), typeof(ColorPicker));



    public ColorPicker()
    {
        InitializeComponent();
    }

    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        ColorPickView view = new();
        var color = view.ShowView(SelectedColor.Color);
        if (SelectedColor == null)
        {
            SetCurrentValue(SelectedColorProperty, new SolidColorBrush(color));
        }
        else
        {
            SelectedColor.SetCurrentValue(SolidColorBrush.ColorProperty, color);
        }

    }
}
