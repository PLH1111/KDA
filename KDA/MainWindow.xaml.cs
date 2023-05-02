global using PropertyChanged;
using KDA.Hooks;
using KDA.Models;
using MahApps.Metro.Controls;
using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace KDA;


[AddINotifyPropertyChangedInterface]
public partial class MainWindow : MetroWindow
{

    #region 字段

    KeyboardHook hook;

    #endregion


    #region 属性

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

    #region 命令

    public DelegateCommand CloseApplicationCommand { get; private set; }

    public DelegateCommand MinimizeWindowCommand { get; private set; }


    #endregion

    #region 初始化

    public MainWindow()
    {
        InitializeComponent();
        InitFields();
        InitProperties();
        InitCommands();
        InitEvents();
        Loaded += MainWindow_Loaded;
        DataContext = this;
    }

    private void InitFields()
    {

    }


    private void InitProperties()
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


    private void InitCommands()
    {
        CloseApplicationCommand = new DelegateCommand(CloseApplication);
        MinimizeWindowCommand = new DelegateCommand(MinimizeWindow);
    }



    private void InitEvents()
    {


    }

    #endregion

    #region 事件

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        hook = new KeyboardHook();
        hook.OnKeyDown += Hook_OnKeyDown;
        hook.OnKeyUp += Hook_OnKeyUp;
        hook.Start();
    }

    private void Hook_OnKeyDown(object sender, KeyEventArgs e)
    {
        var models = KeyModelList.FindAll(x => x.Key == e.Key);
        System.Diagnostics.Trace.WriteLine(e.Key.ToString());
        if (models != null || models.Count > 0)
        {
             foreach (var m in models)
            {
                m.IsPressed = true;
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
                m.IsPressed = false;
            }
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        hook?.Stop();
    }


    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    #endregion

    #region 方法

    private void CloseApplication()
    {
        Close();
        
    }


    private void MinimizeWindow()
    {
        SetCurrentValue(WindowStateProperty, WindowState.Minimized);
    }

    #endregion


}
