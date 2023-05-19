using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianWeiToolsPro.Extensions;

namespace KDA.Services;
public class BootloaderHelper
{
    private const byte requestCode = 0x3F;
    private const byte responseCode = 0x21;

    /// <summary>W:0x57 </summary>
    private const byte writeDataRequestCode = 0x57;

    /// <summary>D:0x44 </summary>
    private const byte writeDataCode = 0x44;

    private static byte[] writeDataHeader = Encoding.ASCII.GetBytes("?W");
    private static byte[] sendDataHeader = Encoding.ASCII.GetBytes("?D");

    public static bool WriteDataRequest(byte addr, uint lenth)
    {

        HidReport report = new HidReport(8);
        return true;
    }

    public static bool WriteData(byte addr, byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return false;
        }
        byte[] cmdFrame = new byte[7];
        cmdFrame[0] = sendDataHeader[0];
        cmdFrame[1] = sendDataHeader[1];
        cmdFrame[2] = addr;
        cmdFrame[3] = (byte)bytes.Length;
        cmdFrame[4] = 0x0D;
        cmdFrame[5] = 0x0A;
        cmdFrame[6] = cmdFrame.GetCheckSum();

        byte[] dataFrame = new byte[bytes.Length + 1];
        bytes.CopyTo(dataFrame, 0);
        dataFrame[^1] = dataFrame.GetCheckSum();

        byte[] totalFrame = cmdFrame.Concat(dataFrame).ToArray();
        GCH.Device.Write(totalFrame);
        byte[] respone = GCH.Device.Read(50)?.Data;
        if (respone == null || respone.Length < 4)
        {
            return false;
        }

        return true;
    }

}
