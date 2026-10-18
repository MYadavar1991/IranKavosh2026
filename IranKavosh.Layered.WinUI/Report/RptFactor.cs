using DevExpress.CodeParser;
using DevExpress.XtraReports.UI;
using IranKavosh.Layered.WinUI.ClassReport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace IranKavosh.Layered.WinUI.Report
{
    public partial class RptFactor : DevExpress.XtraReports.UI.XtraReport
    {
        public RptFactor()
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
            //  var getData = DataSource as List<GroupedPerson>;
            //var getData = DataSource as ClassReport.ClassReport;
            var getData = Tag as ClassReport.ClassReport.ModelFactor;
            
            lblHeader2.Text = getData.Header;
            lblNum.Text = getData.Num;
            lblDate.Text = getData.Date;
            lblBuyerName.Text = getData.Buyer;
            lblBuyerMobile.Text = getData.Mobile;
            lblAddress.Text = getData.Address;
            this.DataSource = getData.FactorDetails;

            //Detail.Band= getData.FactorDetails
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
