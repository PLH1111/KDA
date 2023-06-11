using HidLibrary;
using KDA.Models;
using KDA.Models.Bootloader;
using KDA.Models.Commands;
using KDA.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TianWeiToolsPro.Controls;
using TianWeiToolsPro.Extensions;
using CyUSB;
using System.Threading.Tasks;
using System.Threading;

namespace KDA;

[AddINotifyPropertyChangedInterface]
public partial class SettingView : FilletWindow
{

    #region 字段

    private IEnumerable<CyHidDevice> hidDevices;

    USBDeviceList usbDevices;
    USBDeviceList usbMonitor;

    #endregion

    #region 属性


    public ObservableCollection<HidDeviceModel> HidDeviceList { get; set; } = new();
    public HidDeviceModel Device { get; set; }

    public string InputMessage { get; set; }
    public string OutputMessage { get; set; }

    public bool IsDeviceConnect { get; set; }

    #region Commands

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

    public static KeyColorModel KeyColorModel { get; set; } = new();

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

    #region RBG_Map

    public static KeyColorMapList KeyColorMaps { get; set; } = new(11);

    public KeyColorMap SelectedKeyColorMap { get; set; } = KeyColorMaps[0];

    #endregion

    #region MarcoMap

    public static MarcoMapList MarcoMaps { get; set; } = new(0x40);

    public MarcoMap SelectedMarcoMap { get; set; } = MarcoMaps[0];

    #endregion

    #region ProfileMaps

    public static ProfileMapList ProfileMaps { get; set; } = new(0x04);

    public ProfileMap SelectedProfileMap { get; set; } = ProfileMaps[0];

    #endregion

    #region BootupMap

    public static BootUpMapList BootUpMaps { get; set; } = new(0x04);

    public BootUpMap SelectedBootUpMap { get; set; } = BootUpMaps[0];

    #endregion

    #endregion

    #region Bootloader

    public static ConfigModel ConfigModel { get; set; } = new();

    public static RangeModel RangeModel { get; set; } = new();

    public static FlashModel FlashModel { get; set; } = new();

    public static CheckSumModel CheckSumModel { get; set; } = new();

    public static RunModel RunModel { get; set; } = new();

    #endregion

    #region CmdFlashModel

    public static CmdFlashModel CmdFlashModel { get; set; } = new();

    #endregion

    #endregion

    #region 命令

    public DelegateCommand ConnectDeviceCommand { get; private set; }

    public DelegateCommand DisconnectDeviceCommand { get; private set; }

    public DelegateCommand RefreshCommand { get; private set; }


    public DelegateCommand ReadDataCommand { get; private set; }

    public DelegateCommand WriteDataCommand { get; private set; }

    public DelegateCommand ClearInputMessageCommand { get; private set; }

    public DelegateCommand ClearOutputMessageCommand { get; private set; }


    public DelegateCommand<string> ApplicationReadCommand { get; private set; }

    public DelegateCommand<string> ApplicationWriteCommand { get; private set; }



    public DelegateCommand<string> BootLoaderWriteCommand { get; private set; }

    public DelegateCommand<string> BootLoaderReadCommand { get; private set; }


    public DelegateCommand LoadMusicCommand { get; private set; }

    public DelegateCommand PlayMusicCommand { get; private set; }

    public DelegateCommand StopMusicCommand { get; private set; }


    public DelegateCommand StartRecordingCommand { get; private set; }

    public DelegateCommand StopRecordingCommand { get; private set; }



    #endregion

    #region 初始化

    public SettingView() : base()
    {
        InitializeComponent();
    }

    protected override void InitFields()
    {

    }



    protected override void InitProperties()
    {

    }

