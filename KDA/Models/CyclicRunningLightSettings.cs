using System;
using TianWeiToolsPro.Service;

namespace KDA.Models
{
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
                if ((IsAutoColor && (colorCount + 1) * value > 42))
                {
                    MsgBoxService.ShowError("列数与自动（颜色数+1）相乘不能大于42!");
                    return;
                }
                if ((IsAutoColor == false && (CustomColors.Count + 1) * value > 42))
                {
                    MsgBoxService.ShowError("列数与手动（颜色数+1）相乘不能大于42!");
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
                if (IsAutoColor && (value + 1) * col > 42)
                {
                    MsgBoxService.ShowError("列数与自动（颜色数+1）相乘不能大于42!");
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
                if (value < 0)
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
}