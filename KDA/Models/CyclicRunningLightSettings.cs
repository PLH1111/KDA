using System;

namespace KDA.Models;

[Serializable]
public class CyclicRunningLightSettings
{
    public int Columns { get; set; } = 7;

    public int ColorCount { get; set; } = 3;

    public int AnimationDuration { get; set; } = 25;
}
