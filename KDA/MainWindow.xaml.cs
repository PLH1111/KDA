using HidLibrary;
using KDA.Audio;
using KDA.Hooks;
using KDA.Models;
using KDA.Models.Commands;
using KDA.Services;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TianWeiToolsPro.Commands;
using TianWeiToolsPro.Controls;
using TianWeiToolsPro.Events;
using TianWeiToolsPro.Service;

namespace KDA;

[AddINotifyPropertyChangedInterface]
public partial class MainWindow : FilletWindow
{
    #region 字段

    KeyboardHook hook;

    private AnimationKeyModel animationKeyModel;

    private KeyBarList keyBars;

    private IEnumerable<HidDevice> hidDevices;

    private readonly VisualizerDataHelper visualizerDataHelper = new(128);

    #endregion

    #region 属性

    #region Key - 按键相关

    #region 第一行按键

    public KeyModel KeyEscape { get; set; } = new KeyModel(Key.Escape);

    public KeyModel KeyF1 { get; set; } = new KeyModel(Key.F1);

    public KeyModel KeyF2 { get; set; } = new KeyModel(Key.F2);

    public KeyModel KeyF3 { get; set; } = new KeyModel(Key.F3);

    public KeyModel KeyF4 { get; set; } = new KeyModel(Key.F4);

    public KeyModel KeyF5 { get; set; } = new KeyModel(Key.F5);

    public KeyModel KeyF6 { get; set; } = new KeyModel(Key.F6);

    public KeyModel KeyF7 { get; set; } = new KeyModel(Key.F7);

    public KeyModel KeyF8 { get; set; } = new KeyModel(Key.F8);

    public KeyModel KeyF9 { get; set; } = new KeyModel(Key.F9);

    public KeyModel KeyF10 { get; set; } = new KeyModel(Key.F10);

    public KeyModel KeyF11 { get; set; } = new KeyModel(Key.F11);

    public KeyModel KeyF12 { get; set; } = new KeyModel(Key.F12);

    public KeyModel KeyPrintScreen { get; set; } = new KeyModel(Key.PrintScreen);

    public KeyModel KeyScroll { get; set; } = new KeyModel(Key.Scroll);

    public KeyModel KeyPause { get; set; } = new KeyModel(Key.Pause);

    #endregion

    #region 第二行按键

    public KeyModel KeyOem3 { get; set; } = new KeyModel(Key.Oem3);

    public KeyModel KeyD1 { get; set; } = new KeyModel(Key.D1);

    public KeyModel KeyD2 { get; set; } = new KeyModel(Key.D2);

    public KeyModel KeyD3 { get; set; } = new KeyModel(Key.D3);

    public KeyModel KeyD4 { get; set; } = new KeyModel(Key.D4);

    public KeyModel KeyD5 { get; set; } = new KeyModel(Key.D5);

    public KeyModel KeyD6 { get; set; } = new KeyModel(Key.D6);

    public KeyModel KeyD7 { get; set; } = new KeyModel(Key.D7);

    public KeyModel KeyD8 { get; set; } = new KeyModel(Key.D8);

    public KeyModel KeyD9 { get; set; } = new KeyModel(Key.D9);

    public KeyModel KeyD0 { get; set; } = new KeyModel(Key.D0);

    public KeyModel KeyOemMinus { get; set; } = new KeyModel(Key.OemMinus);

    public KeyModel KeyOemPlus { get; set; } = new KeyModel(Key.OemPlus);

    public KeyModel KeyBack { get; set; } = new KeyModel(Key.Back);

    public KeyModel KeyInsert { get; set; } = new KeyModel(Key.Insert);

    public KeyModel KeyHome { get; set; } = new KeyModel(Key.Home);

    public KeyModel KeyPageUp { get; set; } = new KeyModel(Key.PageUp);

    public KeyModel KeyNumLock { get; set; } = new KeyModel(Key.NumLock);

    public KeyModel KeyDivide { get; set; } = new KeyModel(Key.Divide);

    public KeyModel KeyMultiply { get; set; } = new KeyModel(Key.Multiply);

    public KeyModel KeySubtract { get; set; } = new KeyModel(Key.Subtract);

    #endregion

    #region 第三行按键

    public KeyModel KeyTab { get; set; } = new KeyModel(Key.Tab);

    public KeyModel KeyQ { get; set; } = new KeyModel(Key.Q);

