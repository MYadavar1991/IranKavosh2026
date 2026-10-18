using MediaDevices;

namespace IranKavosh.Layered.FileCopy
{
    public static class ClsDeviceAndroid
    {
        public static MediaDevice? ConnectFirstDevice(List<string> usbFlash)
        {
            var devices = MediaDevice.GetDevices().ToList();
            if (usbFlash != null)
                foreach (string s in usbFlash)
                {
                    devices.RemoveAll(r => r.FriendlyName.ToLower() == s.ToLower());
                }

            if (!devices.Any())
            {
                return null;
            }

            var device = devices.First();
            device.Connect();
           
            return device;
        }
        public static List<string> ListDirectories(MediaDevice device, string path = @"\")
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
