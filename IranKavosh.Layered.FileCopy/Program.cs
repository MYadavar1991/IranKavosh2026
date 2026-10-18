using DevExpress.Skins;
using DevExpress.UserSkins;
using MyCom.Class;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using static MyCom.Class.ClassFileManage;
//using MediaDevices;

namespace IranKavosh.Layered.FileCopy
{
    public static class Program
    {




        /// <summary>
        ///  The main entry point for the application.
        /// param = 3 => (Copy To Flash)
        /// param = 4 => (Copy Fast To Hdd) (دسته جمعی)
        /// param = 5 => (Copy To Android)
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            //string s1 = "";
            //foreach (var s in param)
            //{
            //    s1 += s + Environment.NewLine;
            //}

            //MessageBox.Show(s1);
            //return;
            #region Test 2
            // 0 => data
            // 1 => MemoryId
            // 2 => 1
            // 3 => AddFast
            // 4 => android
            string a0 =
                "FKBYlmankTKe2CPq4pro50GWtmCvu+V0PYmKkZptUYWSHbP1V6dVPqX6wo+Il2NY1Gs83Mm4UK9dbgTqFF2iT858ze/3LRaVoLiFDB2cGoxpqLYIuKmMj7B6sC8mPQLOmxp2Esptdv/RfnyTfE0tzyMLw4AuhEc4vsdGkH2UCpEaamj5PB//ZuPiR4HDqTsGjxY1Jnij+uLoj5R356Ie9Nv5Yo/JJs5OUpc8ohNmFevY/npfFrUcUvQ4rnZStKorYpb/wol8bpxWRLecMqNOla/B7KbAuRyKYFrfuCmakKB62gevw+yZJfJx8mPoOlI3SAf2SXFkJFR0u59donmKIw==";
            //00000000-0000-0000-0000-000000000000


            //string a0 = "GqsAXN97JNjXywc/9q1UDjvemCHxelGkkij+xQuL4IZvR+LqEj8j9VqGzdwceBOHsMpTBQe8cOjrAkTBtDoMbCo7LlIZKZefW7B5J6sHOtRnC5F81JpA+QDq76aly5rj73m5YSZZJ+AdzCjYtehzo4/cuZqgOyK9P9pyWzhpBYSBQeshrArNEhYkp5y367it7xc6brpjae5GUEF0f6CC9rRpFxAVPzX948Uy1Go6phjedrswrUHtbTS02P2BVMEIo3ufvsMMoOWvFqpjORxb0Va7m8AAqp37U3LKUs3RmhR0D14yiF0lULkqD0G72gfPDv9Y47UneOdeR6H1vmBVoXowahHWCfC/mDT2l8m3K80mA5LunQy97A7/fap/nu7EH9vaqsiHEYAkEUU2EMe0GuQjz8fA4XS9SENKRDsW52wsqMd3STFkuUPqYpmrSBrSYMHi9n2rAV90UlMVFtM/loSvEnVxjL3jXZWjuasqW1vEwSHQ3ESaCJ9e4fs3yF4V+O0tx7KZAZX8Z93g3EQcEA==";
            string a1 = "00000000-0000-0000-0000-000000000000";
            //string a2 = "Q:\\KavoshVideo";
            string a2 = "5034";
            //string a3 = "AddFast";  or
            string a4 = "";
            //foreach (var s in param)
            //{
            //    MessageBox.Show(s);
            //}
            //string a4 = "Android";
            // string a4 = "";


           //  param = [a0, a1, a2];
           //  param = [a0, a1, a2, a3,a4];

            #endregion


            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            //Application.Run(new FrmSelectAndroidMemory(new KeyValuePair<List<ModelFile>, ulong>()));
            //MessageBox.Show(param.Length.ToString());
            //Application.Run(new FrmAddVideo(@"Q:\KavoshVideo"));
            //return;


            if (param.Length > 0)
            {
                #region Copy Andoid

                if (param.Length == 5)
                {
                    //MessageBox.Show("Enter1");
                    bool android = param[4] == "android";
                    try
                    {
                        //var getAndroid = param[4];

                        if (android)
                        {

                            //var getFileDec = JsonConvert.DeserializeObject<List<ClassFileManage.ModelFile>>(ClassEncryptDecrypt.Decrypt(param[0], "IranKavosh123456"));
                            //var _dataCopyAndroid= JsonConvert.DeserializeObject<KeyValuePair<List<ClassFileManage.ModelFile>, ulong>>(getFileDec);


                            var getFile2 = ClassEncryptDecrypt.Decrypt(param[0], "IranKavosh123456");
                            var _dataCopyAndroid = JsonConvert.DeserializeObject<KeyValuePair<List<ModelFile>, ulong>>(getFile2);

                            //foreach (var i in _dataCopyAndroid.Key)
                            //{
                            //    MessageBox.Show(i.From);
                            //}
                            Application.Run(new FrmSelectAndroidMemory(_dataCopyAndroid));
                            return;
                            //Application.Run(new FrmCopyFile(getFile, getDevice, ""));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

                #endregion
                //MessageBox.Show("Enter2");
                var getFile = JsonConvert.DeserializeObject<List<ClassFileManage.ModelFile>>(ClassEncryptDecrypt.Decrypt(param[0], "IranKavosh123456"));
                Guid getMemoryId = default;
                try
                {
                    getMemoryId = new Guid(param[1]);
                }
                catch (Exception e)
                {

                }
                var to = param[2];
         
  


                bool fast = param.Length == 4;
                Application.Run(new FrmCopyFile(getFile, getMemoryId, to, fast));
            }

        }
    }
}