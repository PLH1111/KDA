using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models.Commands;


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
