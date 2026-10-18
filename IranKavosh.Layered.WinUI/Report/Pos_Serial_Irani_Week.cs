using DevExpress.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using IranKavosh.Layered.WinUI.ClassReport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using static IranKavosh.Layered.WinUI.Forms.FrmShowVideo;

namespace IranKavosh.Layered.WinUI.Report
{
    public partial class Pos_Serial_Irani_Week : DevExpress.XtraReports.UI.XtraReport
    {
        public Pos_Serial_Irani_Week()
        {
            InitializeComponent();
        }


        protected override void AdjustDataSource()
        {
            //var getData = this.DataSource;
            var getData = this.Tag;
            base.AdjustDataSource();
        }

        protected override void BeforeReportPrint()
        {
            //SesionImg.Visible = Sesionlbl2.Visible = false;
            ////  var getData = DataSource as List<GroupedPerson>;
            ////var getData = DataSource as ClassReport.ClassReport;
            var getData = Tag as List<ModelPosters>;
            DataSource = getData;
            PageHeightF = 23.2F* getData.Count;
            PageHeightF -= 0.5F;


            //lblName.Text = getData.Name;
            //lblGenere.Text = getData.Genere;
            ////imgFilm.ImageSource = new ImageSource(DXImage.FromStream(new ));
            //imgFilm.ImageSource = new ImageSource(DXImage.FromBase64String(getData.Image));
            //if (getData.MultimediaHeader != null)
            //{
            //    if (getData.MultimediaHeader.ChapterCount > 1 && getData.Serial)
            //    {
            //        SesionImg.Visible = Sesionlbl2.Visible = true;
            //        Sesionlbl2.Text = getData.MultimediaHeader.ChapterCount.ToString();

            //        lblName.Text = getData.Name + " - " + "قسمت (" + getData.MultimediaHeader.EndChapterDetailsCount + ")";
            //    }

            //    if (getData.MultimediaHeader.ChapterCount == 1 && getData.Serial)
            //    {
            //        SesionImg.Visible = Sesionlbl2.Visible = false;
            //        Sesionlbl2.Text = getData.MultimediaHeader.ChapterCount.ToString();

            //        lblName.Text = getData.Name + " - " + "قسمت (" + getData.MultimediaHeader.EndChapterDetailsCount + ")";
            //    }

            //}


            // Example: set lblPasdar.Text to the count of persons
            //var getAlllbl = this.AllControls<XRLabel>().ToList();
            //if (getData != null)
            //    foreach (XtraForm1.GroupedPerson p in getData)
            //    {
            //        if (getAlllbl.Any(f => f.Tag.ToString() == p.NoeTile))
            //        {
            //            var getFind = getAlllbl.First(f => f.Tag.ToString() == p.NoeTile);
            //            getFind.Text = p.Count.ToString();
            //        }

            //        var getSumN = getData.Sum(s => s.Count).ToString();
            //        lblSumN.Text = getSumN;

            //        var getSumS = getData.First().Hour.ToString();
            //        //var getSumS = getData.Sum(s => s.Hour).ToString();
            //        //lblSumS.Text = getSumS;
            //    }
            ////  lblPasder.Text = "0";

            base.BeforeReportPrint();
        }
    }
}
