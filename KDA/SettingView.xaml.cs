using HidLibrary;
using KDA.Audio;
using KDA.Models;
using KDA.Models.Bootloader;
using KDA.Models.Commands;
using KDA.Services;
using Microsoft.Win32;
using NAudio.Wave;
using NAudio.WaveFormRenderer;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TianWeiToolsPro.Events;
using TianWeiToolsPro.Extensions;

namespace KDA;

[AddINotifyPropertyChangedInterface]
public partial class SettingView : MetroWindow
{

    #region 字段

    private IEnumerable<HidDevice> hidDevices;

    private readonly Audio.VisualizerDataHelper visualizerDataHelper = new(256);

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

    #region Wave Peak

    public string SelectedFileName { get; set; }

    public bool CanPlayAudio { get; set; } = true;

    public bool CanStartRecording { get; set; } = true;


    public static FFTBarList FFTBars { get; set; } = new(20);

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

        LoadMusicCommand = new DelegateCommand(LoadMusic);

        PlayMusicCommand = new DelegateCommand(StartPlayMusic, CanExcuteStartPlayMusic)
            .ObservesProperty(() => CanPlayAudio)
            .ObservesProperty(() => SelectedFileName);
        StopMusicCommand = new DelegateCommand(StopPlayMusic, CanExcuteStopPlayMusic)
            .ObservesProperty(() => CanPlayAudio);

        LoadMusicCommand = new DelegateCommand(LoadMusic);


        StartRecordingCommand = new DelegateCommand(StartRecording, CanExcuteStartRecording)
            .ObservesProperty(() => CanStartRecording);
        StopRecordingCommand = new DelegateCommand(StopRecording, CanExcuteStopRecording)
            .ObservesProperty(() => CanStartRecording);
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

    private bool CanExcuteStartPlayMusic()
    {
        return CanPlayAudio && !string.IsNullOrEmpty(SelectedFileName);
    }

    private bool CanExcuteStopPlayMusic()
    {
        return !CanPlayAudio;
    }


    private bool CanExcuteStartRecording()
    {
        return CanStartRecording;
    }

    private bool CanExcuteStopRecording()
    {
        return !CanStartRecording;
    }

    #endregion

    #region Excute


    private void ConncetDevice()
    {
        if (hidDevices != null && Device != null)
        {
            GCH.Device = hidDevices.FirstOrDefault(x => x.DevicePath == Device.DevicePath);
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
            HidDeviceList.Clear();
            foreach (var hidDevice in hidDevices)
            {
                if (hidDevice.Description.Contains("符合"))
                {
                    continue;
                }
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
        byte[] bytes = new byte[Device.OutputReportByteLength];
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

    private void LoadMusic()
    {
        var ofd = new OpenFileDialog
        {
            Filter = "MP3 Files|*.mp3|WAV files|*.wav"
        };
        if (ofd.ShowDialog(this) == true)
        {
            SelectedFileName = ofd.FileName;
        }
    }

    WaveOutEvent outputDevice;

    private void StartPlayMusic()
    {
        try
        {
            using var audioFile = new AudioFileReader(SelectedFileName);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play(); // 异步执行
            CanPlayAudio = !(outputDevice.PlaybackState == PlaybackState.Playing);
        }
        catch (Exception ex)
        {
            TianWeiToolsPro.Service.NoticeBoxService.ShowError(ex.Message);
        }

    }

    private void StopPlayMusic()
    {
        if (outputDevice == null)
        {
            return;
        }
        try
        {
            outputDevice.Stop();
            CanPlayAudio = true;
            outputDevice.Dispose();
        }
        catch (Exception ex)
        {
            TianWeiToolsPro.Service.NoticeBoxService.ShowError(ex.Message);
        }

    }



    WasapiLoopbackCapture capture;
    private void StartRecording()
    {
        try
        {
            capture = new WasapiLoopbackCapture();
            capture.DataAvailable += Cap_DataAvailable;
            capture.StartRecording();
            CanStartRecording = false;
            Task.Run(GetSpectrumData);
        }
        catch (Exception ex)
        {
            TianWeiToolsPro.Service.NoticeBoxService.ShowError(ex.Message);
        }

    }


    private void Cap_DataAvailable(object sender, WaveInEventArgs e)
    {
        int length = e.BytesRecorded / 4;           // 采样的数量 (每一个采样是 4 字节)
        double[] result = new double[length];       // 声明结果

        for (int i = 0; i < length; i++)
        {
            result[i] = BitConverter.ToSingle(e.Buffer, i * 4);      // 取出采样值
        }
        visualizerDataHelper.PushSampleData(result);          // 将新的采样存储到 可视化器 中
    }



    double[] spectrumData;
    readonly double scale = 200000;
    double[] barDatas = new double[FFTBars.Count];
    private void GetSpectrumData()
    {
        DateTime time = DateTime.Now;
        while (true)
        {
            if (CanStartRecording)
            {
                break;
            }
            if (DateTime.Now.Subtract(time).TotalMilliseconds >= 32)
            {
                time = DateTime.Now;
                double[] newSpectrumData = visualizerDataHelper.GetSpectrumData();         // 从可视化器中获取频谱数据
                newSpectrumData = VisualizerDataHelper.MakeSmooth(newSpectrumData, 2);                // 平滑频谱数据
                spectrumData = newSpectrumData;
                if (spectrumData.All(x => x >= 0))
                {
                    for (int i = 0; i < FFTBars.Count; i++)
                    {
                        barDatas[i] = spectrumData.ToList().GetRange(6 * i, 6).Average() * scale;
                        FFTBars[i].Height = barDatas[i];
                    }
                    var events = new TEventArgs<double[]>("", "BarData", barDatas);
                    EaHelper.Publish(events);
                }

            }

            Thread.Sleep(1);
        }
    }

    private void StopRecording()
    {
        try
        {
            capture.StopRecording();
            CanStartRecording = true;
        }
        catch (Exception ex)
        {
            TianWeiToolsPro.Service.NoticeBoxService.ShowError(ex.Message);
        }

    }

    #endregion

}
