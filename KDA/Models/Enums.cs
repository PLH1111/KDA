using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;

public enum ResponseCodes : byte
{
    Success,
    InvalidCommand = 0x41,
    ParameterError,
    AddressError,
    SizeError,
    TBA,
}

public enum AnimationIds : byte
{
    Off,
    Solid,
    Breathing,
    Reactive,
    Rain,
    Gradient,
    Fade,
    Ripple,
    Wave
}

public enum AnimationDisplays : byte
{
    ShowRGB,
    Rainbow,
    Random,
    Divide0x10 = 0x10,
    Divide0x11 = 0x11,
    Divide0x12 = 0x12,
    Divide0x13,
    Divide0x14,
    Divide0x15,
    Divide0x16,
    Divide0x17,
    Divide0x18,
    Divide0x19,
    Divide0x1A,
    Divide0x1B,
    Divide0x1C,
    Divide0x1D,
    Divide0x1E,
    Divide0x1F,
}

public enum AnimationDirections : byte
{
    LeftToRight = 0x01,
    RightToLeft,
    TopToBottom,
    BottomToTop
}

public enum KeyBoardLanguages
{
    None,
    US = 0x01,
    UK,
    JP,
    KR,
}

public enum KeyCommandNames
{
    Model,
    Sleep,
    Key_Macro,
    Key_RBG,
    Animation,
    Profile,
    RBG_Map,
    Language,
    Macro_Data,
    Profile_Data,
    BootUp,
    Flash_Data,
    Reset_Default,
}

public enum KeyCommandAcceess
{
    ReadWrite, 
    WriteOnly, 
    ReadOnly
}

public enum KeyModes
{
    NormalKey = 0x01,
    UserKey,
    MacroKey,
}

public enum KeyMarcoModes
{
    KeyMakeThenRelease = 0x01,
    KeyMake,
    KeyRelease,
}


public enum SleepTimes : byte
{
    /// <summary>关闭</summary>
    Disable = 0x00,

    /// <summary>10分钟</summary>
    Minutes10 = 0x01,

    /// <summary>15分钟</summary>
    Minutes15 = 0x02,

    /// <summary>30分钟</summary>
    Minutes30 = 0x03,

    /// <summary>45分钟</summary>
    Minutes45 = 0x04,

    /// <summary>60分钟</summary>
    Minutes60 = 0x05,

    /// <summary>90分钟</summary>
    Minutes90 = 0x06,

    /// <summary>120分钟</summary>
    Minutes120 = 0x07,
}


public enum LightingModes : byte
{
    Black = 0x00,
    Breathing = 0x01
}


public enum PeakProviders
{
    Max,
    Rms,
    Sampling,
    Average
}