    public KeyModel KeyW { get; set; } = new KeyModel(Key.W);

    public KeyModel KeyE { get; set; } = new KeyModel(Key.E);

    public KeyModel KeyR { get; set; } = new KeyModel(Key.R);

    public KeyModel KeyT { get; set; } = new KeyModel(Key.T);

    public KeyModel KeyY { get; set; } = new KeyModel(Key.Y);

    public KeyModel KeyU { get; set; } = new KeyModel(Key.U);

    public KeyModel KeyI { get; set; } = new KeyModel(Key.I);

    public KeyModel KeyO { get; set; } = new KeyModel(Key.O);

    public KeyModel KeyP { get; set; } = new KeyModel(Key.P);

    public KeyModel KeyOem4 { get; set; } = new KeyModel(Key.Oem4);

    public KeyModel KeyOem6 { get; set; } = new KeyModel(Key.Oem6);

    public KeyModel KeyOem5 { get; set; } = new KeyModel(Key.Oem5);

    public KeyModel KeyDelete { get; set; } = new KeyModel(Key.Delete);

    public KeyModel KeyEnd { get; set; } = new KeyModel(Key.End);

    public KeyModel KeyPageDown { get; set; } = new KeyModel(Key.PageDown);

    public KeyModel KeyNumPad7 { get; set; } = new KeyModel(Key.NumPad7);

    public KeyModel KeyNumPad8 { get; set; } = new KeyModel(Key.NumPad8);

    public KeyModel KeyNumPad9 { get; set; } = new KeyModel(Key.NumPad9);

    public KeyModel KeyAdd { get; set; } = new KeyModel(Key.Add);

    #endregion

    #region 第四行按键

    public KeyModel KeyCapsLock { get; set; } = new KeyModel(Key.CapsLock);

    public KeyModel KeyA { get; set; } = new KeyModel(Key.A);

    public KeyModel KeyS { get; set; } = new KeyModel(Key.S);

    public KeyModel KeyD { get; set; } = new KeyModel(Key.D);

    public KeyModel KeyF { get; set; } = new KeyModel(Key.F);

    public KeyModel KeyG { get; set; } = new KeyModel(Key.G);

    public KeyModel KeyH { get; set; } = new KeyModel(Key.H);

    public KeyModel KeyJ { get; set; } = new KeyModel(Key.J);

    public KeyModel KeyK { get; set; } = new KeyModel(Key.K);

    public KeyModel KeyL { get; set; } = new KeyModel(Key.L);

    public KeyModel KeyOemSemicolon { get; set; } = new KeyModel(Key.OemSemicolon);

    public KeyModel KeyOemQuotes { get; set; } = new KeyModel(Key.OemQuotes);

    public KeyModel KeyEnter { get; set; } = new KeyModel(Key.Enter);

    public KeyModel KeyNumPad4 { get; set; } = new KeyModel(Key.NumPad4);

    public KeyModel KeyNumPad5 { get; set; } = new KeyModel(Key.NumPad5);

    public KeyModel KeyNumPad6 { get; set; } = new KeyModel(Key.NumPad6);


    #endregion

    #region 第五行按键

    public KeyModel KeyLeftShift { get; set; } = new KeyModel(Key.LeftShift);
    public KeyModel KeyZ { get; set; } = new KeyModel(Key.Z);
    public KeyModel KeyX { get; set; } = new KeyModel(Key.X);
    public KeyModel KeyC { get; set; } = new KeyModel(Key.C);
    public KeyModel KeyV { get; set; } = new KeyModel(Key.V);
    public KeyModel KeyB { get; set; } = new KeyModel(Key.B);
    public KeyModel KeyN { get; set; } = new KeyModel(Key.N);
    public KeyModel KeyM { get; set; } = new KeyModel(Key.M);
    public KeyModel KeyOemComma { get; set; } = new KeyModel(Key.OemComma);
    public KeyModel KeyOemPeriod { get; set; } = new KeyModel(Key.OemPeriod);
    public KeyModel KeyOemQuestion { get; set; } = new KeyModel(Key.OemQuestion);
    public KeyModel KeyRightShift { get; set; } = new KeyModel(Key.RightShift);
    public KeyModel KeyModelUp { get; set; } = new KeyModel(Key.Up);
    public KeyModel KeyNumPad1 { get; set; } = new KeyModel(Key.NumPad1);
    public KeyModel KeyNumPad2 { get; set; } = new KeyModel(Key.NumPad2);
    public KeyModel KeyNumPad3 { get; set; } = new KeyModel(Key.NumPad3);
    public KeyModel KeyReturn { get; set; } = new KeyModel(Key.Enter);