    protected override void InitCommands()
    {
        ConnectDeviceCommand = new DelegateCommand(ConncetDevice, CanConnectDevice)
            .ObservesProperty(() => Device)
            .ObservesProperty(() => IsDeviceConnect);
        DisconnectDeviceCommand = new DelegateCommand(DisconnectDevice, CanDisconnectDevice)
            .ObservesProperty(() => Device)
            .ObservesProperty(() => IsDeviceConnect);

        RefreshCommand = new DelegateCommand(RefreshDevices);


        ReadDataCommand = new DelegateCommand(ReadData, CanExcuteRead).ObservesProperty(() => IsDeviceConnect);
        WriteDataCommand = new DelegateCommand(WriteData, CanExcuteWrite)
            .ObservesProperty(() => IsDeviceConnect)
            .ObservesProperty(() => OutputMessage);
        ClearInputMessageCommand = new DelegateCommand(ClearInputMessage);
        ClearOutputMessageCommand = new DelegateCommand(ClearOutputMessage);

        ApplicationWriteCommand = new DelegateCommand<string>(ApplicationWrite, CanExcuteCommandWrite)
          .ObservesProperty(() => IsDeviceConnect);
        ApplicationReadCommand = new DelegateCommand<string>(ApplicationRead, CanExcuteCommandRead)
            .ObservesProperty(() => IsDeviceConnect);

        BootLoaderWriteCommand = new DelegateCommand<string>(BootLoaderWrite, CanExcuteCommandWrite)
            .ObservesProperty(() => IsDeviceConnect);
        BootLoaderReadCommand = new DelegateCommand<string>(BootLoaderRead, CanExcuteCommandRead)
            .ObservesProperty(() => IsDeviceConnect);

    }


    protected override void InitEvents()
    {
        Loaded += MainWindow_Loaded;
    }

    #endregion

