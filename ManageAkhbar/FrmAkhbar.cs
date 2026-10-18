using DevExpress.XtraEditors;
using ManageAkhbar.Class;
using static ManageAkhbar.Class.RssNewsService;



namespace ManageAkhbar
{
    public partial class FrmAkhbar : XtraForm
    {
        public FrmAkhbar()
        {
            InitializeComponent();
        }

        private void FrmAkhbar_Load(object sender, EventArgs e)
        {

        }

       
        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            var service = new RssNewsService();
            var tasnim= NewsSources.GetTasnim(); // 
            var mehr= NewsSources.GetMehr(); // 
            var fera= NewsSources.GetFeraro(); // 
            var tasks = new List<Task<List<NewsItem>>>();

            //foreach (var feed in mehr)
            //{
            //    tasks.Add(service.FetchMehr(feed.Value, feed.Key));
            //}

            //#region Tasnim

            //foreach (var feed in tasnim)
            //{
            //    tasks.Add(service.FetchTasnimToModelAsync(feed.Value, feed.Key));
            //}

            //#endregion

            #region Isna

            foreach (var feed in fera)
            {
                tasks.Add(service.FetchFararu(feed.Value, feed.Key));
            }

            #endregion

            // ایجاد یک Task برای هر خبرگزاری


            //foreach (var feed in allFeeds)
            //{
            //    tasks.Add(service.FetchFromRssAsync(feed.Value, feed.Key));
            //}

            //// ✅ اجرای همزمان همه تسک‌ها (Non-blocking)
            ///
            /// 
            var results = await Task.WhenAll(tasks);

            //// جمع‌آوری همه اخبار از نتایج
            var allNews = new List<NewsItem>();
            foreach (var result in results)
            {
                allNews.AddRange(result);
            }

            //// مرتب‌سازی بر اساس تاریخ
            //allNews = allNews.OrderByDescending(x => x.PubDate).ToList();

            // نمایش در Grid یا ListBox

        }
    }
}
