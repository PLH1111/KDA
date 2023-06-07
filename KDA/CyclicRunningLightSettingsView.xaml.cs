using KDA.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace KDA;

/// <summary>
/// CyclicRunningLightSettingsView.xaml 的交互逻辑
/// </summary>
public partial class CyclicRunningLightSettingsView : FilletWindow
{

    public static CyclicRunningLightSettings Settings { get; set; } = new CyclicRunningLightSettings();

    public CustomColor SelectedColor { get; set; }


    public DelegateCommand AddCustomColorCommand { get; set; }

    public DelegateCommand RemoveCustomColorCommand { get; set; }

    public CyclicRunningLightSettingsView()
    {
        InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        if (!Settings.IsAutoColor && Settings.CustomColors.Count == 0)
        {
            TianWeiToolsPro.Service.MsgBoxService.ShowError("如果选择自选颜色，请添加一些自选颜色，否则请使用自动颜色！");
            e.Cancel = true;
            return;
        }

    }

    protected override void InitCommands()
    {
        AddCustomColorCommand = new DelegateCommand(AddCustomColor);
        RemoveCustomColorCommand = new DelegateCommand(RemoveCustomColor, CanExcuteRemoveCustomColor).ObservesProperty(() => SelectedColor);
    }

    private bool CanExcuteRemoveCustomColor()
    {
        return SelectedColor != null;
    }

    readonly Random random = new();
    private void AddCustomColor()
    {
        if (Settings.Columns * (Settings.CustomColors.Count + 1) > 41)
        {
            MsgBoxService.ShowError("列数与颜色相乘不能大于41!");
            return;
        }
        byte[] rbg = new byte[3];
        random.NextBytes(rbg);
        Brush brush = new SolidColorBrush(Color.FromRgb(rbg[0], rbg[1], rbg[2]));
        Settings.CustomColors.Add(new CustomColor(brush));
    }

    private void RemoveCustomColor()
    {
        Settings.CustomColors.Remove(SelectedColor);
    }

    public CyclicRunningLightSettings ShowView()
    {
        ShowDialog();
        return Settings;
    }
}
