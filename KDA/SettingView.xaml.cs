using HidLibrary;
using KDA.Hooks;
using KDA.Models;
using KDA.Services;
using Prism.Commands;
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
using TianWeiToolsPro.Extensions;

namespace KDA;

[AddINotifyPropertyChangedInterface]
public partial class SettingView : MetroWindow
{

    #region 字段

    private IEnumerable<HidDevice> hidDevices;

    #endregion

    #region 属性


    public List<HidDeviceModel> HidDeviceList { get; set; }

    private HidDeviceModel selectedDeviceModel;
    public HidDeviceModel Device
    {
        get => selectedDeviceModel;
        set => selectedDeviceModel = value;
    }

    public string InputMessage { get; set; }
    public string OutputMessage { get; set; }

    public bool IsDeviceConnect { get; set; }


    public List<KeyCommand> CommandList { get; set; } = new List<KeyCommand>()
    {
        new KeyCommand(KeyCommandNames.Model, 0x00, 0x80, KeyCommandAcceess.ReadOnly),
        new KeyCommand(KeyCommandNames.Sleep, 0x01, 0x81),
        new KeyCommand(KeyCommandNames.Key_Macro, 0x02, 0x82),
        new KeyCommand(KeyCommandNames.Key_RBG, 0x03, 0x83),
        new KeyCommand(KeyCommandNames.Animation, 0x04, 0x84),
        new KeyCommand(KeyCommandNames.Profile, 0x05, 0x85),
        new KeyCommand(KeyCommandNames.RBG_Map, 0x06, 0x86),
        new KeyCommand(KeyCommandNames.Language, 0x07, 0x87),
        new KeyCommand(KeyCommandNames.Macro_Data, 0x10, 0x90),
        new KeyCommand(KeyCommandNames.Profile_Data, 0x11, 0x91),
        new KeyCommand(KeyCommandNames.BootUp, 0x12, 0x92),
        new KeyCommand(KeyCommandNames.Flash_Data, 0x13, 0x93),
        new KeyCommand(KeyCommandNames.Reset_Default, 0x0F, 0x00,KeyCommandAcceess.WriteOnly),
    };


    public KeyCommand selectedCommand;
    public KeyCommand SelectedCommand
    {
        get => selectedCommand;
        set
        {
            selectedCommand = value;
            CanGetModelValue = selectedCommand.Acceess == KeyCommandAcceess.ReadOnly ||
               selectedCommand.Acceess == KeyCommandAcceess.ReadWrite;
            CanSetModelValue = selectedCommand.Acceess == KeyCommandAcceess.WriteOnly ||
                selectedCommand.Acceess == KeyCommandAcceess.ReadWrite;
            switch (selectedCommand.Name)
            {
                case KeyCommandNames.Model:
                    TCSelectedIndex = 0;
                    break;
                case KeyCommandNames.Sleep:
                    TCSelectedIndex = 1;
                    break;
                case KeyCommandNames.Key_Macro:
                    TCSelectedIndex = 2;
                    break;
                case KeyCommandNames.Key_RBG:
                    TCSelectedIndex = 3;
                    break;
                case KeyCommandNames.Animation:
                    TCSelectedIndex = 4;
                    break;
                case KeyCommandNames.Profile:
                    TCSelectedIndex = 5;
                    break;
                case KeyCommandNames.RBG_Map:
                    TCSelectedIndex = 6;
                    break;
                case KeyCommandNames.Language:
                    TCSelectedIndex = 7;
                    break;
                case KeyCommandNames.Macro_Data:
                    TCSelectedIndex = 8;
                    break;
                case KeyCommandNames.Profile_Data:
                    TCSelectedIndex = 9;
                    break;
                case KeyCommandNames.BootUp:
                    TCSelectedIndex = 10;
                    break;
                case KeyCommandNames.Flash_Data:
                    TCSelectedIndex = 11;
                    break;
                default:
                    TCSelectedIndex = 0;
                    break;
            }
        }
    }

    public int TCSelectedIndex { get; set; }

    public bool CanSetModelValue { get; set; }
    public bool CanGetModelValue { get; set; }


    #region KeyModelName

    public string KeyModelName { get; set; }

    #endregion

    #region Sleep

    public static SleepModel SleepModel { get; set; } = new SleepModel();

    public static List<SleepTimes> SleepTimeList => EnumHelper.ToList<SleepTimes>();

    public static List<LightingModes> LightingModeList => EnumHelper.ToList<LightingModes>();

    #endregion

    #region Key Marco

    public static KeyMacroModel KeyMacroModel { get; set; } = new KeyMacroModel();

    public static List<KeyModes> KeyModeList => EnumHelper.ToList<KeyModes>();


    #endregion

    #region Key Color

    public static KeyColorModel KeyColorModel => new();

    #endregion


    #region Animation

    public static AnimationModel AnimationModel { get; set; } = new AnimationModel();

    public static List<AnimationIds> AnimationIdList => EnumHelper.ToList<AnimationIds>();