    #endregion

    #region 第六行按键

    public KeyModel KeyLeftCtrl { get; set; } = new KeyModel(Key.LeftCtrl);

    public KeyModel KeyFn { get; set; } = new KeyModel(Key.FinalMode);

    public KeyModel KeyLWin { get; set; } = new KeyModel(Key.LWin);

    public KeyModel KeyLeftAlt { get; set; } = new KeyModel(Key.LeftAlt);

    public KeyModel KeySpace { get; set; } = new KeyModel(Key.Space);

    public KeyModel KeyRightAlt { get; set; } = new KeyModel(Key.RightAlt);

    public KeyModel KeyApps { get; set; } = new KeyModel(Key.Apps);

    public KeyModel KeyRightCtrl { get; set; } = new KeyModel(Key.RightCtrl);

    public KeyModel KeyModelLeft { get; set; } = new KeyModel(Key.Left);

    public KeyModel KeyModelDown { get; set; } = new KeyModel(Key.Down);

    public KeyModel KeyModelRight { get; set; } = new KeyModel(Key.Right);

    public KeyModel KeyNumPad0 { get; set; } = new KeyModel(Key.NumPad0);

    public KeyModel KeyDecimal { get; set; } = new KeyModel(Key.Decimal);

    #endregion

    public KeyModelList KeyModelList { get; set; } = new KeyModelList();

    #endregion


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


    #region Wave Peak

    public string SelectedFileName { get; set; }

    public bool CanPlayAudio { get; set; } = true;

    public bool CanStartRecording { get; set; } = true;


    public static FFTBarList FFTBars { get; set; } = new(21);

    #endregion

    #region Anition Simulating

    public bool CanStartSimulating { get; set; } = true;

    #endregion

    #region CmdFlashModel

    public static CmdFlashModel CmdFlashModel { get; set; } = new();

    #endregion

    #endregion

    #region 命令

    public DelegateCommand ConnectDeviceCommand { get; private set; }

    public DelegateCommand DisconnectDeviceCommand { get; private set; }

    public DelegateCommand RefreshCommand { get; private set; }


    public DelegateCommand<string> CommandReadCommand { get; private set; }

    public DelegateCommand<string> CommandWriteCommand { get; private set; }


    public DelegateCommand LoadMusicCommand { get; private set; }

    public DelegateCommand PlayMusicCommand { get; private set; }

    public DelegateCommand StopMusicCommand { get; private set; }

    public DelegateCommand StartRecordingCommand { get; private set; }

    public DelegateCommand StopRecordingCommand { get; private set; }


    public DelegateCommand StartSimulatingCommand { get; private set; }

    public DelegateCommand StopSimulatingCommand { get; private set; }



    #endregion

    #region 初始化

    public MainWindow()
    {
        InitializeComponent();
        InitFields();
        InitProperties();
        InitCommands();
        InitEvents();
        DataContext = this;
    }

    protected override void InitFields()
    {
        InitAnimationKeyGroups();
        InitKeyBars();
    }

    private void InitAnimationKeyGroups()
    {

        animationKeyModel = new AnimationKeyModel()
        {
            KeyOem3,
            KeyTab,

            KeyD1,
            KeyQ,

            KeyD2,
            KeyW,

            KeyD3,
            KeyE,

            KeyD4,
            KeyR,

            KeyD5,
            KeyT,

            KeyD6,
            KeyY,

            KeyD7,
            KeyU,

            KeyD8,
            KeyI,

            KeyD9,
            KeyO,

            KeyD0,
            KeyP,

            KeyOemMinus,
            KeyOem4,

            KeyOemPlus,
            KeyOem6,

            KeyBack,
            KeyOem5,

            KeyInsert,
            KeyDelete,

            KeyHome,
            KeyEnd,

            KeyPageUp,
            KeyPageDown,

            KeyNumLock,
            KeyNumPad7,

            KeyDivide,
            KeyNumPad8,

            KeyMultiply,
            KeyNumPad9,

            KeySubtract,
            KeyAdd,

            KeyReturn,
            KeyNumPad6,

            KeyNumPad3,
            KeyDecimal,

            KeyNumPad2,
            KeyNumPad0,

            KeyNumPad1,
            KeyNumPad0,

            KeyModelRight,
            KeyModelRight,

            KeyModelUp,
            KeyModelDown,

            KeyModelLeft,
            KeyModelLeft,

            KeyRightShift,
            KeyRightCtrl,

            KeyOemQuestion,
            KeyApps,

            KeyOemPeriod,
            KeyRightAlt,

            KeyOemComma,
            KeySpace,

            KeyM,
            KeySpace,

            KeyN,
            KeySpace,

            KeyB,
            KeySpace,

            KeyV,
            KeySpace,

            KeyC,
            KeySpace,

            KeyX,
            KeyLeftAlt,

            KeyZ,
            KeyLWin,

            KeyLeftShift,
            KeyLeftCtrl,

            KeyCapsLock,
            KeyA,

        };
    }

