using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using IranKavosh.Layered.WinUI.Forms;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IranKavosh.Layered.WinUI
{
    internal static class Program
    {
        private static Mutex _mutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            
            GlobalExceptionHandler.Initialize("ایران کاوش", true, true);
            var getAddress = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<string> keyRege = new List<string>
            {
                "AddVideo", "CreatePoster", "SortName"
            };
            if (param.Length > 0)
            {
                // MessageBox.Show(param[0]);
                if (param[0].ToLower() == "AddVideo".ToLower())
                {
                reWri:
                    try
                    {
                        StreamWriter sw = new StreamWriter(getAddress + "\\SelFiles.txt", true);

                        foreach (var s in param)
                        {
                            // MessageBox.Show(s);
                            if (keyRege.All(a => a.ToLower() != s.ToLower()))
                                sw.WriteLine(s);
                        }

                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                        goto reWri;
                    }
                }
                else if (param[0].ToLower() == "SortName".ToLower())
                // else //if (param[0].ToLower() == "SortName".ToLower())
                {
                reWri2:
                    try
                    {
                        StreamWriter sw = new StreamWriter(getAddress + "\\SelFiles.txt", true);
                        sw.WriteLine("Sort");
                        foreach (var s in param)
                        {
                            // MessageBox.Show(s);
                            if (keyRege.All(a => a.ToLower() != s.ToLower()))
                            {
                                string directory = Path.GetDirectoryName(s); // خروجی: C:\asdasd\1\f
                                string fileName = Path.GetFileName(s); // خروجی: 154-1.mp4

                                // حذف عدد قبل از خط تیره
                                string newFileName = Regex.Replace(fileName, @"^\d+-", ""); // خروجی: 1.mp4

                                // ساخت آدرس جدید
                                string newPath = Path.Combine(directory, newFileName);

                                // File.Move(s, newPath);
                                Thread.Sleep(75);
                                sw.WriteLine(newPath);
                            }
                        }

                        //Thread.Sleep(1000);
                        sw.Close();

                        return;
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                        goto reWri2;
                    }
                }
            }

            #region Mutex

            string appName = Assembly.GetExecutingAssembly().GetName().Name;

            _mutex = new Mutex(true, appName, out var createdNew);
            if (!createdNew)
            {
                // برنامه قبلاً اجرا شده است
                //ClassMessageBox.ShowMSG(
                //    "برنامه در حال اجراست." + Environment.NewLine + "از قسمت پایین کنار ساعت اجراش کنید",
                //    Class_Text.Msg_Name,
                //    ClassMessageBox.enumIcon.بلندگو
                //);
                return;
            }

            //  Thread.Sleep(1000);

            #endregion
            // Thread.Sleep(200);




            //Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            BonusSkins.Register();
            //DevExpress.XtraEditors.WindowsFormsSettings.AllowHoverAnimation = DevExpress.Utils.DefaultBoolean.False;

            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            //UserLookAndFeel.Default.SetSkinStyle("WXI Compact"); // یا "The Bezier", "Office 2019 Colorful"
            Application.SetCompatibleTextRenderingDefault(false);



            //Application.Run(new FrmPrint1());
            //return;
            string[] lines = [];
            if (File.Exists(getAddress + "\\SelFiles.txt"))
                lines = File.ReadAllLines(getAddress + "\\SelFiles.txt");

            int lineCount = lines.Length; // تعداد خطوط

            if (lineCount == 0)
            {

                //Application.Run(new FrmLogin());
                Application.Run(new FrmMain2());
                //Application.Run(new FrmBuySell());
            }
            else
            {
                //if (lines[0].ToLower() == "sort")
                //{
                //    for (int i = 0; i < lines.Length; i++)
                //    {
                //        var s = lines[i];
                //        if (File.Exists(s) && keyRege.All(a => a.ToLower() != s.ToLower()))
                //        {
                //            string directory = Path.GetDirectoryName(s); // خروجی: C:\asdasd\1\f
                //            string fileName = Path.GetFileName(s); // خروجی: 154-1.mp4

                //            // حذف عدد قبل از خط تیره
                //            string newFileName = Regex.Replace(fileName, @"^\d+-", ""); // خروجی: 1.mp4

                //            // ساخت آدرس جدید
                //            string newPath = Path.Combine(directory, newFileName);

                //             File.Move(s, newPath);
                //            Thread.Sleep(75);

                //        }
                //    }
                //}
                //else
                //{

                Application.Run(new FrmManageVideo(true));
                // }

            }
        }
    }
}