    #region 事件

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        if (usbDevices != null)
        {
            usbMonitor.DeviceRemoved -= UsbDevices_DeviceRemoved;
            usbMonitor.DeviceAttached -= UsbDevices_DeviceAttached;
            //usbMonitor.Dispose();
            usbMonitor = null;
            GC.Collect();
        }
    }

    private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        InitUsbMonitor();
        RefreshDevices();
        if (HidDeviceList != null && GCH.Device != null)
        {
            Device = HidDeviceList.FirstOrDefault(x => x.DevicePath == GCH.Device.Path);
        }
        IsDeviceConnect = GCH.IsDeviceConnect;
    }

    private void InitUsbMonitor()
    {
        //必须从UI线程创建该对象，与窗口挂钩，否则无法监控设备拔
        usbMonitor = new USBDeviceList(CyConst.DEVICES_HID);
        usbMonitor.DeviceRemoved += UsbDevices_DeviceRemoved;
        usbMonitor.DeviceAttached += UsbDevices_DeviceAttached;
    }

    void UsbDevices_DeviceAttached(object sender, EventArgs e)
    {
        RefreshDevices();
    }



    void UsbDevices_DeviceRemoved(object sender, EventArgs e)
    {
        RefreshDevices();
    }

    private async void RefreshDevices()
    {
        Device = null;
        GCH.Device = null;
        IsDeviceConnect = false;
        await Task.Run(() =>
        {
            usbDevices = new(CyConst.DEVICES_HID);
        });
        if (usbDevices != null && usbDevices.Count > 0)
        {
            HidDeviceList.Clear();
            foreach (var device in usbDevices)
            {
                if (device is CyHidDevice hid && hid.Outputs.RptByteLen == 64)
                {
                    HidDeviceModel model = new(hid.Manufacturer,
                                               hid.Product,
                                               null,
                                               hid.Version,
                                               hid.SerialNumber,
                                               hid.VendorID,
                                               hid.ProductID,
                                               hid.Inputs.RptByteLen,
                                               hid.Outputs.RptByteLen,
                                               hid.Features.RptByteLen,
                                               hid.Path,
                                               hid.FriendlyName);

                    HidDeviceList.Add(model);
                }
            }
        }
    }

    #endregion

    #region 方法

    #region Can Excute

    private bool CanExcuteCommandWrite(string arg)
    {
        return IsDeviceConnect && Device != null;
    }

    private bool CanExcuteCommandRead(string arg)
    {
        return IsDeviceConnect && Device != null;
    }

    private bool CanConnectDevice()
    {
        return !IsDeviceConnect && Device != null;
    }


    private bool CanDisconnectDevice()
    {
        return IsDeviceConnect && Device != null;
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



    #endregion

    #region Excute


    private void ConncetDevice()
    {
        if (usbDevices != null && Device != null)
        {
            foreach (CyHidDevice x in usbDevices)
            {
                if (x.Path == Device.DevicePath)
                {
                    GCH.Device = x;
                    //读写超时，单位：毫秒
                    GCH.Device.TimeOut = 100;
                }
            }
            IsDeviceConnect = GCH.Device.RwAccessible;
        }
        //IsDeviceConnect = GCH.OpenDevice();
    }

    private void DisconnectDevice()
    {
        GCH.Device = null;
        IsDeviceConnect = false;
    }




    private void ReadData()
    {

        byte[] bytes = GCH.ReadData();
        if (bytes != null)
        {
            InputMessage += bytes.ToHex(true) + "\r\n";
        }
    }

    private void WriteData()
    {
        byte[] bytes = new byte[Device.OutputReportByteLength];
        new Random().NextBytes(bytes);
        bool isOK = GCH.WriteData(bytes);
    }


    private void ClearInputMessage()
    {
        InputMessage = null;
    }

    private void ClearOutputMessage()
    {
        OutputMessage = null;
    }

    private void ApplicationWrite(string obj)
    {
        if (Enum.TryParse(obj, out KeyCommandNames cmd) == false)
        {
            return;
        }
        switch (cmd)
        {
            case KeyCommandNames.Sleep:
                ACH.SetSleep(SleepModel);
                break;

            case KeyCommandNames.Key_Macro:
                ACH.SetKeyMacro(KeyMacroModel);
                break;

            case KeyCommandNames.Key_RBG:
                ACH.SetKeyColor(KeyColorModel);
                break;
            case KeyCommandNames.Key_RBG_Random:
                SetKeysRGBRandom();
                break;
            case KeyCommandNames.Animation:
                ACH.SetAnimation(AnimationModel);
                break;

            case KeyCommandNames.Profile:
                ACH.SetProfile(ProfileModel);
                break;

            case KeyCommandNames.RBG_Map:
                ACH.SetColorMap(SelectedKeyColorMap);
                break;

            case KeyCommandNames.Language:
                ACH.SetLanguage(SelectedLanguage);
                break;

            case KeyCommandNames.Macro_Data:
                ACH.SetMarcoMap(SelectedMarcoMap);
                break;

            case KeyCommandNames.Profile_Data:
                ACH.SetProfileMap(SelectedProfileMap);
                break;

            case KeyCommandNames.BootUp:
                ACH.SetBootUpMap(SelectedBootUpMap);
                break;

            case KeyCommandNames.Flash_Data:
                break;

            case KeyCommandNames.Reset_Default:
                break;

            default:
                break;
        }
    }

    readonly Random random = new();
    private void SetKeysRGBRandom()
    {
        Task.Run(() =>
        {
            byte[] bytes = new byte[3];
            KeyColorModel model = new();
            for (byte i = 0; i < 100; i++)
            {
                random.NextBytes(bytes);
                model.KeyIndex = i;
                model.ColorR = bytes[0];
                model.ColorG = bytes[1];
                model.ColorB = bytes[2];
                ACH.SetKeyColor(model);
                Thread.Sleep(1);
            }
        });
    }

    private void ApplicationRead(string obj)
    {
        if (Enum.TryParse(obj, out KeyCommandNames cmd) == false)
        {
            return;
        }
        switch (cmd)
        {
            case KeyCommandNames.Model:
                KeyModelName = ACH.GetModel();
                break;
            case KeyCommandNames.Sleep:
                GetSleepModel();

                break;
            case KeyCommandNames.Key_Macro:
                GetMacroModel();

                break;
            case KeyCommandNames.Key_RBG:
                GetRGBModel();

                break;
            case KeyCommandNames.Animation:
                GetAnimationModel();
                break;

            case KeyCommandNames.Profile:
                GetProfileModel();
                break;
            case KeyCommandNames.RBG_Map:
                GegRGBMap();
                break;

            case KeyCommandNames.Language:
                SelectedLanguage = ACH.GetLanguage();
                break;

            case KeyCommandNames.Macro_Data:
                GetMarcoMap();
                break;

            case KeyCommandNames.Profile_Data:
                GetProfileMap();
                break;

            case KeyCommandNames.BootUp:
                GetBootUpMap();
                break;

            case KeyCommandNames.Flash_Data:
                break;

            default:
                break;
        }
    }

    #region Get Model

    private static void GetSleepModel()
    {
        var sleep = ACH.GetSleep();
        if (sleep != null)
        {
            SleepModel.SleepTime = sleep.SleepTime;
            SleepModel.SleepMode = sleep.SleepMode;
        }
    }

    private static void GetMacroModel()
    {
        var marco = ACH.GetKeyMacro();
        if (marco != null)
        {
            KeyMacroModel.KeyName = marco.KeyName;
            KeyMacroModel.KeyMode = marco.KeyMode;
            KeyMacroModel.KeyCodeHex = marco.KeyCodeHex;
        }
    }

    private static void GetRGBModel()
    {
        var colorModel = ACH.GetKeyColor(KeyColorModel.KeyIndex);
        if (colorModel != null)
        {
            KeyColorModel.ColorRHex = colorModel.ColorRHex;
            KeyColorModel.ColorGHex = colorModel.ColorGHex;
            KeyColorModel.ColorBHex = colorModel.ColorBHex;
            KeyColorModel.ColorAHex = colorModel.ColorAHex;
        }
    }

    private static void GetAnimationModel()
    {
        var animation = ACH.GetAnimation();
        if (animation != null)
        {
            AnimationModel.AnimationId = animation.AnimationId;
            AnimationModel.ColorRHex = animation.ColorRHex;
            AnimationModel.ColorBHex = animation.ColorBHex;
            AnimationModel.ColorGHex = animation.ColorGHex;
            AnimationModel.ColorAHex = animation.ColorAHex;
            AnimationModel.SpeedHex = animation.SpeedHex;
            AnimationModel.Display = animation.Display;
            AnimationModel.Direction = animation.Direction;
        }
    }

    private static void GetProfileModel()
    {
        var profileModel = ACH.GetProfile();
        if (profileModel != null)
        {
            ProfileModel.NumberHex = profileModel.NumberHex;
        }
    }

    private void GegRGBMap()
    {
        var colorMap = ACH.GetColorMap();
        if (colorMap != null)
        {
            SelectedKeyColorMap.MapDatas = colorMap.MapDatas;
        }
    }

    private void GetMarcoMap()
    {
        var macroMap = ACH.GetMarcoMap(SelectedMarcoMap.Number);
        if (macroMap != null)
        {
            SelectedMarcoMap.MapDatas = macroMap.MapDatas;
        }
    }

    private void GetProfileMap()
    {
        var profileMap = ACH.GetProfileMap(SelectedProfileMap.Number);
        if (profileMap != null)
        {
            SelectedProfileMap.AnimationId = profileMap.AnimationId;
            SelectedProfileMap.ColorRHex = profileMap.ColorRHex;
            SelectedProfileMap.ColorBHex = profileMap.ColorBHex;
            SelectedProfileMap.ColorGHex = profileMap.ColorGHex;
            SelectedProfileMap.ColorAHex = profileMap.ColorAHex;
            SelectedProfileMap.SpeedHex = profileMap.SpeedHex;
            SelectedProfileMap.Display = profileMap.Display;
            SelectedProfileMap.Direction = profileMap.Direction;
            SelectedProfileMap.MapDatas = profileMap.MapDatas;
        }
    }

    private void GetBootUpMap()
    {
        var map = ACH.GetBootUpMap(SelectedBootUpMap.Number);
        if (map != null)
        {
            SelectedBootUpMap.MapDatas = map.MapDatas;
        }
    }

    #endregion

    private void BootLoaderWrite(string obj)
    {
        switch (obj)
        {
            case "Range":
                BCH.SetRange(RangeModel);
                break;
            case "Flash":
                BCH.WriteFlash(FlashModel);
                break;
            case "Run":
                BCH.RunApp(RunModel.Adderss);
                break;
        }
    }

    private void BootLoaderRead(string obj)
    {
        switch (obj)
        {
            case "Config":
                ConfigModel.ResponseCode = ResponseCodes.TBA;
                var config = BCH.GetConfig();
                if (config != null)
                {
                    ConfigModel.AdderssHex = config.AdderssHex;
                    ConfigModel.VersionHex = config.VersionHex;
                    ConfigModel.ResponseCode = config.ResponseCode;
                }
                break;
            case "Range":
                var range = BCH.GetRange();
                if (range != null)
                {
                    RangeModel.AdderssHex = range.AdderssHex;
                    RangeModel.SizeHex = range.SizeHex;
                    RangeModel.ResponseCode = range.ResponseCode;
                }
                break;
            case "Flash":
                var flash = BCH.ReadFlash(FlashModel);
                if (flash != null)
                {
                    FlashModel.DataHex = flash.DataHex;
                    FlashModel.ResponseCode = flash.ResponseCode;
                }
                break;
            case "CheckSum":
                var sum = BCH.GetCheckSum(CheckSumModel);
                if (sum != null)
                {
                    CheckSumModel.CheckSumHex = sum.CheckSumHex;
                    CheckSumModel.ResponseCode = sum.ResponseCode;
                }
                break;
        }
    }


    #endregion

    #endregion

}
