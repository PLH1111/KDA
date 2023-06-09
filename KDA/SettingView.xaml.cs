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

namespace KDA;

[AddINotifyPropertyChangedInterface]
public partial class SettingView : FilletWindow
{

    #region 字段

    private IEnumerable<CyHidDevice> hidDevices;

    USBDeviceList usbDevices;

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

    public static KeyColorMapList KeyColorMaps { get; set; } = new(12);

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


    public DelegateCommand<string> CommandReadCommand { get; private set; }

    public DelegateCommand<string> CommandWriteCommand { get; private set; }



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

        CommandWriteCommand = new DelegateCommand<string>(CommandWrite, CanExcuteBootLoaderWrite)
          .ObservesProperty(() => IsDeviceConnect);
        CommandReadCommand = new DelegateCommand<string>(CommandRead, CanExcuteBootLoaderRead)
            .ObservesProperty(() => IsDeviceConnect);

        BootLoaderWriteCommand = new DelegateCommand<string>(BootLoaderWrite, CanExcuteBootLoaderWrite)
            .ObservesProperty(() => IsDeviceConnect);
        BootLoaderReadCommand = new DelegateCommand<string>(BootLoaderRead, CanExcuteBootLoaderRead)
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
            usbDevices.DeviceRemoved -= new EventHandler(UsbDevices_DeviceRemoved);
            usbDevices.DeviceAttached -= new EventHandler(UsbDevices_DeviceAttached);
            usbDevices = null;
            GC.Collect();
        }
    }

    private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await Task.Run(ListHidDevice);
        RefreshDevices();
        if (HidDeviceList != null && GCH.Device != null)
        {
            Device = HidDeviceList.FirstOrDefault(x => x.DevicePath == GCH.Device.Path);
        }
        IsDeviceConnect = GCH.IsDeviceConnect;
    }

    private void ListHidDevice()
    {
        usbDevices = new(CyConst.DEVICES_HID);
        usbDevices.DeviceRemoved += new EventHandler(UsbDevices_DeviceRemoved);
        usbDevices.DeviceAttached += new EventHandler(UsbDevices_DeviceAttached);
    }

    void UsbDevices_DeviceAttached(object sender, EventArgs e)
    {
        RefreshDevices();
    }



    void UsbDevices_DeviceRemoved(object sender, EventArgs e)
    {
        RefreshDevices();
    }

    #endregion

    #region 方法

    #region Can Excute

    private bool CanExcuteBootLoaderWrite(string arg)
    {
        return IsDeviceConnect && Device != null;
    }

    private bool CanExcuteBootLoaderRead(string arg)
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
            foreach(CyHidDevice x in usbDevices)
            {
                if(x.Path== Device.DevicePath)
                {
                    GCH.Device=x;
                }
            }
            IsDeviceConnect = GCH.Device.RwAccessible;
        }
        //IsDeviceConnect = GCH.OpenDevice();
    }

    private void DisconnectDevice()
    {

        //IsDeviceConnect = GCH.CloseDevice();

    }

    private void RefreshDevices()
    {
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
                                               hid.Path);

                    HidDeviceList.Add(model);
                }
            }
        }
        //if (hidDevice.Description.Contains("符合") || hidDevice.Capabilities.OutputReportByteLength != 64)
        //{
        //    continue;
        //}
        //string mc = string.Empty;
        //string pd = string.Empty;
        //string sn = string.Empty;
        //if (hidDevice.ReadManufacturer(out byte[] mcBy))
        //{
        //    mc = Encoding.UTF8.GetString(mcBy).TrimEnd('\0');
        //}

        //if (hidDevice.ReadProduct(out byte[] pdBy))
        //{
        //    pd = Encoding.UTF8.GetString(pdBy).TrimEnd('\0');
        //}

        //if (hidDevice.ReadSerialNumber(out byte[] snBy))
        //{
        //    sn = Encoding.UTF8.GetString(snBy).TrimEnd('\0');
        //}
        //hidDevices = HidDevices.Enumerate();
        //if (hidDevices != null && hidDevices.Any())
        //{
        //    HidDeviceList.Clear();
        //    foreach (var hidDevice in hidDevices)
        //    {
        //        if (hidDevice.Description.Contains("符合") || hidDevice.Capabilities.OutputReportByteLength != 64)
        //        {
        //            continue;
        //        }
        //        string mc = string.Empty;
        //        string pd = string.Empty;
        //        string sn = string.Empty;
        //        if (hidDevice.ReadManufacturer(out byte[] mcBy))
        //        {
        //            mc = Encoding.UTF8.GetString(mcBy).TrimEnd('\0');
        //        }

        //        if (hidDevice.ReadProduct(out byte[] pdBy))
        //        {
        //            pd = Encoding.UTF8.GetString(pdBy).TrimEnd('\0');
        //        }

        //        if (hidDevice.ReadSerialNumber(out byte[] snBy))
        //        {
        //            sn = Encoding.UTF8.GetString(snBy).TrimEnd('\0');
        //        }

        //        HidDeviceModel model = new(mc, pd, hidDevice.Description, hidDevice.Attributes.Version, sn,
        //                                   hidDevice.Attributes.VendorHexId, hidDevice.Attributes.ProductHexId,
        //                                   hidDevice.Capabilities.InputReportByteLength,
        //                                   hidDevice.Capabilities.OutputReportByteLength,
        //                                   hidDevice.Capabilities.FeatureReportByteLength, hidDevice.DevicePath);

        //        HidDeviceList.Add(model);

        //    }
        //}
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

    private void CommandWrite(string obj)
    {
        if (Enum.TryParse(obj, out KeyCommandNames cmd) == false)
        {
            return;
        }
        switch (cmd)
        {
            case KeyCommandNames.Sleep:
                KBCH.SetSleep(SleepModel);
                break;

            case KeyCommandNames.Key_Macro:
                KBCH.SetKeyMacro(KeyMacroModel);
                break;

            case KeyCommandNames.Key_RBG:
                KBCH.SetKeyColor(KeyColorModel);
                break;

            case KeyCommandNames.Animation:
                KBCH.SetAnimation(AnimationModel);
                break;

            case KeyCommandNames.Profile:
                KBCH.SetProfile(ProfileModel);
                break;

            case KeyCommandNames.RBG_Map:
                KBCH.SetColorMap(SelectedKeyColorMap);
                break;

            case KeyCommandNames.Language:
                KBCH.SetLanguage(SelectedLanguage);
                break;

            case KeyCommandNames.Macro_Data:
                KBCH.SetMarcoMap(SelectedMarcoMap);
                break;

            case KeyCommandNames.Profile_Data:
                KBCH.SetProfileMap(SelectedProfileMap);
                break;

            case KeyCommandNames.BootUp:
                KBCH.SetBootUpMap(SelectedBootUpMap);
                break;

            case KeyCommandNames.Flash_Data:
                break;

            case KeyCommandNames.Reset_Default:
                break;

            default:
                break;
        }
    }

    private void CommandRead(string obj)
    {
        if (Enum.TryParse(obj, out KeyCommandNames cmd) == false)
        {
            return;
        }
        switch (cmd)
        {
            case KeyCommandNames.Model:
                KeyModelName = KBCH.GetModel();
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
                SelectedLanguage = KBCH.GetLanguage();
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
        var sleep = KBCH.GetSleep();
        if (sleep != null)
        {
            SleepModel.SleepTime = sleep.SleepTime;
            SleepModel.SleepMode = sleep.SleepMode;
        }
    }

    private static void GetMacroModel()
    {
        var marco = KBCH.GetKeyMacro();
        if (marco != null)
        {
            KeyMacroModel.KeyName = marco.KeyName;
            KeyMacroModel.KeyMode = marco.KeyMode;
            KeyMacroModel.KeyCodeHex = marco.KeyCodeHex;
        }
    }

    private static void GetRGBModel()
    {
        var colorModel = KBCH.GetKeyColor(KeyMacroModel.KeyIndex);
        if (colorModel != null)
        {
            KeyColorModel.ColorRHex = colorModel.ColorRHex;
            KeyColorModel.ColorBHex = colorModel.ColorBHex;
            KeyColorModel.ColorGHex = colorModel.ColorGHex;
            KeyColorModel.ColorAHex = colorModel.ColorAHex;
        }
    }

    private static void GetAnimationModel()
    {
        var animation = KBCH.GetAnimation();
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
        var profileModel = KBCH.GetProfile();
        if (profileModel != null)
        {
            ProfileModel.NumberHex = profileModel.NumberHex;
        }
    }

    private void GegRGBMap()
    {
        var colorMap = KBCH.GetColorMap();
        if (colorMap != null)
        {
            SelectedKeyColorMap.MapDatas = colorMap.MapDatas;
        }
    }

    private void GetMarcoMap()
    {
        var macroMap = KBCH.GetMarcoMap(SelectedMarcoMap.Number);
        if (macroMap != null)
        {
            SelectedMarcoMap.MapDatas = macroMap.MapDatas;
        }
    }

    private void GetProfileMap()
    {
        var profileMap = KBCH.GetProfileMap(SelectedProfileMap.Number);
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
        var map = KBCH.GetBootUpMap(SelectedBootUpMap.Number);
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
                BLH.SetRange(RangeModel);
                break;
            case "Flash":
                BLH.WriteFlash(FlashModel);
                break;
        }
    }

    private void BootLoaderRead(string obj)
    {
        switch (obj)
        {
            case "Config":
                var config = BLH.GetConfig();
                if (config != null)
                {
                    ConfigModel.AdderssHex = config.AdderssHex;
                    ConfigModel.VersionHex = config.VersionHex;
                    ConfigModel.ResponseCode = config.ResponseCode;
                }
                break;
            case "Range":
                var range = BLH.GetRange();
                if (range != null)
                {
                    RangeModel.AdderssHex = range.AdderssHex;
                    RangeModel.SizeHex = range.SizeHex;
                    RangeModel.ResponseCode = range.ResponseCode;
                }
                break;
            case "Flash":
                var flash = BLH.ReadFlash(FlashModel);
                if (flash != null)
                {
                    FlashModel.DataHex = flash.DataHex;
                    FlashModel.ResponseCode = flash.ResponseCode;
                }
                break;
            case "CheckSum":
                var sum = BLH.GetCheckSum(CheckSumModel);
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
