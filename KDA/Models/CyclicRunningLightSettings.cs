using System;

namespace KDA.Models;

[Serializable]
public class CyclicRunningLightSettings
{
    private int col = 8;
    public int Columns
    {
        get => col;
        set
        {
            if (value < 1)
            {
                MsgBoxService.ShowError("键的列数数目不能小于1！");
                return;
            }
            if (colorCount * value > 41 || CustomColors.Count * value > 41)
            {
                MsgBoxService.ShowError("列数与颜色相乘不能大于41!");
                return;
            }
            col = value;
        }
    }

    private int colorCount = 3;
    public int ColorCount
    {
        get => colorCount;
        set
        {
            if (value < 1)
            {
                MsgBoxService.ShowError("自动生成随机颜色的数目不能小于1！");
                return;
            }
            if (value * col > 41)
            {
                MsgBoxService.ShowError("列数与颜色相乘不能大于41!");
                return;
            }
            colorCount = value;
        }
    }

    private int animationDuration = 25;
    public int AnimationDuration
    {
        get => animationDuration;
        set
        {
            if (value < 1)
            {
                MsgBoxService.ShowError("动画周期不能小于1！");
                return;
            }
            animationDuration = value;
        }
    }

    public CustomColors CustomColors { get; set; } = new CustomColors();

    public bool IsAutoColor { get; set; }
}