    public static List<AnimationDisplays> DisplayList => EnumHelper.ToList<AnimationDisplays>();

    public static List<AnimationDirections> DirectionList => EnumHelper.ToList<AnimationDirections>();


    #endregion

    #region Profile

    public static ProfileModel ProfileModel => new();

    #endregion

    #region Language

    public List<KeyBoardLanguages> LanguageList { get; set; } = EnumHelper.ToList<KeyBoardLanguages>();

    public KeyBoardLanguages SelectedLanguage { get; set; } = KeyBoardLanguages.US;


    #endregion




    public List<KeyColorMap> MapList { get; set; } = new()
    {
        new KeyColorMap(0x01),
        new KeyColorMap(0x02),
        new KeyColorMap(0x03),
        new KeyColorMap(0x04),
        new KeyColorMap(0x05),
        new KeyColorMap(0x06),
        new KeyColorMap(0x07),
        new KeyColorMap(0x08),
        new KeyColorMap(0x09),
        new KeyColorMap(0x0A),
        new KeyColorMap(0x0B),
        new KeyColorMap(0x0C),
    };

    public KeyColorMap SelectedMap { get; set; }

    #endregion

    #region 命令

    public DelegateCommand ConnectDeviceCommand { get; private set; }

    public DelegateCommand DisconnectDeviceCommand { get; private set; }

    public DelegateCommand RefreshCommand { get; private set; }


    public DelegateCommand GetModelValueCommand { get; private set; }

    public DelegateCommand SetModelValueCommand { get; private set; }


    public DelegateCommand ReadDataCommand { get; private set; }

    public DelegateCommand WriteDataCommand { get; private set; }

    public DelegateCommand ClearInputMessageCommand { get; private set; }

    public DelegateCommand ClearOutputMessageCommand { get; private set; }

    #endregion

    #region 初始化

    public SettingView()
    {
        InitializeComponent();
        InitFields();
        InitProperties();
        InitCommands();
        InitEvents();
        DataContext = this;
    }

    private void InitFields()
    {

    }

    private void InitProperties()
    {

    }

    private void InitCommands()
    {
        ConnectDeviceCommand = new DelegateCommand(ConncetDevice, CanConnectDevice)
            .ObservesProperty(() => Device)
            .ObservesProperty(() => IsDeviceConnect);
        DisconnectDeviceCommand = new DelegateCommand(DisconnectDevice, CanDisconnectDevice)
            .ObservesProperty(() => Device)
            .ObservesProperty(() => IsDeviceConnect);

        RefreshCommand = new DelegateCommand(RefreshDevices);

        GetModelValueCommand = new DelegateCommand(GetModelValue, CanExcuteGetModelValue)
            .ObservesProperty(() => IsDeviceConnect)
            .ObservesProperty(() => CanGetModelValue);
        SetModelValueCommand = new DelegateCommand(SetModelValue, CanExcuteSetModelValue)
            .ObservesProperty(() => IsDeviceConnect)
            .ObservesProperty(() => CanSetModelValue);

        ReadDataCommand = new DelegateCommand(ReadData, CanExcuteRead).ObservesProperty(() => IsDeviceConnect);
        WriteDataCommand = new DelegateCommand(WriteData, CanExcuteWrite)
            .ObservesProperty(() => IsDeviceConnect)
            .ObservesProperty(() => OutputMessage);
        ClearInputMessageCommand = new DelegateCommand(ClearInputMessage);
        ClearOutputMessageCommand = new DelegateCommand(ClearOutputMessage);
    }



    private bool CanConnectDevice()
    {
        return !IsDeviceConnect && selectedDeviceModel != null;
    }


    private bool CanDisconnectDevice()
    {
        return IsDeviceConnect && selectedDeviceModel != null;
    }

    private bool CanExcuteGetModelValue()
    {
        return IsDeviceConnect && CanGetModelValue;
    }

    private bool CanExcuteSetModelValue()
    {
        return IsDeviceConnect && CanSetModelValue;
    }


    private bool CanExcuteWrite()
    {
        return Device != null
            && Device.OutputReportByteLength > 0
            && !string.IsNullOrEmpty(OutputMessage)
            && IsDeviceConnect;
    }

    private bool CanExcuteRead()
    {
        return Device != null && Device.InputReportByteLength > 0 && IsDeviceConnect;
    }


    private void InitEvents()
    {
        Loaded += MainWindow_Loaded;
    }




    #endregion

    #region 事件