    private void InitKeyBars()
    {
        keyBars = new KeyBarList();

        KeyBar bar01 = new();
        bar01.Keys01.Add(KeyLeftCtrl);
        bar01.Keys01.Add(KeyFn);
        bar01.Keys02.Add(KeyLeftShift);
        bar01.Keys03.Add(KeyCapsLock);
        bar01.Keys04.Add(KeyTab);
        bar01.Keys05.Add(KeyOem3);
        keyBars.Add(bar01);

        KeyBar bar02 = new();
        bar02.Keys01.Add(KeyLWin);
        bar02.Keys02.Add(KeyZ);
        bar02.Keys03.Add(KeyA);
        bar02.Keys04.Add(KeyQ);
        bar02.Keys05.Add(KeyD1);
        keyBars.Add(bar02);

        KeyBar bar03 = new();
        bar03.Keys01.Add(KeyLeftAlt);
        bar03.Keys02.Add(KeyX);
        bar03.Keys03.Add(KeyS);
        bar03.Keys04.Add(KeyW);
        bar03.Keys05.Add(KeyD2);
        keyBars.Add(bar03);

        KeyBar bar04 = new();
        bar04.Keys01.Add(KeySpace);
        bar04.Keys02.Add(KeyC);
        bar04.Keys03.Add(KeyD);
        bar04.Keys04.Add(KeyE);
        bar04.Keys05.Add(KeyD3);
        keyBars.Add(bar04);

        KeyBar bar05 = new();
        bar05.Keys01.Add(KeySpace);
        bar05.Keys02.Add(KeyV);
        bar05.Keys03.Add(KeyF);
        bar05.Keys04.Add(KeyR);
        bar05.Keys05.Add(KeyD4);
        keyBars.Add(bar05);

        KeyBar bar06 = new();
        bar06.Keys01.Add(KeySpace);
        bar06.Keys02.Add(KeyB);
        bar06.Keys03.Add(KeyG);
        bar06.Keys04.Add(KeyT);
        bar06.Keys05.Add(KeyD5);
        keyBars.Add(bar06);

        KeyBar bar07 = new();
        bar07.Keys01.Add(KeySpace);
        bar07.Keys02.Add(KeyN);
        bar07.Keys03.Add(KeyH);
        bar07.Keys04.Add(KeyY);
        bar07.Keys05.Add(KeyD6);
        keyBars.Add(bar07);

        KeyBar bar08 = new();
        bar08.Keys01.Add(KeySpace);
        bar08.Keys02.Add(KeyM);
        bar08.Keys03.Add(KeyJ);
        bar08.Keys04.Add(KeyU);
        bar08.Keys05.Add(KeyD7);
        keyBars.Add(bar08);

        KeyBar bar09 = new();
        bar09.Keys01.Add(KeySpace);
        bar09.Keys02.Add(KeyOemComma);
        bar09.Keys03.Add(KeyK);
        bar09.Keys04.Add(KeyI);
        bar09.Keys05.Add(KeyD8);
        keyBars.Add(bar09);

        KeyBar bar10 = new();
        bar10.Keys01.Add(KeyRightAlt);
        bar10.Keys02.Add(KeyOemPeriod);
        bar10.Keys03.Add(KeyL);
        bar10.Keys04.Add(KeyO);
        bar10.Keys05.Add(KeyD9);
        keyBars.Add(bar10);

        KeyBar bar11 = new();
        bar11.Keys01.Add(KeyApps);
        bar11.Keys02.Add(KeyOemQuestion);
        bar11.Keys03.Add(KeyOemSemicolon);
        bar11.Keys04.Add(KeyP);
        bar11.Keys05.Add(KeyD0);
        keyBars.Add(bar11);

        KeyBar bar12 = new();
        bar12.Keys01.Add(KeyRightCtrl);
        bar12.Keys02.Add(KeyRightShift);
        bar12.Keys03.Add(KeyOemQuotes);
        bar12.Keys03.Add(KeyEnter);
        bar12.Keys04.Add(KeyOem4);
        bar12.Keys05.Add(KeyOemMinus);
        keyBars.Add(bar12);

        KeyBar bar13 = new();
        bar13.Keys01.Add(KeyRightCtrl);
        bar13.Keys02.Add(KeyRightShift);
        bar13.Keys03.Add(KeyOemQuotes);
        bar13.Keys03.Add(KeyEnter);
        bar13.Keys04.Add(KeyOem4);
        bar13.Keys05.Add(KeyOemMinus);
        keyBars.Add(bar13);

        KeyBar bar14 = new();
        bar14.Keys01.Add(KeyRightCtrl);
        bar14.Keys02.Add(KeyRightShift);
        bar14.Keys03.Add(KeyOemQuotes);
        bar14.Keys03.Add(KeyEnter);
        bar14.Keys04.Add(KeyOem5);
        bar14.Keys05.Add(KeyBack);
        keyBars.Add(bar14);

        KeyBar bar15 = new();
        bar15.Keys01.Add(KeyModelLeft);
        bar15.Keys04.Add(KeyDelete);
        bar15.Keys05.Add(KeyInsert);
        keyBars.Add(bar15);

        KeyBar bar16 = new();
        bar16.Keys01.Add(KeyModelDown);
        bar16.Keys02.Add(KeyModelUp);
        bar16.Keys04.Add(KeyEnd);
        bar16.Keys05.Add(KeyHome);
        keyBars.Add(bar16);

        KeyBar bar17 = new();
        bar17.Keys01.Add(KeyModelRight);
        bar17.Keys04.Add(KeyPageDown);
        bar17.Keys05.Add(KeyPageUp);
        keyBars.Add(bar17);

        KeyBar bar18 = new();
        bar18.Keys01.Add(KeyNumPad0);
        bar18.Keys02.Add(KeyNumPad1);
        bar18.Keys03.Add(KeyNumPad4);
        bar18.Keys04.Add(KeyNumPad7);
        bar18.Keys05.Add(KeyNumLock);
        keyBars.Add(bar18);

        KeyBar bar19 = new();
        bar19.Keys01.Add(KeyNumPad0);
        bar19.Keys02.Add(KeyNumPad2);
        bar19.Keys03.Add(KeyNumPad5);
        bar19.Keys04.Add(KeyNumPad8);
        bar19.Keys05.Add(KeyDivide);
        keyBars.Add(bar19);

        KeyBar bar20 = new();
        bar20.Keys01.Add(KeyDecimal);
        bar20.Keys02.Add(KeyNumPad3);
        bar20.Keys03.Add(KeyNumPad6);
        bar20.Keys04.Add(KeyNumPad9);
        bar20.Keys05.Add(KeyMultiply);
        keyBars.Add(bar20);

        KeyBar bar21 = new();
        bar21.Keys01.Add(KeyReturn);
        bar21.Keys02.Add(KeyReturn);
        bar21.Keys03.Add(KeyAdd);
        bar21.Keys04.Add(KeyAdd);
        bar21.Keys05.Add(KeySubtract);
        keyBars.Add(bar21);

    }

