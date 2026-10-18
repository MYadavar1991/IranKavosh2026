using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MediaDevices;


namespace IranKavosh.Layered.WinUI
{
    public static class ClsDeviceAndroid
    {
        public static MediaDevice ConnectFirstDevice()
        {
            try
            {
                var devices = MediaDevice.GetDevices().ToList();

                if (!devices.Any())
                    throw new Exception("هیچ دستگاه MTP متصل نیست.");

                var device = devices.First();
                device.Connect();

                Console.WriteLine(@"Connected to: " + device.FriendlyName);

                return device;
            }
            catch (Exception e)
            {
                
            }

            return null;
        }
        public static List<string> ListDirectories(MediaDevice device, string path=@"\")
        {
            var dirs = device.GetDirectories(path);
            return dirs.ToList();
        }
        public static void CopyToPhone(MediaDevice device, string localPath, string devicePath)
        {
            device.UploadFile(localPath, devicePath);
            device.ObjectUpdated += (s1, e1) =>
            {

            };
        }

}
 

}
