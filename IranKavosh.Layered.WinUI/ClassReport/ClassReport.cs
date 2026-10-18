using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IranKavosh.Layered.Service.Middleware;

namespace IranKavosh.Layered.WinUI.ClassReport
{

    public class ClassReport
    {
        public class ModelPoster
        {
            //public Image Image { get; set; }
            /// <summary>
            /// base64
            /// </summary>
            public string Image { get; set; }
            public string Name { get; set; }

            public string Genere { get; set; }
            public bool Serial { get; set; }
            public ModelMultimediaHeader MultimediaHeader { get; set; }

        }

        public class ModelFactor
        {
            public string Date { get; set; }
            public string Num { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string Header { get; set; }
            public string Buyer { get; set; }
            public List<ModelFactorDetail> FactorDetails { get; set; }
        }

        public class ModelFactorDetail
        {
            public long Code { get; set; }
            public string Product { get; set; }
            public long Count { get; set; }
            public long Price { get; set; }
            public long Total { get; set; }
        }
    }

}
