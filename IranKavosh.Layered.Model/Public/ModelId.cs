namespace IranKavosh.Layered.Model.Public
{
    public class ModelId
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public ModelId()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
            IsActive = true;
            IsDelete = false;
        }

    }
}
