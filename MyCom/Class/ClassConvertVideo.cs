using DevExpress.XtraEditors;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
namespace MyCom.Class
{
    public class ClassConvertVideo
    {

        private  (int Width, int Height) GetDimensions(string qualityType)
        {
            return qualityType switch
            {

                "240" => (550, 300),    // عرض 640، ارتفاع 360 (نسبت 16:9)
                "360" => (640, 360),    // عرض 640، ارتفاع 360 (نسبت 16:9)
                "720" => (1280, 720),   // عرض 1280، ارتفاع 720 (نسبت 16:9)
                "1080" => (1920, 1080), // عرض 1920، ارتفاع 1080 (نسبت 16:9)
                "Trl" => (784, 394), // عرض 1920، ارتفاع 1080 (نسبت 16:9)
                _ => (640, 360)         // پیش‌فرض 360p
            };
        }

        public enum SizeVideo
        {

            //a240,
            a360,
            a720,
            a1080,
            trl,
            defualt
        }

        public async Task ConvertAllFilesAsync(VideoCodec videoCodec, List<string> files, SizeVideo sizeVideo, ProgressBarControl barControl = null)
        {
            string ffmpegPath = AppContext.BaseDirectory;
            FFmpeg.SetExecutablesPath(ffmpegPath);

            // var getfile = Directory.GetFiles(@"C:\Users\Mojtaba\Downloads\Telegram Desktop", "*.mp4");
            //foreach (var fileName in getfile)
            //{
            //    listBoxControl1.Items.Add(fileName);
            //}

            foreach (var item in files)
            {
                var fileName = item.ToString();
                var geOutfile = "x264-" + Path.GetFileNameWithoutExtension(fileName);
                var getOutFile2 = Path.Combine(Path.GetDirectoryName(fileName), geOutfile) + ".mp4";

                var mediaInfo = await FFmpeg.GetMediaInfo(fileName);

                var inputVideo = mediaInfo.VideoStreams.FirstOrDefault();
                var inputAudio = mediaInfo.AudioStreams.FirstOrDefault();

                if (inputVideo == null)
                    throw new InvalidOperationException("هیچ استریم ویدئویی در فایل ورودی یافت نشد.");

                (int Width, int Height) videoSize = new ValueTuple<int, int>(1280, 720);
                var minBitrate = 1_100_000;

               // switch (cmbSize.Text.ToLower())
               switch (sizeVideo)
               {
                   //case "default":
                   case SizeVideo.defualt:
                       videoSize = default;
                       break;

                   case SizeVideo.trl:
                   {
                       videoSize = GetDimensions("Trl");
                       minBitrate = 250_000;
                   }
                       break;
                   //case "تیزر":
                   case SizeVideo.a360:
                   {
                       videoSize = GetDimensions(360.ToString());
                       minBitrate = 190_000;
                   }
                       break;

                   //case "720":
                   case SizeVideo.a720:
                   {
                       videoSize = GetDimensions(720.ToString());
                       minBitrate = 1_000_000;
                   }
                       break;

                   //case "1080":
                   case SizeVideo.a1080:
                   {
                       videoSize = GetDimensions(1080.ToString());
                       minBitrate = 1_150_000;
                   }
                       break;
               }

                //videoCodec == VideoCodec.av1
                inputVideo.SetCodec(videoCodec);
                //inputVideo.SetCodec("av1_nvenc");
                if (videoSize != default)
                    inputVideo.SetSize(videoSize.Width, videoSize.Height);
                //inputVideo.SetBitrate(minBitrate);
                IVideoStream videoStream = inputVideo;

                IConversion conversion = FFmpeg.Conversions.New()
                    .AddStream(videoStream)

                    .AddParameter("-rc vbr")
                    .AddParameter("-cq 20")
                    .AddParameter("-preset p7")
                    .AddParameter("-tune hq")
                    .AddParameter("-profile:v main")
                    .AddParameter("-spatial_aq 1")
                    .AddParameter("-aq-strength 8")
                    .AddParameter("-temporal_aq 1")
                    .AddParameter("-rc-lookahead 32")

                    .SetOutput(getOutFile2)
                    .SetOverwriteOutput(true);

                if (inputAudio != null)
                {
                    var audioStream = inputAudio.CopyStream();
                    conversion = conversion.AddStream(audioStream);
                }

                if (barControl != null)
                {
                    barControl.Properties.Maximum = 100;
                    barControl.Properties.ShowTitle = true;
                    conversion.OnProgress += (sender, args) =>
                    {
                        Console.WriteLine(@$"پیشرفت: {args.Percent:0.0}%  |  زمان پردازش: {args.Duration}");
                        barControl.Invoke(() => { barControl.EditValue = args.Percent; });
                    };
                }

                // اینجا تا فایل فعلی کامل نشه، حلقه جلو نمی‌ره
                await conversion.Start();
            }
        }
    }
}
