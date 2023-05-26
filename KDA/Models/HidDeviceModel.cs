using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;

public class HidDeviceModel
{
    public string Manufacturer { get; }

    public string Product { get; }

    public string Description { get; }

    public int Version { get; }

    public string SerialNumber { get; }

    public string VendorHexId { get; }

    public string ProductHexId { get; }

    public short InputReportByteLength { get; }

    public short OutputReportByteLength { get; }

    public short FeatureReportByteLength { get; }

    public short Usage { get; }

    public short UsagePage { get; }

    public string DevicePath { get; }

    public HidDeviceModel()
    {

    }

    public HidDeviceModel(string manufacturer, string product, string des, int version, string serialNumber, string vendorHexId,
                          string productHexId, short inputReportByteLength, short outputReportByteLength,
                          short featureReportByteLength, string devicePath)
    {
        Manufacturer = manufacturer;
        Product = product;
        Description = des;
        Version = version;
        SerialNumber = serialNumber;
        VendorHexId = vendorHexId;
        ProductHexId = productHexId;
        InputReportByteLength = inputReportByteLength;
        OutputReportByteLength = outputReportByteLength;
        FeatureReportByteLength = featureReportByteLength;
        DevicePath = devicePath;
    }


    public HidDeviceModel(string manufacturer, string product, string des, int version, string serialNumber, string vendorHexId,
                          string productHexId, short usage, short usagePage, short inputReportByteLength,
                          short outputReportByteLength, short featureReportByteLength, string devicePath)
    {
        Manufacturer = manufacturer;
        Product = product;
        Description = des;
        Version = version;
        SerialNumber = serialNumber;
        VendorHexId = vendorHexId;
        ProductHexId = productHexId;
        Usage = usage;
        UsagePage = usagePage;
        InputReportByteLength = inputReportByteLength;
        OutputReportByteLength = outputReportByteLength;
        FeatureReportByteLength = featureReportByteLength;
        DevicePath = devicePath;
    }

    public override string ToString()
    {
        return $"Vid_{VendorHexId}  Pid_{ProductHexId}  {Description}";
    }
}
