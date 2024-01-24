using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models
{
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
        STATIC = 0,
        SINGLE_ON = 1,// 单点亮
        SINGLE_OFF = 2,// 单熄灭
        PIANO = 3,// 随波逐流
        VORTEX = 4,// 峰回路转
        TIDE = 5,// 多彩纵横
        SNOW = 6,// 漫天飞雪
        STAR = 7,// 繁星点点
        DIMPLE = 8,// 涟漪扩散
        TOUCH = 9,// 一触即发
        BIRD = 10,// 一石二鸟
        FLOWER = 11,// 百花争艳
        FOUNTAIN = 12,// 彩泉涌动
        BREATHE = 13,// 动态呼吸
        SPECTRUM = 14,// 光谱循环
        WAVEBAND = 15,// 重峦叠嶂
        FLOW = 16,// 川流不息
        WAVE_LIGHT = 17,// 斜风细雨
        WAVE = 18,// 左右穿梭
        DEFINE = 19,
        DEFINE1 = 20,
        DEFINE2 = 21,
        POWER_UP = 22,
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

    public enum ColorNum : byte
    {
        七彩, 红, 绿, 蓝, 黄, 紫, 青, 白
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
        Key_RBG_Random,
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
}
