using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;


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


[AddINotifyPropertyChangedInterface]
public class SleepModel
{
    public SleepTimes SleepTime { get; set; }

    public LightingModes SleepMode { get; set; }

    public SleepModel()
    {

    }

    public SleepModel(SleepTimes sleepTime, LightingModes sleepMode)
    {
        SleepTime = sleepTime;
        SleepMode = sleepMode;
    }
}