    protected override void InitProperties()
    {
        //第一行按键
        KeyModelList.Add(KeyEscape);
        KeyModelList.Add(KeyF1);
        KeyModelList.Add(KeyF2);
        KeyModelList.Add(KeyF3);
        KeyModelList.Add(KeyF4);
        KeyModelList.Add(KeyF5);
        KeyModelList.Add(KeyF6);
        KeyModelList.Add(KeyF7);
        KeyModelList.Add(KeyF8);
        KeyModelList.Add(KeyF9);
        KeyModelList.Add(KeyF10);
        KeyModelList.Add(KeyF11);
        KeyModelList.Add(KeyF12);
        KeyModelList.Add(KeyPrintScreen);
        KeyModelList.Add(KeyScroll);
        KeyModelList.Add(KeyPause);

        //第二行按键
        KeyModelList.Add(KeyOem3);
        KeyModelList.Add(KeyD1);
        KeyModelList.Add(KeyD2);
        KeyModelList.Add(KeyD3);
        KeyModelList.Add(KeyD4);
        KeyModelList.Add(KeyD5);
        KeyModelList.Add(KeyD6);
        KeyModelList.Add(KeyD7);
        KeyModelList.Add(KeyD8);
        KeyModelList.Add(KeyD9);
        KeyModelList.Add(KeyD0);
        KeyModelList.Add(KeyOemMinus);
        KeyModelList.Add(KeyOemPlus);
        KeyModelList.Add(KeyBack);
        KeyModelList.Add(KeyInsert);
        KeyModelList.Add(KeyHome);
        KeyModelList.Add(KeyPageUp);
        KeyModelList.Add(KeyNumLock);
        KeyModelList.Add(KeyDivide);
        KeyModelList.Add(KeyMultiply);
        KeyModelList.Add(KeySubtract);

        //第三行按键
        KeyModelList.Add(KeyTab);
        KeyModelList.Add(KeyQ);
        KeyModelList.Add(KeyW);
        KeyModelList.Add(KeyE);
        KeyModelList.Add(KeyR);
        KeyModelList.Add(KeyT);
        KeyModelList.Add(KeyY);
        KeyModelList.Add(KeyU);
        KeyModelList.Add(KeyI);
        KeyModelList.Add(KeyO);
        KeyModelList.Add(KeyP);
        KeyModelList.Add(KeyOem4);
        KeyModelList.Add(KeyOem6);
        KeyModelList.Add(KeyOem5);
        KeyModelList.Add(KeyDelete);
        KeyModelList.Add(KeyEnd);
        KeyModelList.Add(KeyPageDown);
        KeyModelList.Add(KeyNumPad7);
        KeyModelList.Add(KeyNumPad8);
        KeyModelList.Add(KeyNumPad9);
        KeyModelList.Add(KeyAdd);


        //第四行按键
        KeyModelList.Add(KeyCapsLock);
        KeyModelList.Add(KeyA);
        KeyModelList.Add(KeyS);
        KeyModelList.Add(KeyD);
        KeyModelList.Add(KeyF);
        KeyModelList.Add(KeyG);
        KeyModelList.Add(KeyH);
        KeyModelList.Add(KeyJ);
        KeyModelList.Add(KeyK);
        KeyModelList.Add(KeyL);
        KeyModelList.Add(KeyOemSemicolon);
        KeyModelList.Add(KeyOemQuotes);
        KeyModelList.Add(KeyEnter);
        KeyModelList.Add(KeyNumPad4);
        KeyModelList.Add(KeyNumPad5);
        KeyModelList.Add(KeyNumPad6);

        //第五行按键
        KeyModelList.Add(KeyLeftShift);
        KeyModelList.Add(KeyZ);
        KeyModelList.Add(KeyX);
        KeyModelList.Add(KeyC);
        KeyModelList.Add(KeyV);
        KeyModelList.Add(KeyB);
        KeyModelList.Add(KeyN);
        KeyModelList.Add(KeyM);
        KeyModelList.Add(KeyOemComma);
        KeyModelList.Add(KeyOemPeriod);
        KeyModelList.Add(KeyOemQuestion);
        KeyModelList.Add(KeyRightShift);
        KeyModelList.Add(KeyModelUp);
        KeyModelList.Add(KeyNumPad1);
        KeyModelList.Add(KeyNumPad2);
        KeyModelList.Add(KeyNumPad3);
        KeyModelList.Add(KeyReturn);

        //第六行按键
        KeyModelList.Add(KeyLeftCtrl);
        KeyModelList.Add(KeyLWin);
        KeyModelList.Add(KeyLeftAlt);
        KeyModelList.Add(KeySpace);
        KeyModelList.Add(KeyRightAlt);
        KeyModelList.Add(KeyApps);
        KeyModelList.Add(KeyRightCtrl);
        KeyModelList.Add(KeyModelLeft);
        KeyModelList.Add(KeyModelDown);
        KeyModelList.Add(KeyModelRight);
        KeyModelList.Add(KeyNumPad0);
        KeyModelList.Add(KeyDecimal);


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


        CommandWriteCommand = new DelegateCommand<string>(CommandWrite, CanExcuteBootLoaderWrite)
          .ObservesProperty(() => IsDeviceConnect);
        CommandReadCommand = new DelegateCommand<string>(CommandRead, CanExcuteBootLoaderRead)
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

        StartSimulatingCommand = new DelegateCommand(StartSimulating, CanExcuteStartSimulating)
           .ObservesProperty(() => CanStartSimulating);
        StopSimulatingCommand = new DelegateCommand(StopSimulating, CanExcuteStopSimulating)
            .ObservesProperty(() => CanStartSimulating);
    }



