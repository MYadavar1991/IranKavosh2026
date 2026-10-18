using MyCom.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranKavosh.Layered.WinUI.Class
{
    public static class ClassFillToCopy
    {
        public static string FillToCopy(object getfileModel)
        {
            var Json = JsonConvert.SerializeObject(getfileModel);
            var encrypt = ClassEncryptDecrypt.Encrypt(Json, "IranKavosh123456");
            return encrypt;
        }
       
    }
}
