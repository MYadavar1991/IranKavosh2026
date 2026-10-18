namespace IranKavosh.Layered.Service.Middleware
{
    public class ModelFullPriceList
    {
        public Guid ProductId { get; set; }
        public Guid PriceListId { get; set; }

        public string Title { get; set; }
        public string NameApp { get; set; }
        public string FullTitle { get; set; }
        public string FuntType { get; set; }
        public long Price { get; set; }
        public List<ModelFullPriceListDiscount> Count_Discount { get; set; }

    }

    public class ModelFullPriceListDiscount
    {
        public int Count { get; set; }
        public float Discount { get; set; }
    }
}