    protected override void InitEvents()
    {
        Loaded += MainWindow_Loaded;
    }

    #endregion

    #region 事件

    private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {

        hook = new KeyboardHook();
        hook.OnKeyDown += Hook_OnKeyDown;
        hook.OnKeyUp += Hook_OnKeyUp;
        hook.Start();
        RefreshDevices();
        if (HidDeviceList != null && GCH.Device != null)
        {
            Device = HidDeviceList.FirstOrDefault(x => x.DevicePath == GCH.Device.DevicePath);
        }
        IsDeviceConnect = GCH.IsDeviceConnect;
    }

    private void Hook_OnKeyDown(object sender, KeyEventArgs e)
    {
        var models = KeyModelList.FindAll(x => x.Key == e.Key);
        System.Diagnostics.Trace.WriteLine(e.Key.ToString());
        if (models != null || models.Count > 0)
        {
            foreach (var m in models)
            {
                m.IsKeyPressed = true;
            }
        }
    }

    private void Hook_OnKeyUp(object sender, KeyEventArgs e)
    {
        var models = KeyModelList.FindAll(x => x.Key == e.Key);
        System.Diagnostics.Trace.WriteLine(e.Key.ToString());
        if (models != null || models.Count > 0)
        {
            foreach (var m in models)
            {
                m.IsKeyPressed = false;
            }
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        hook?.Stop();
        Environment.Exit(0);
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

    private bool CanExcuteStartSimulating()
    {
        return CanStartSimulating;
    }

    private bool CanExcuteStopSimulating()
    {
        return !CanStartSimulating;
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
    private void GetSpectrumData()
    {
        DateTime time = DateTime.Now;
        while (true)
        {
            if (CanStartRecording)
            {
                break;
            }
            if (DateTime.Now.Subtract(time).TotalMilliseconds >= 25)
            {
                time = DateTime.Now;
                double[] newSpectrumData = visualizerDataHelper.GetSpectrumData();         // 从可视化器中获取频谱数据
                newSpectrumData = VisualizerDataHelper.MakeSmooth(newSpectrumData, 2);                // 平滑频谱数据
                spectrumData = newSpectrumData;
                if (spectrumData.Any())
                {
                    List<double> data = spectrumData.ToList();
                    List<double> range = null;
                    for (int i = 0; i < FFTBars.Count; i++)
                    {
                        range = data.GetRange(3 * i, 3);
                        var hight = range.Max() * 50000;
                        hight = hight < 0 ? 0 : hight;
                        hight = hight > 500 ? 500 : hight;
                        keyBars[i].SetValue(hight);
                        FFTBars[i].Height = hight;
                    }
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

    Random random = new();
    private async void StartSimulating()
    {
        CanStartSimulating = false;

        var view = new CyclicRunningLightSettingsView();
        var settings = TianWeiToolsPro.Extensions.SerializerExtension.DeepCopyByBin(view.ShowView());

        List<Brush> brushes = new();

        byte[] rbg = new byte[3];
        for (int i = 0; i < settings.ColorCount; i++)
        {
            random.NextBytes(rbg);
            Color color = Color.FromRgb(rbg[0], rbg[1], rbg[2]);
            //构建 SolidColorBrush 需要UI 线程！！
            Brush brush = new SolidColorBrush(color);
            brushes.Add(brush);
        }

        var colors = brushes.ToArray();

        await Task.Run(() =>
        {

            var groupsList = animationKeyModel.GetSetAnimationGroupsList(settings.Columns, colors);

            while (true)
            {
                if (CanStartSimulating)
                {
                    break;
                }
                foreach (var group in groupsList)
                {
                    group.SetAnimation();
                    TimeHelper.Delay(settings.AnimationDuration);
                    group.ClearAnimation();
                    if (CanStartSimulating)
                    {
                        break;
                    }
                }
            }
        });

    }

    private void StopSimulating()
    {
        CanStartSimulating = true;
    }

    #endregion
}
