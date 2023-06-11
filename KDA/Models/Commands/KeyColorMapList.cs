using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KDA.Models.Commands;

public class KeyColorMapList : List<KeyColorMap>
{
    public KeyColorMapList()
    {
    }

    public KeyColorMapList(byte count)
    {
        for (byte i = 0; i < count; i++)
        {
            Add(new KeyColorMap(i));
        }
    }

    private List<KeyColorData> GetColorDatas()
    {
        List<KeyColorData> keyColorDatas = new();
        foreach (KeyColorMap x in this)
        {
            keyColorDatas.AddRange(x.MapDatas);
        }
        return keyColorDatas;
    }

    public KeyColorData GetColorData(Key key)
    {
        var keyColorDatas = GetColorDatas();
        if (keyColorDatas != null)
        {
            return keyColorDatas.FirstOrDefault(x => x.Key == key && x.Key != Key.None);
        }
        return null;
    }

    public KeyColorDataList SetColorDatas(AnimationKeyGroups groups)
    {
        KeyColorDataList datas = new();
        foreach (var model in groups.GetKeyModels())
        {
            KeyColorData data = GetColorData(model.Key);
            if (data != null)
            {
                data.ColorR = model.AnimationColor.R;
                data.ColorG = model.AnimationColor.G;
                data.ColorB = model.AnimationColor.B;
                data.ColorA = (byte)(model.AnimationColor.A/255.0*100);
            }
            datas.Add(data);
        }
        return datas;
    }
}
