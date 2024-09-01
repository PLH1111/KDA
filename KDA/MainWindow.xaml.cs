using CefSharp;
using CyUSB;
using KDA.Audio;
using KDA.Controls;
using KDA.Hooks;
using KDA.Models;
using KDA.Models.Commands;
using KDA.Services;
using MahApps.Metro.Controls;
using NAudio.Wave;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TianWeiToolsPro.Commands;
using TianWeiToolsPro.Service;

namespace KDA
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow
    {
        #region 字段

        double[] spectrumData;

        KeyboardHook hook;

        WasapiLoopbackCapture capture;

        private CycleAnimationKeyModel animationKeyModel;

        private KeyBarList keyBars;

        private readonly VisualizerDataHelper visualizerDataHelper = new VisualizerDataHelper(256);

        readonly Random random = new Random();

        private KeyColorMapList KeyColorMaps { get; set; } = new KeyColorMapList(11);

        Protocol protocol = new Protocol();

        private readonly CefSharpExample cefSharpExample = new CefSharpExample();

        private ImageBrush imageBrush;

        #endregion

        #region 属性

        #region Key - 按键相关

        #region 第一行按键

        public KeyModel KeyEscape { get; set; } = new KeyModel(Key.Escape, "ESC");
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
        public KeyModel KeyPrintScreen { get; set; } = new KeyModel(Key.PrintScreen, "PrintScreen");
        public KeyModel KeyScroll { get; set; } = new KeyModel(Key.Scroll, "ScrollLock");
        public KeyModel KeyPause { get; set; } = new KeyModel(Key.Pause, "Pause");
        public KeyModel KeyVolumeMute { get; set; } = new KeyModel(Key.VolumeMute, "静音");
        public KeyModel KeyVolumeDown { get; set; } = new KeyModel(Key.VolumeDown, "音量减");
        public KeyModel KeyVolumeUp { get; set; } = new KeyModel(Key.VolumeUp, "音量+");
        public KeyModel KeyCalc { get; set; } = new KeyModel(Key.LaunchApplication1, "计算器");

        #endregion

        #region 第二行按键

        public KeyModel KeyOem3 { get; set; } = new KeyModel(Key.Oem3, "`");

        public KeyModel KeyD1 { get; set; } = new KeyModel(Key.D1, "1");

        public KeyModel KeyD2 { get; set; } = new KeyModel(Key.D2, "2");

        public KeyModel KeyD3 { get; set; } = new KeyModel(Key.D3, "3");

        public KeyModel KeyD4 { get; set; } = new KeyModel(Key.D4, "4");

        public KeyModel KeyD5 { get; set; } = new KeyModel(Key.D5, "5");

        public KeyModel KeyD6 { get; set; } = new KeyModel(Key.D6, "6");

        public KeyModel KeyD7 { get; set; } = new KeyModel(Key.D7, "7");

        public KeyModel KeyD8 { get; set; } = new KeyModel(Key.D8, "8");

        public KeyModel KeyD9 { get; set; } = new KeyModel(Key.D9, "9");

        public KeyModel KeyD0 { get; set; } = new KeyModel(Key.D0, "0");

        public KeyModel KeyOemMinus { get; set; } = new KeyModel(Key.OemMinus, "−");

        public KeyModel KeyOemPlus { get; set; } = new KeyModel(Key.OemPlus, "=");

        public KeyModel KeyBack { get; set; } = new KeyModel(Key.Back, "Backspace");

        public KeyModel KeyInsert { get; set; } = new KeyModel(Key.Insert);

        public KeyModel KeyHome { get; set; } = new KeyModel(Key.Home);

        public KeyModel KeyPageUp { get; set; } = new KeyModel(Key.PageUp);

        public KeyModel KeyNumLock { get; set; } = new KeyModel(Key.NumLock);

        public KeyModel KeyDivide { get; set; } = new KeyModel(Key.Divide, "/");

        public KeyModel KeyMultiply { get; set; } = new KeyModel(Key.Multiply, "×");

        public KeyModel KeySubtract { get; set; } = new KeyModel(Key.Subtract, "−");

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

        public KeyModel KeyOem4 { get; set; } = new KeyModel(Key.Oem4, "[");

        public KeyModel KeyOem6 { get; set; } = new KeyModel(Key.Oem6, "]");

        public KeyModel KeyOem5 { get; set; } = new KeyModel(Key.Oem5, "\\");

        public KeyModel KeyDelete { get; set; } = new KeyModel(Key.Delete);

        public KeyModel KeyEnd { get; set; } = new KeyModel(Key.End);

        public KeyModel KeyPageDown { get; set; } = new KeyModel(Key.PageDown, "PageDown");

        public KeyModel KeyNumPad7 { get; set; } = new KeyModel(Key.NumPad7, "7");

        public KeyModel KeyNumPad8 { get; set; } = new KeyModel(Key.NumPad8, "8");

        public KeyModel KeyNumPad9 { get; set; } = new KeyModel(Key.NumPad9, "9");

        public KeyModel KeyAdd { get; set; } = new KeyModel(Key.Add, "+");

        #endregion

        #region 第四行按键

        public KeyModel KeyCapsLock { get; set; } = new KeyModel(Key.CapsLock, "CapsLock");

        public KeyModel KeyA { get; set; } = new KeyModel(Key.A);

        public KeyModel KeyS { get; set; } = new KeyModel(Key.S);

        public KeyModel KeyD { get; set; } = new KeyModel(Key.D);

        public KeyModel KeyF { get; set; } = new KeyModel(Key.F);

        public KeyModel KeyG { get; set; } = new KeyModel(Key.G);

        public KeyModel KeyH { get; set; } = new KeyModel(Key.H);

        public KeyModel KeyJ { get; set; } = new KeyModel(Key.J);

        public KeyModel KeyK { get; set; } = new KeyModel(Key.K);

        public KeyModel KeyL { get; set; } = new KeyModel(Key.L);

        public KeyModel KeyOemSemicolon { get; set; } = new KeyModel(Key.OemSemicolon, ";");

        public KeyModel KeyOemQuotes { get; set; } = new KeyModel(Key.OemQuotes, "'");

        public KeyModel KeyEnter { get; set; } = new KeyModel(Key.Enter, "Enter");

        public KeyModel KeyNumPad4 { get; set; } = new KeyModel(Key.NumPad4, "4");

        public KeyModel KeyNumPad5 { get; set; } = new KeyModel(Key.NumPad5, "5");

        public KeyModel KeyNumPad6 { get; set; } = new KeyModel(Key.NumPad6, "6");


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
        public KeyModel KeyOemComma { get; set; } = new KeyModel(Key.OemComma, ",");
        public KeyModel KeyOemPeriod { get; set; } = new KeyModel(Key.OemPeriod, ".");
        public KeyModel KeyOemQuestion { get; set; } = new KeyModel(Key.OemQuestion, "/");
        public KeyModel KeyRightShift { get; set; } = new KeyModel(Key.RightShift);
        public KeyModel KeyModelUp { get; set; } = new KeyModel(Key.Up);
        public KeyModel KeyNumPad1 { get; set; } = new KeyModel(Key.NumPad1, "1");
        public KeyModel KeyNumPad2 { get; set; } = new KeyModel(Key.NumPad2, "2");
        public KeyModel KeyNumPad3 { get; set; } = new KeyModel(Key.NumPad3, "3");
        public KeyModel KeyReturn { get; set; } = new KeyModel(Key.Enter, "Enter");


        #endregion

        #region 第六行按键

        public KeyModel KeyLeftCtrl { get; set; } = new KeyModel(Key.LeftCtrl);

        public KeyModel KeyFn { get; set; } = new KeyModel(Key.FinalMode, "Fn");

        public KeyModel KeyLWin { get; set; } = new KeyModel(Key.LWin);

        public KeyModel KeyLeftAlt { get; set; } = new KeyModel(Key.LeftAlt);

        public KeyModel KeySpace { get; set; } = new KeyModel(Key.Space);

        public KeyModel KeyRWin { get; set; } = new KeyModel(Key.RWin);

        public KeyModel KeyRightAlt { get; set; } = new KeyModel(Key.RightAlt);

        //public KeyModel KeyApps { get; set; } = new KeyModel(Key.FinalMode);

        public KeyModel KeyRightCtrl { get; set; } = new KeyModel(Key.RightCtrl);

        public KeyModel KeyModelLeft { get; set; } = new KeyModel(Key.Left);

        public KeyModel KeyModelDown { get; set; } = new KeyModel(Key.Down);

        public KeyModel KeyModelRight { get; set; } = new KeyModel(Key.Right);

        public KeyModel KeyNumPad0 { get; set; } = new KeyModel(Key.NumPad0, "0");

        public KeyModel KeyDecimal { get; set; } = new KeyModel(Key.Decimal);

        #endregion

        public KeyModelList KeyModelList { get; set; } = new KeyModelList();

        #endregion

        #region Wave Peak

        public bool CanStartRecording { get; set; } = true;


        public static FFTBarList FFTBars { get; set; } = new FFTBarList(64);

        #endregion

        #region Anition Simulating

        public bool CanStartSimulating { get; set; } = true;

        #endregion

        #region CmdFlashModel

        public static CmdFlashModel CmdFlashModel { get; set; } = new CmdFlashModel();

        #endregion

        public MacroList MacroList { get; set; } = new MacroList();

        public Macro Macro { get; set; } = new Macro();

        #endregion

        #region 命令

        public DelegateCommand ShowDebugViewCommand { get; private set; }
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

            //webView.Source = new Uri($"{System.AppDomain.CurrentDomain.BaseDirectory}web/index.html");

            Browser.Address = $"{AppDomain.CurrentDomain.BaseDirectory}web/index.html";

            //去除右键
            Browser.MenuHandler = new MenuHandler();

            // 允许以同步的方式注册C#的对象到JS中
            Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            //CefSharpSettings.WcfEnabled = true;

            // 注册C#的对象到JS中的代码必须在Cef的Browser加载之前调用
            Browser.JavascriptObjectRepository.Register("cefSharpExample", cefSharpExample, false, BindingOptions.DefaultBinder);

            MacroComboBox.ItemsSource = MacroList.macros.Select(p => p.Name).ToList();

            cefSharpExample.ShowSetting = ResetSetting;

            logo1.Source = GetFirstImage("Logo");

            imageBrush = new ImageBrush();

            imageBrush.ImageSource = GetFirstImage("Background");

            SolidColorBrush solidBrush = Brushes.Gray;

            if (imageBrush.ImageSource != null)
            {
                this.SetCurrentValue(BackgroundProperty, imageBrush);
            }
        }

        private ImageSource GetFirstImage(string folderName)
        {
            var path = new DirectoryInfo($"{Environment.CurrentDirectory}\\{folderName}");

            var files = path.GetFiles();

            foreach (var item in files)
            {
                if (item.Extension.IsInIgnoreCase(".jpg", ".png"))
                {
                    var uri = new Uri(item.FullName);

                    return new BitmapImage(uri);
                }
            }

            return null;
        }

        protected void InitFields()
        {
            InitAnimationKeyGroups();
            InitKeyBars();
        }

        private void InitAnimationKeyGroups()
        {

            animationKeyModel = new CycleAnimationKeyModel()
        {

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
            KeyFn,

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

            KeyTab,
            KeyQ,

            KeyOem3,
            KeyD1,

        };
        }

        private void InitKeyBars()
        {
            keyBars = new KeyBarList();

            KeyBar bar01 = new KeyBar();
            bar01.Keys01.Add(KeyLeftCtrl);
            bar01.Keys01.Add(KeyFn);
            bar01.Keys02.Add(KeyLeftShift);
            bar01.Keys03.Add(KeyCapsLock);
            bar01.Keys04.Add(KeyTab);
            bar01.Keys05.Add(KeyOem3);
            keyBars.Add(bar01);

            KeyBar bar02 = new KeyBar();
            bar02.Keys01.Add(KeyLWin);
            bar02.Keys02.Add(KeyZ);
            bar02.Keys03.Add(KeyA);
            bar02.Keys04.Add(KeyQ);
            bar02.Keys05.Add(KeyD1);
            keyBars.Add(bar02);

            KeyBar bar03 = new KeyBar();
            bar03.Keys01.Add(KeyLeftAlt);
            bar03.Keys02.Add(KeyX);
            bar03.Keys03.Add(KeyS);
            bar03.Keys04.Add(KeyW);
            bar03.Keys05.Add(KeyD2);
            keyBars.Add(bar03);

            KeyBar bar04 = new KeyBar();
            bar04.Keys01.Add(KeySpace);
            bar04.Keys02.Add(KeyC);
            bar04.Keys03.Add(KeyD);
            bar04.Keys04.Add(KeyE);
            bar04.Keys05.Add(KeyD3);
            keyBars.Add(bar04);

            KeyBar bar05 = new KeyBar();
            bar05.Keys01.Add(KeySpace);
            bar05.Keys02.Add(KeyV);
            bar05.Keys03.Add(KeyF);
            bar05.Keys04.Add(KeyR);
            bar05.Keys05.Add(KeyD4);
            keyBars.Add(bar05);

            KeyBar bar06 = new KeyBar();
            bar06.Keys01.Add(KeySpace);
            bar06.Keys02.Add(KeyB);
            bar06.Keys03.Add(KeyG);
            bar06.Keys04.Add(KeyT);
            bar06.Keys05.Add(KeyD5);
            keyBars.Add(bar06);

            KeyBar bar07 = new KeyBar();
            bar07.Keys01.Add(KeySpace);
            bar07.Keys02.Add(KeyN);
            bar07.Keys03.Add(KeyH);
            bar07.Keys04.Add(KeyY);
            bar07.Keys05.Add(KeyD6);
            keyBars.Add(bar07);

            KeyBar bar08 = new KeyBar();
            bar08.Keys01.Add(KeySpace);
            bar08.Keys02.Add(KeyM);
            bar08.Keys03.Add(KeyJ);
            bar08.Keys04.Add(KeyU);
            bar08.Keys05.Add(KeyD7);
            keyBars.Add(bar08);

            KeyBar bar09 = new KeyBar();
            bar09.Keys01.Add(KeySpace);
            bar09.Keys02.Add(KeyOemComma);
            bar09.Keys03.Add(KeyK);
            bar09.Keys04.Add(KeyI);
            bar09.Keys05.Add(KeyD8);
            keyBars.Add(bar09);

            KeyBar bar10 = new KeyBar();
            bar10.Keys01.Add(KeyRightAlt);
            bar10.Keys02.Add(KeyOemPeriod);
            bar10.Keys03.Add(KeyL);
            bar10.Keys04.Add(KeyO);
            bar10.Keys05.Add(KeyD9);
            keyBars.Add(bar10);

            KeyBar bar11 = new KeyBar();
            bar11.Keys01.Add(KeyFn);
            bar11.Keys02.Add(KeyOemQuestion);
            bar11.Keys03.Add(KeyOemSemicolon);
            bar11.Keys04.Add(KeyP);
            bar11.Keys05.Add(KeyD0);
            keyBars.Add(bar11);

            KeyBar bar12 = new KeyBar();
            bar12.Keys01.Add(KeyRightCtrl);
            bar12.Keys02.Add(KeyRightShift);
            bar12.Keys03.Add(KeyOemQuotes);
            bar12.Keys03.Add(KeyEnter);
            bar12.Keys04.Add(KeyOem4);
            bar12.Keys05.Add(KeyOemMinus);
            keyBars.Add(bar12);

            KeyBar bar13 = new KeyBar();
            bar13.Keys01.Add(KeyRightCtrl);
            bar13.Keys02.Add(KeyRightShift);
            bar13.Keys03.Add(KeyOemQuotes);
            bar13.Keys03.Add(KeyEnter);
            bar13.Keys04.Add(KeyOem4);
            bar13.Keys05.Add(KeyOemMinus);
            keyBars.Add(bar13);

            KeyBar bar14 = new KeyBar();
            bar14.Keys01.Add(KeyRightCtrl);
            bar14.Keys02.Add(KeyRightShift);
            bar14.Keys03.Add(KeyOemQuotes);
            bar14.Keys03.Add(KeyEnter);
            bar14.Keys04.Add(KeyOem5);
            bar14.Keys05.Add(KeyBack);
            keyBars.Add(bar14);

            KeyBar bar15 = new KeyBar();
            bar15.Keys01.Add(KeyModelLeft);
            bar15.Keys04.Add(KeyDelete);
            bar15.Keys05.Add(KeyInsert);
            keyBars.Add(bar15);

            KeyBar bar16 = new KeyBar();
            bar16.Keys01.Add(KeyModelDown);
            bar16.Keys02.Add(KeyModelUp);
            bar16.Keys04.Add(KeyEnd);
            bar16.Keys05.Add(KeyHome);
            keyBars.Add(bar16);

            KeyBar bar17 = new KeyBar();
            bar17.Keys01.Add(KeyModelRight);
            bar17.Keys04.Add(KeyPageDown);
            bar17.Keys05.Add(KeyPageUp);
            keyBars.Add(bar17);

            KeyBar bar18 = new KeyBar();
            bar18.Keys01.Add(KeyNumPad0);
            bar18.Keys02.Add(KeyNumPad1);
            bar18.Keys03.Add(KeyNumPad4);
            bar18.Keys04.Add(KeyNumPad7);
            bar18.Keys05.Add(KeyNumLock);
            keyBars.Add(bar18);

            KeyBar bar19 = new KeyBar();
            bar19.Keys01.Add(KeyNumPad0);
            bar19.Keys02.Add(KeyNumPad2);
            bar19.Keys03.Add(KeyNumPad5);
            bar19.Keys04.Add(KeyNumPad8);
            bar19.Keys05.Add(KeyDivide);
            keyBars.Add(bar19);

            KeyBar bar20 = new KeyBar();
            bar20.Keys01.Add(KeyDecimal);
            bar20.Keys02.Add(KeyNumPad3);
            bar20.Keys03.Add(KeyNumPad6);
            bar20.Keys04.Add(KeyNumPad9);
            bar20.Keys05.Add(KeyMultiply);
            keyBars.Add(bar20);

            KeyBar bar21 = new KeyBar();
            bar21.Keys01.Add(KeyReturn);
            bar21.Keys02.Add(KeyReturn);
            bar21.Keys03.Add(KeyAdd);
            bar21.Keys04.Add(KeyAdd);
            bar21.Keys05.Add(KeySubtract);
            keyBars.Add(bar21);

        }

        protected void InitProperties()
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
            KeyModelList.Add(KeyVolumeMute);
            KeyModelList.Add(KeyVolumeDown);
            KeyModelList.Add(KeyVolumeUp);
            KeyModelList.Add(KeyCalc);

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
            KeyModelList.Add(KeyRWin);
            KeyModelList.Add(KeyFn);
            KeyModelList.Add(KeyRightCtrl);
            KeyModelList.Add(KeyModelLeft);
            KeyModelList.Add(KeyModelDown);
            KeyModelList.Add(KeyModelRight);
            KeyModelList.Add(KeyNumPad0);
            KeyModelList.Add(KeyDecimal);
        }

        protected void InitCommands()
        {
            ShowDebugViewCommand = new DelegateCommand(ShowDebugView);

            StartRecordingCommand = new DelegateCommand(StartRecording, CanExcuteStartRecording)
                .ObservesProperty(() => CanStartRecording);
            StopRecordingCommand = new DelegateCommand(StopRecording, CanExcuteStopRecording)
                .ObservesProperty(() => CanStartRecording);

            StartSimulatingCommand = new DelegateCommand(StartSimulating, CanExcuteStartSimulating)
               .ObservesProperty(() => CanStartSimulating);
            StopSimulatingCommand = new DelegateCommand(StopSimulating, CanExcuteStopSimulating)
                .ObservesProperty(() => CanStartSimulating);
        }

        protected void InitEvents()
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

            InitUsbMonitor();
            RefreshDevices();
            
            ListView_SelectionChanged(null, null);//L.A添加
        }

        private void Hook_OnKeyDown(object sender, KeyEventArgs e)
        {
            var models = KeyModelList.FindAll(x => x.Key == e.Key);
            var keyStr = e.Key.ToString();
            System.Diagnostics.Trace.WriteLine(keyStr);

            //MacroManageInput.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, keyStr);

            if (models != null || models.Count > 0)
            {
                foreach (var m in models)
                {
                    m.IsKeyPressed = true;
                }
            }

            KeyInput.SetCurrentValue(TextBox.TextProperty, e.Key.ToString());

            if (CheckBox1.IsChecked == true)
            {
                Macro.MacroContents.Add(new MacroContent(e.Key));
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

        private void ColorPicker_SelectedColorChanged(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {

        }

        private void MenuItemMacro_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var contextMenu = (ContextMenu)item.Parent;
            var keyControl = (KeyControl)contextMenu.PlacementTarget;
            var key = keyControl.KeyModel;

            MacroEditWindow macroEditWindow = new MacroEditWindow(key, MacroList);
            macroEditWindow.ShowDialog();
        }

        private void MenuItemKey_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var contextMenu = (ContextMenu)item.Parent;
            var keyControl = (KeyControl)contextMenu.PlacementTarget;
            var key = keyControl.KeyModel;

            KeyEditWindow keyEditWindow = new KeyEditWindow(key);
            hook.OnKeyDown += keyEditWindow.Hook_OnKeyDown;
            keyEditWindow.ShowDialog();
        }

        private void ButtonAddMacroData_Click(object sender, RoutedEventArgs e)
        {
            //if(Enum.TryParse(typeof(Key), MacroManageInput.Text, out var key))
            //{
            //    Macro.MacroContents.Add(new MacroContent((Key)key));
            //}
        }

        private void ButtonSetMacroData_Click(object sender, RoutedEventArgs e)
        {
            ACH.SetMarcoMap(Macro);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1 == null || List1.SelectedItem == null) { return; }
            if (PreviewSliderSpeed == null || PreviewSliderLight == null || List2 == null) { return; }
            var id = (AnimationId)List1.SelectedIndex;//L.A修改
            AnimationModel model = new AnimationModel()
            {
                AnimationId = (AnimationIds)(byte)id,
                Speed = (byte)PreviewSliderSpeed.Value,
                ColorA = (byte)PreviewSliderLight.Value,
                Direction = (ColorNum)List2.SelectedIndex,
            };
            ColorSelect.SetCurrentValue(VisibilityProperty, id == AnimationId.游戏模式 ? Visibility.Visible : Visibility.Collapsed);
            colorLabel.SetCurrentValue(VisibilityProperty, id == AnimationId.游戏模式 ? Visibility.Collapsed : Visibility.Visible);
            List2.SetCurrentValue(VisibilityProperty, id == AnimationId.游戏模式 ? Visibility.Collapsed : Visibility.Visible);

            //ACH.SetAnimation(model);
            ////pyh 20240311
            InvokeJs(new UseDataObject() {ledNum = (int)model.AnimationId, color = (int)model.Direction, brightness = model.ColorA, speed = model.Speed });
        }

        private void MenuItemDefult_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var contextMenu = (ContextMenu)item.Parent;
            var keyControl = (KeyControl)contextMenu.PlacementTarget;
            var keyIndex = (byte)KeyMap.GetKeyIndex(keyControl.KeyModel.Key);
            KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.NormalKey, 0);

            ACH.SetKeyMacro(model);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ACH.ResetDefaultSetting();
        }

        private void PreviewSliderLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (List1 == null || List1.SelectedItem == null) { return; }
            //if (PreviewSliderSpeed == null || PreviewSliderLight == null || List2 == null) { return; }
            //var id = (AnimationId)List1.SelectedIndex;//L.A修改
            //AnimationModel model = new AnimationModel()
            //{
            //    AnimationId = (AnimationIds)(byte)id,
            //    Speed = (byte)PreviewSliderSpeed.Value,
            //    ColorA = (byte)PreviewSliderLight.Value,
            //    Direction = (ColorNum)List2.SelectedIndex,
            //};
            //ACH.SetAnimation(model);
        }

        private void PreviewSliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (List1 == null || List1.SelectedItem == null) { return; }
            //if (PreviewSliderSpeed == null || PreviewSliderLight == null || List2 == null) { return; }
            //var id = (AnimationId)List1.SelectedIndex;//L.A修改
            //AnimationModel model = new AnimationModel()
            //{
            //    AnimationId = (AnimationIds)(byte)id,
            //    Speed = (byte)PreviewSliderSpeed.Value,
            //    ColorA = (byte)PreviewSliderLight.Value,
            //    Direction = (ColorNum)List2.SelectedIndex,
            //};

            //ACH.SetAnimation(model);
        }

        private void ButtonReadMacro_Click(object sender, RoutedEventArgs e)
        {
            MacroList.Refersh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (usbDevices != null)
            {
                usbMonitor.DeviceRemoved -= UsbDevices_DeviceRemoved;
                usbMonitor.DeviceAttached -= UsbDevices_DeviceAttached;
                //usbMonitor.Dispose();
                usbMonitor = null;
                GC.Collect();
            }
        }

        #endregion

        #region 方法

        #region Can Excute

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

        private void ShowDebugView()
        {
            var view = new SettingView();
            view.ShowDialog();
        }

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

        private void GetSpectrumData()
        {
            DateTime time = DateTime.Now;
            while (true)
            {
                if (CanStartRecording)
                {
                    for (int i = 0; i < keyBars.Count; i++)
                    {
                        keyBars[i].SetValue(0);
                    }
                    for (int i = 0; i < FFTBars.Count; i++)
                    {
                        FFTBars[i].Height = 0;
                    }
                    break;
                }
                if (DateTime.Now.Subtract(time).TotalMilliseconds >= 20)
                {
                    time = DateTime.Now;
                    double[] newSpectrumData = visualizerDataHelper.GetSpectrumData();         // 从可视化器中获取频谱数据
                    newSpectrumData = VisualizerDataHelper.MakeSmooth(newSpectrumData, 2);                // 平滑频谱数据
                    spectrumData = newSpectrumData;
                    if (spectrumData.Any())
                    {
                        List<double> data = spectrumData.ToList();
                        List<double> range;
                        for (int i = 0; i < keyBars.Count; i++)
                        {
                            range = data.GetRange(6 * i, 6);
                            var hight = range.Max() * 30000;
                            hight = hight < 0 ? 0 : hight;
                            hight = hight > 300 ? 300 : hight;
                            keyBars[i].SetValue(hight);
                        }

                        for (int i = 0; i < FFTBars.Count; i++)
                        {
                            range = data.GetRange(2 * i, 2);
                            var hight = range.Max() * 30000;
                            hight = hight < 0 ? 0 : hight;
                            hight = hight > 300 ? 300 : hight;
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
                NoticeBoxService.ShowError(ex.Message);
            }

        }

        private async void StartSimulating()
        {
            CanStartSimulating = false;

            var view = new CyclicRunningLightSettingsView();
            var settings = view.ShowView();

            List<Color> brushes = new List<Color>();

            Color[] colors = new Color[settings.ColorCount + 1];
            if (settings.IsAutoColor)
            {
                byte[] rbg = new byte[3];
                for (int i = 1; i < settings.ColorCount + 1; i++)
                {
                    random.NextBytes(rbg);
                    Color color = Color.FromRgb(rbg[0], rbg[1], rbg[2]);
                    colors[i] = color;
                }

            }
            else
            {
                colors = settings.CustomColors.GetBrushes();
            }


            KeyColorDataList keyColorDatas = new KeyColorDataList();

            await Task.Run(() =>
            {

                var groupsList = animationKeyModel.GetSetAnimationGroupsList(settings.Columns, colors);

                if (groupsList == null)
                {
                    return;
                }

                while (true)
                {
                    if (CanStartSimulating)
                    {
                        break;
                    }
                    foreach (var group in groupsList)
                    {
                        group.SetAnimation();
                        var datas = KeyColorMaps.SetColorDatas(group);
                        keyColorDatas.AddRange(TianWeiToolsPro.Extensions.SerializerExtension.DeepCopyByXml(datas));
                        TimeHelper.Delay(settings.AnimationDuration);
                        datas?.ClearColorDatas();
                        if (CanStartSimulating)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }

        private void StopSimulating()
        {
            CanStartSimulating = true;
        }

        private void ResetSetting()
        {
            Application.Current.Invoke(new Action(() => { ButtonComboBox.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedIndexProperty, 0); }));
        }

        #endregion

        #region Usb Driver Control

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
            //Growl.Info("键盘插入");
        }

        void UsbDevices_DeviceRemoved(object sender, EventArgs e)
        {
            RefreshDevices();
            //Growl.Info("键盘拔出");

            IsDeviceConnect = false;
        }

        private async void RefreshDevices()
        {
            Device = null;
            GCH.Device = null;
            IsDeviceConnect = false;
            await Task.Run(() =>
            {
                usbDevices = new USBDeviceList(CyConst.DEVICES_HID);
            });
            if (usbDevices != null && usbDevices.Count > 0)
            {
                HidDeviceList.Clear();
                foreach (var device in usbDevices)
                {
                    //if (device is CyHidDevice hid )
                    if (device is CyHidDevice hid && hid.Outputs.RptByteLen == 64)
                    {
                        HidDeviceModel model = new HidDeviceModel(hid.Manufacturer,
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

                        Device = model;
                    }
                }

                if (HidDeviceList.Count > 0)
                {
                    ConncetDevice();
                }
            }
        }

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
                //L.A添加第一次进入设置页面默认选择Button相关页面
                if (IsDeviceConnect && MainTimes == 0) { MainTimes++; StackPanelButton_MouseDown(null, null); }
            }

            //if(IsDeviceConnect) Growl.Success("连接成功");
            //IsDeviceConnect = GCH.OpenDevice();
        }

        public ObservableCollection<HidDeviceModel> HidDeviceList { get; set; } = new ObservableCollection<HidDeviceModel>();
        public HidDeviceModel Device { get; set; }
        public bool IsDeviceConnect { get; set; } = true;
        public int MainTimes { get; set; } = 0;

        private void OnIsDeviceConnectChanged()
        {
            if (IsDeviceConnect)
            {
                if (imageBrush.ImageSource != null)
                {
                    this.SetCurrentValue(BackgroundProperty, imageBrush);
                }
            }
            else
            {
                this.SetCurrentValue(BackgroundProperty, Brushes.Gray);
            }
        }

        USBDeviceList usbDevices;
        USBDeviceList usbMonitor;

        #endregion
        BrushConverter brushConverter = new BrushConverter();

        private void ButtonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (label1 == null) return;

            switch (ButtonComboBox.SelectedIndex)
            {
                case 0:
                    label1.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    MacroComboBox.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    label2.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    KeyInput.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    break;
                case 1:
                    label1.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    MacroComboBox.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    label2.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                    KeyInput.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                    KeyInput.SetCurrentValue(TextBox.TextProperty, "");
                    break;
                case 2:
                    label1.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                    MacroComboBox.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                    label2.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    KeyInput.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                    break;
            }
        }

        private void MacroComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StackPanelButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelButton.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            StackPanelLight.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelMacro.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelRestore.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));

            ButtonIco.SetCurrentValue(TextBlock.ForegroundProperty, Brushes.Black);
            LightIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            MacroIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            RestoreIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));

            ButtonSetting.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            LightSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            MacroSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);

            CheckBox1.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, false);
        }

        private void StackPanelLight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelButton.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelLight.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            StackPanelMacro.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelRestore.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));

            ButtonIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            LightIco.SetCurrentValue(TextBlock.ForegroundProperty, Brushes.Black);
            MacroIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            RestoreIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));

            ButtonSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            LightSetting.SetCurrentValue(VisibilityProperty, Visibility.Visible);
            MacroSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);

            CheckBox1.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, false);
        }

        private void StackPanelMacro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelButton.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelLight.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelMacro.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            StackPanelRestore.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));

            ButtonIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            LightIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            MacroIco.SetCurrentValue(TextBlock.ForegroundProperty, Brushes.Black);
            RestoreIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));

            ButtonSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            LightSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            MacroSetting.SetCurrentValue(VisibilityProperty, Visibility.Visible);

            CheckBox1.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, false);

        }

        private void StackPanelRestore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelButton.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelLight.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelMacro.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#3a3a3a"));
            StackPanelRestore.SetCurrentValue(Panel.BackgroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));

            ButtonIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            LightIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            MacroIco.SetCurrentValue(TextBlock.ForegroundProperty, (Brush)brushConverter.ConvertFromString("#e4002b"));
            RestoreIco.SetCurrentValue(TextBlock.ForegroundProperty, Brushes.Black);

            ButtonSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            LightSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            MacroSetting.SetCurrentValue(VisibilityProperty, Visibility.Collapsed);

            CheckBox1.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, false);

            MessageBoxResult result = MessageBoxResult.Yes;
            if (_LACurLang == "EN")
            {
                result = HandyControl.Controls.MessageBox.Show("Factory reset will clear all data. Do you want to continue?", "Factory Reset", MessageBoxButton.YesNo, MessageBoxImage.Question);
            } 
            else if (_LACurLang == "CN") 
            {
                result = HandyControl.Controls.MessageBox.Show("出厂重置将清除所有数据, 是否要继续?", "出厂复位", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
            if (result == MessageBoxResult.Yes)
            {
                ACH.ResetDefaultSetting();
            }
        }

        private void ButtonSetButton_Click(object sender, RoutedEventArgs e)
        {
            var keyIndex = cefSharpExample.KeyIndex;//js过来的按键index

            if (ButtonComboBox.SelectedIndex == 0)
            {
                KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.NormalKey, 0);

                ACH.SetKeyMacro(model);
            }
            else if (ButtonComboBox.SelectedIndex == 1)
            {
                var keyCode = KeyMap.GetKeyValue(KeyInput.Text);

                KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.UserKey, keyCode);

                ACH.SetKeyMacro(model);
            }
            else if (ButtonComboBox.SelectedIndex == 2)
            {
                KeyMacroModel model = new KeyMacroModel(keyIndex, KeyModes.MacroKey, (byte)(MacroComboBox.SelectedIndex + 1));

                ACH.SetKeyMacro(model);
            }
        }

        private void List2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (List1 == null || List1.SelectedItem == null) { return; }
            //if (PreviewSliderSpeed == null || PreviewSliderLight == null || List2 == null) { return; }
            //var id = (AnimationId)List1.SelectedIndex;//L.A修改
            //AnimationModel model = new AnimationModel()
            //{
            //    AnimationId = (AnimationIds)(byte)id,
            //    Speed = (byte)PreviewSliderSpeed.Value,
            //    ColorA = (byte)PreviewSliderLight.Value,
            //    Direction = (ColorNum)List2.SelectedIndex,
            //};

            //ACH.SetAnimation(model);
        }

        private void ButtonSelectColor_Click(object sender, RoutedEventArgs e)
        {
            if (List1 == null || List1.SelectedItem == null) { return; }
            if (PreviewSliderSpeed == null || PreviewSliderLight == null || List2 == null) { return; }
            var id = (AnimationId)List1.SelectedIndex;//L.A修改
            AnimationModel model = new AnimationModel()
            {
                AnimationId = (AnimationIds)(byte)id,
                Speed = (byte)PreviewSliderSpeed.Value,
                ColorA = (byte)PreviewSliderLight.Value,
                Direction = (ColorNum)List2.SelectedIndex,
            };


            ACH.SetAnimation(model);

            if (id == AnimationId.游戏模式)
            {
                Thread.Sleep(200);
                var keyIndex = cefSharpExample.KeyIndex;

                var selectColor = ColorPicker1.SelectedColor.Value;

                KeyColorModel keyColor = new KeyColorModel(keyIndex, selectColor.R, selectColor.G, selectColor.B, 0) { };
                ACH.SetKeyColor(keyColor);
            }
            else
            {
            }
        }

        //pyh 20240311
        private void InvokeJs(UseDataObject data)
        {
            //L.A修改，调用web函数传递参数
            var jsCode = $"setUserData('{data.ledNum},{data.color},{data.index},{data.brightness},{data.speed}')";
            if (Browser == null) return;
            if (!Browser.IsBrowserInitialized) return;
            Browser.ExecuteScriptAsync(jsCode);
        }

        //pyh 20240311
        class UseDataObject
        {
            public int ledNum { get; set; }
            public int color { get; set; }
            public int index { get; set; }
            public int brightness { get; set; }
            public int speed { get; set; }

        }


        #region L.A新增属性和方法
        public static string _LACurLang = "EN";

        public void LAButton_Click(object sender, RoutedEventArgs e)
        {
            string langName = "";
            if (_LACurLang == "CN") 
            {
                langName = "LanguageDictionaryEN";
                _LACurLang = "EN";
                LABtnLang.Content = "中文";
            }
            else if (_LACurLang == "EN") 
            {
                langName = "LanguageDictionaryCN";
                _LACurLang = "CN";
                LABtnLang.Content = "EN";
            }
            ResourceDictionary langRd = null;
            try
            {
                //根据名字载入语言文件
                langRd = Application.LoadComponent(new Uri(@"Resources\" + langName + ".xaml", UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
            if (langRd != null)
            {
                //如果已使用其他语言,先清空
                if (Resources.MergedDictionaries.Count > 0)
                {
                    Resources.MergedDictionaries.Clear();
                }
                Resources.MergedDictionaries.Add(langRd);
            }
            MacroList.LALangRefersh();//宏列表也进行刷新

            //设置背景图片和颜色
            OnIsDeviceConnectChanged();
        }

        #endregion
    }

    public class CefSharpExample
    {
        public byte KeyIndex { get; set; }

        public string KeyName { get; set; }

        public Action ShowSetting { get; set; }

        public void TestMethod(string name)
        {
            KeyName = name;

            Debug.WriteLine(name);

            KeyIndex = KeyMap.GetKeyIndex(name);

            ShowSetting();
        }
    }

    /// <summary>
    /// cef菜单事件
    /// </summary>
    public class MenuHandler : CefSharp.IContextMenuHandler
    {

        void CefSharp.IContextMenuHandler.OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
        {
            model.Clear();
        }

        bool CefSharp.IContextMenuHandler.OnContextMenuCommand(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags)
        {
            //throw new NotImplementedException();
            return false;
        }

        void CefSharp.IContextMenuHandler.OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame)
        {
            //throw new NotImplementedException();
        }

        bool CefSharp.IContextMenuHandler.RunContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback)
        {
            return false;
        }
    }

    public class BoolToNoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            else
            {
                if ((bool)value)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static class Extensions
    {
        public static bool IsInIgnoreCase(this string source, params string[] list)
        {
            if (null == source) return false;

            IEnumerable<string> en = list.Where(i => string.Compare(i, source, StringComparison.OrdinalIgnoreCase) == 0);
            return en.Count() == 0 ? false : true;
        }
    }
}