    private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        RefreshDevices();
        if (HidDeviceList != null && GCH.Device != null)
        {
            Device = HidDeviceList.FirstOrDefault(x => x.DevicePath == GCH.Device.DevicePath);
        }
        IsDeviceConnect = GCH.IsDeviceConnect;
    }

    #endregion

    #region 方法


    private void ConncetDevice()
    {
        if (hidDevices != null && selectedDeviceModel != null)
        {
            GCH.Device = hidDevices.FirstOrDefault(x => x.DevicePath == selectedDeviceModel.DevicePath);
        }
        if (GCH.Device == null)
        {
            return;
        }

        try
        {
            GCH.Device.OpenDevice();
            IsDeviceConnect = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    private void DisconnectDevice()
    {
        if (!GCH.IsDeviceConnect)
        {
            return;
        }
        try
        {
            GCH.Device.CloseDevice();
            IsDeviceConnect = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    private void RefreshDevices()
    {
        hidDevices = HidDevices.Enumerate();
        if (hidDevices != null && hidDevices.Any())
        {
            HidDeviceList = new List<HidDeviceModel>();
            foreach (var hidDevice in hidDevices)
            {

                string mc = string.Empty;
                string pd = string.Empty;
                string sn = string.Empty;
                if (hidDevice.ReadManufacturer(out byte[] mcBy))
                {
                    mc = Encoding.UTF8.GetString(mcBy).TrimEnd('\0');
                }

                if (hidDevice.ReadProduct(out byte[] pdBy))
                {
                    pd = Encoding.UTF8.GetString(pdBy).TrimEnd('\0');
                }

                if (hidDevice.ReadSerialNumber(out byte[] snBy))
                {
                    sn = Encoding.UTF8.GetString(snBy).TrimEnd('\0');
                }

                HidDeviceModel model = new(mc, pd, hidDevice.Description, hidDevice.Attributes.Version, sn,
                                           hidDevice.Attributes.VendorHexId, hidDevice.Attributes.ProductHexId,
                                           hidDevice.Capabilities.InputReportByteLength,
                                           hidDevice.Capabilities.OutputReportByteLength,
                                           hidDevice.Capabilities.FeatureReportByteLength, hidDevice.DevicePath);

                HidDeviceList.Add(model);

            }
        }
    }

    private void GetModelValue()
    {
        switch (selectedCommand.Name)
        {
            case KeyCommandNames.Model:
                KeyModelName = KBCH.GetModel();
                break;
            case KeyCommandNames.Sleep:
                var sleep = KBCH.GetSleep();
                SleepModel.SleepTime = sleep.SleepTime;
                SleepModel.SleepMode = sleep.SleepMode;
                break;
            case KeyCommandNames.Key_Macro:
                var marco = KBCH.GetKeyMacro();
                KeyMacroModel.KeyName = marco.KeyIndex.ToHex();
                KeyMacroModel.KeyMode = marco.KeyMode;
                KeyMacroModel.KeyCodeHex = marco.KeyCode.ToHex();
                break;
            case KeyCommandNames.Key_RBG:
                var colorModel = KBCH.GetKeyColor(KeyMacroModel.KeyIndex);
                KeyColorModel.ColorRHex = colorModel.ColorR.ToHex();
                KeyColorModel.ColorBHex = colorModel.ColorB.ToHex();
                KeyColorModel.ColorGHex = colorModel.ColorG.ToHex();
                KeyColorModel.ColorAHex = colorModel.ColorA.ToHex();

                break;
            case KeyCommandNames.Animation:
                var animation = KBCH.GetAnimation();
                AnimationModel.AnimationId = animation.AnimationId;
                AnimationModel.ColorRHex = animation.ColorR.ToHex();
                AnimationModel.ColorBHex = animation.ColorB.ToHex();
                AnimationModel.ColorGHex = animation.ColorG.ToHex();
                AnimationModel.ColorAHex = animation.ColorA.ToHex();
                AnimationModel.SpeedHex = animation.Speed.ToHex();
                AnimationModel.Display = animation.Display;
                AnimationModel.Direction = animation.Direction;
                break;
            case KeyCommandNames.Profile:
                TCSelectedIndex = 5;
                break;
            case KeyCommandNames.RBG_Map:
                TCSelectedIndex = 6;
                break;
            case KeyCommandNames.Language:
                SelectedLanguage = KBCH.GetLanguage();
                break;
            case KeyCommandNames.Macro_Data:
                TCSelectedIndex = 8;
                break;
            case KeyCommandNames.Profile_Data:
                TCSelectedIndex = 9;
                break;
            case KeyCommandNames.BootUp:
                TCSelectedIndex = 10;
                break;
            case KeyCommandNames.Flash_Data:
                TCSelectedIndex = 11;
                break;
            default:
                TCSelectedIndex = 0;
                break;
        }

    }

    private void SetModelValue()
    {

    }


    private void ReadData()
    {

        HidDeviceData report = GCH.Device.Read(20);
        if (report != null)
        {
            InputMessage += report.Data.ToHex(true) + "\r\n";
        }
    }

    private void WriteData()
    {
        byte[] bytes = new byte[selectedDeviceModel.OutputReportByteLength];
        new Random().NextBytes(bytes);
        bool isOK = GCH.Device.Write(bytes);
    }


    private void ClearInputMessage()
    {
        InputMessage = null;
    }

    private void ClearOutputMessage()
    {
        OutputMessage = null;
    }

    #endregion



}
